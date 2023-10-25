using QLCongTy.DTO;
using QLCongTy.Views.NhanSu;
using System;
using System.Collections.Generic;
using System.Data;
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
                    MessageBox.Show($"Thêm thành viên thất bại: {ex.Message}");
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
            string sqlStr = $"select distinct (TenNhom) From NHOM where NHOM.MaDA={mada} ";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable dsThanhVienNhom(int mada, string tennhom)
        {
            string sqlStr = $"SELECT * FROM NHOM WHERE MaDA = {mada} AND TenNhom = '{tennhom}'";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public Boolean KiemTraTonTaiNhomTruong(NHOM nhom)
        {
            string sqlStr = $"SELECT dbo.CheckTonTaiNhomTruong('{nhom.TenNhom}', {nhom.MaDA})";
            SqlCommand cmd = new SqlCommand(sqlStr, conn);
            conn.Open();
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
