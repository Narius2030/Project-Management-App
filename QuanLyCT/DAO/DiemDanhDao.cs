using QLCongTy.DTO;
using System.Data;
using System.Data.SqlClient;

namespace QLCongTy.DAO
{
    public class DiemDanhDao
    {
        DBConnection dbconn = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);

        public DataTable layDanhSachDiemDanh()
        {
            string sqlStr = "SELECT * FROM vw_ngaynghi_trong_duan";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public void ThemNgayNghi(DIEMDANH dd)
        {
            //string sqlStr = $@"INSERT INTO DIEMDANH VALUES('{dd.Ngay}', '{dd.MaNV}', '{dd.NoiDung}')";
            //dbconn.ExecuteCommand(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ngay", SqlDbType.Date) {Value = dd.Ngay},
                new SqlParameter("@manv ", SqlDbType.VarChar,10) {Value = dd.MaNV},
                new SqlParameter("@noidungnghi", SqlDbType.NVarChar, 20) {Value= dd.NoiDung}
            };
            dbconn.ExecuteProcedure("sp_themNgayNghi", parameters);
        }

        public void TinhTimeSprint(string manv)
        {
            string sqlStr = $@"SELECT 
                            MaNV, n.MaDA, n.SoGioMotNg, gd.MaGiaiDoan
                        FROM NHOM n
                        JOIN GIAIDOAN gd ON n.MaDA=gd.MaDA 
                        WHERE n.MaNV='{manv}'";

            DataTable dataset = dbconn.ExecuteQuery(sqlStr);

            // Lặp qua các GiaiDoan mà Nhân viên tham gia dự án
            foreach (DataRow row in dataset.Rows)
            {
                int mada = int.Parse(row["MaDA"].ToString());
                int soGioMotNg = int.Parse(row["SoGioMotNg"].ToString());
                string magd = row["MaGiaiDoan"].ToString();

                // Thực hiện tính số thời gian giai đoạn đang làm
                sqlStr = $"SELECT dbo.sfn_CapNhatTimeSprint('{magd}', {mada}, {soGioMotNg})";
                double soGioLam = double.Parse(dbconn.ExecuteScalar(sqlStr).ToString());

                // Thực hiện tính số thời gian nghỉ trúng phải giai đoạn đang làm
                sqlStr = $"SELECT dbo.sfn_TimThoiGianNghi('{manv}', '{magd}')";
                double soGioNghi = double.Parse(dbconn.ExecuteScalar(sqlStr).ToString());

                // Tính Time Sprint
                double res = soGioLam - soGioNghi;

                //Cập nhật UOCLUONG cho NhanVien sau khi nghỉ
                sqlStr = $@"UPDATE UOCLUONG SET TimeSprint={res} WHERE MaNV='{manv}' AND MaGiaiDoan='{magd}' AND MaDA={mada}";
                dbconn.ExecuteCommand(sqlStr);
            }
        }
    }
}
