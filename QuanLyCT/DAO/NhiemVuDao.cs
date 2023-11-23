using QLCongTy.DTO;
using System;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QLCongTy.DAO
{
    internal class NhiemVuDao
    {
        DBConnection dbconn = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);

        public DataTable DSNhiemVuNhom(string MaNV, int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@" select 
                                MaNhiemVu as [Nhiệm Vụ],TenNhiemVu as [Tên Nhiệm Vụ],TrangThai as [Trạng Thái],MaTienQuyet as [Mã Tiên Quyết],
                                ThoiGianUocTinh as [Giờ Ước Tính],ThoiGianLamThucTe as [Giờ Thực Tế] 
                            From v_DanhSachNhiemVuNhom
                            WHERE MaDA = {MaDA} AND MaGiaiDoan = '{MaGiaiDoan}' AND MaCV = {MaCV} AND TenNhom = '{TenNhom}' AND MaNV='{MaNV}'";
            return dbconn.ExecuteQuery(sqlStr);
        }

        public void ThemNhiemVu(NHIEMVU nv)
        {
            string sqlStr;
            if (nv.MaTienQuyet == null)
            {
                sqlStr = $@"INSERT INTO NHIEMVU VALUES ('{nv.MaNhiemVu}', NULL, '{nv.TrangThai}', NULL, '{nv.TenNhiemVu}', {nv.ThoiGianUocTinh}, '{nv.MaNV}', {nv.MaCV})";
            }
            else
            {
                sqlStr = $@"INSERT INTO NHIEMVU VALUES ('{nv.MaNhiemVu}', '{nv.MaTienQuyet}', '{nv.TrangThai}', NULL, '{nv.TenNhiemVu}', {nv.ThoiGianUocTinh}, '{nv.MaNV}', {nv.MaCV})";
            }
            dbconn.ExecuteCommand(sqlStr);
        }

        public void SuaNhiemVu(NHIEMVU nv)
        {
            string sqlStr = $@"UPDATE NHIEMVU SET TrangThai='{nv.TrangThai}', ThoiGianLamThucTe={nv.ThoiGianLamThucTe} WHERE MaNhiemVu='{nv.MaNhiemVu}'";
            dbconn.ExecuteCommand(sqlStr);
        }
        public int XoaNhiemVu(NHIEMVU nv)
        {
            try
            {
                string sqlStr = $@"DELETE NHIEMVU WHERE NHIEMVU.MaNhiemVu = '{nv.MaNhiemVu}'";
                dbconn.ExecuteCommand(sqlStr);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public DataTable DSNhiemVu(int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@"SELECT CONCAT(MaNhiemVu, ' - ' , TenNhiemVu) AS NhiemVu, MaNhiemVu
                     FROM v_DanhSachNhiemVuNhom
                     WHERE MaDA = {MaDA} AND MaGiaiDoan = '{MaGiaiDoan}' AND MaCV = {MaCV} AND TenNhom = '{TenNhom}'
                     ORDER BY MaNhiemVu";
            return dbconn.ExecuteQuery(sqlStr);
        }

        public string NhiemVuMoiNhat(int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@"SELECT Top 1 MaNhiemVu
                FROM v_DanhSachNhiemVuNhom
                WHERE MaDA = {MaDA} AND MaGiaiDoan = '{MaGiaiDoan}' AND MaCV = {MaCV} AND TenNhom = '{TenNhom}'
                ORDER BY MaNhiemVu DESC";
            DataTable result = dbconn.ExecuteQuery(sqlStr);
            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["MaNhiemVu"].ToString();
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được phân công nhiệm vụ nào", "Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return "00" + "CV" + MaCV.ToString("D2") + "DA" + MaDA.ToString("D2");
            }
        }
        public int KiemTraNhiemVuTienQuyet(string manhiemvu)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                  new SqlParameter("@manv",SqlDbType.VarChar, 10){Value=manhiemvu},
                  new SqlParameter("@check",SqlDbType.Real){Direction = ParameterDirection.Output}

            };
            dbconn.ExecuteProcedure("sp_KiemTraNhiemVuTienQuyet", sp);
            int ketqua = Convert.ToInt32(sp[1].Value);
            return ketqua;
            
        }
        public void SetNullTienQuyet(NHIEMVU nv)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@manhiemvu",SqlDbType.VarChar, 10){Value=nv.MaNhiemVu}
            };
            dbconn.ExecuteProcedure("sp_KiemTraNhiemVu", parameters);
        }
        public int CapNhatTimeTask(string manv, int maduan, string magiaidoan)
        {
            int ketqua;

            try
            {
                ketqua = Convert.ToInt32(dbconn.ExecuteScalar($"SELECT dbo.sfn_CapNhatTimeTask('{manv}',{maduan},'{magiaidoan}')"));
            }
            catch
            (Exception)
            {
                ketqua = 0;
            }
            return ketqua;
        }
        public int TongTimeTask(string manv, int maduan, string magiaidoan)
        {
            int ketqua;
            try
            {
                ketqua = Convert.ToInt32(dbconn.ExecuteScalar($"SELECT dbo.sfn_SumTimeTask('{manv}',{maduan},'{magiaidoan}')"));
            }
            catch
            (Exception)
            {
                ketqua = 0;
            }
            return ketqua;
        }
    }
}
