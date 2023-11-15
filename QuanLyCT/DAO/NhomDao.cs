using QLCongTy.DTO;
using System;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Windows;

namespace QLCongTy.DAO
{
    public class NhomDao
    {
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        DBConnection dbconn = new DBConnection();
        public void ThemThanhVien(NHOM nhom)
        {
            using (QLDAEntities entity = new QLDAEntities())
            {
                try
                {
                    entity.NHOMs.Add(nhom);
                    entity.SaveChanges();
                }
                catch (Exception ex)
                {
                    SqlException sqlEx = ex.GetBaseException() as SqlException;
                    if (sqlEx != null)
                    {
                        MessageBox.Show("Lỗi SQL xảy ra: " + sqlEx.Message);
                        // Xử lý lỗi SQL cụ thể tại đây
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xảy ra khi: " + ex.Message);
                    }
                }
            }
        }
        public void ThemTruongNhom(TRUONGNHOM tn)
        {
            using (QLDAEntities entity = new QLDAEntities())
            {
                try
                {
                    entity.TRUONGNHOMs.Add(tn);
                    entity.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Thuc Thi That Bai: {ex.Message}");
                }
            }
        }
        public DataTable FindTruongNhom(NHOM nhom)
        {
            string sqlStr = $"SELECT MaNV FROM TRUONGNHOM WHERE MaNV = '{nhom.MaNV}' AND MaDA = {nhom.MaDA} AND TenNhom = '{nhom.TenNhom}'";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable laydanhsachnhom(int mada)
        {
            string sqlStr = $"select distinct (TenNhom) From NHOM where NHOM.MaDA={mada}";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable dsThanhVienNhom(int mada, string tennhom)
        {
            SqlParameter[] parame = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int){Value=mada},
                new SqlParameter("@tennhom",SqlDbType.NVarChar,20){Value=tennhom}
            };
            return dbconn.ExecuteProcedure("sp_dstvmotnhomtrongmotduan", parame);
        }
        public Boolean KiemTraTonTaiNhomTruong(NHOM nhom)
        {
            string sqlStr = $"SELECT dbo.CheckTonTaiNhomTruong('{nhom.TenNhom}', {nhom.MaDA})";
            int result = Convert.ToInt32(dbconn.ExecuteQuery(sqlStr).Rows[0][0]);
            MessageBox.Show(result.ToString());
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public DataTable XacDinhTruongNhom(NHOM nhom)
        {
            string sqlStr = $"SELECT MaNV FROM TRUONGNHOM WHERE MaDA = {nhom.MaDA} AND TenNhom = '{nhom.TenNhom}'";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public int FindSoGioMotNg(string manv, int mada)
        {
            string sqlStr = $@"SELECT DISTINCT(SoGioMotNg) FROM NHOM WHERE MaNV='{manv}' AND MaDA={mada}";
            int ketqua = Convert.ToInt32(dbconn.ExecuteScalar(sqlStr));
            return ketqua;
        }
    }
}
