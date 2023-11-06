using QLCongTy.DTO;
using QLCongTy.Views.NhanSu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace QLCongTy.DAO
{
    public class NhomDao
    {
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        DBConnection dbconn = new DBConnection();
        public void ThemThanhVien(NHOM nhom)
        {
            using(QLDAEntities entity = new QLDAEntities())
            {
                try
                {
                    entity.NHOMs.Add(nhom);
                    entity.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Số giờ làm việc 1 ngày của nhân viên trong dự án này không hợp lệ");
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
