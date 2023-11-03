using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QLCongTy.DTO;
using QLCongTy.Views.NhanSu;

namespace QLCongTy.DAO
{
    public class DiemDanhDao
    {
        DBConnection dbconn = new DBConnection();
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        public DataTable layDanhSachDiemDanh()
        {
            string sqlStr = "SELECT * FROM vw_ngaynghi_trong_duan";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public void ThemNgayNghi(DIEMDANH dd)
        {
            string sqlStr = $@"INSERT INTO DIEMDANH VALUES('{dd.Ngay}', '{dd.MaNV}', '{dd.NoiDung}')";
            dbconn.ExecuteCommand(sqlStr);
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
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                double soGioLam = double.Parse(dbconn.ExecuteScalar(sqlStr).ToString());

                // Thực hiện tính số thời gian nghỉ trúng phải giai đoạn đang làm
                sqlStr = $"SELECT dbo.sfn_TimThoiGianNghi('{manv}', '{magd}', {soGioMotNg})";
                cmd = new SqlCommand(sqlStr, conn);
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
