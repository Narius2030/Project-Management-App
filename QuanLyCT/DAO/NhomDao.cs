using QLCongTy.DTO;
using QLCongTy.Views.NhanSu;
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
        DBConnection dbconn = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);
        public void ThemThanhVien(NHOM nhom)
        {
            try
            {
                string sqlStr = $@"INSERT INTO NHOM VALUES ('{nhom.MaNV}', '{nhom.TenNhom}', {nhom.MaDA}, {nhom.SoGioMotNg})";
                dbconn.ExecuteCommand(sqlStr);
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
        public void ThemTruongNhom(TRUONGNHOM tn)
        {
            string sqlStr = $@"INSERT INTO TRUONGNHOM VALUES ('{tn.TenNhom}', {tn.MaDA}, '{tn.MaNV}')";
            dbconn.ExecuteCommand(sqlStr);
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

        public int CheckRole(int MaDA, string TenNhom, string MaNV)
        {
            //CEO 0 - PM 1 -- LEAD 2 -- PM LEAD 3
            string sqlStr = $"SELECT NHANVIEN.MaNV FROM NHANVIEN WHERE NHANVIEN.ChucVu = 'CEO' AND NHANVIEN.MaNV = '{MaNV}'";
            if (dbconn.ExecuteQuery(sqlStr).Rows.Count != 0 && MaNV == Convert.ToString(dbconn.ExecuteQuery(sqlStr).Rows[0][0]))
            {
                return 0;
            }
            else
            {
                sqlStr = $"SELECT DUAN.MaPM FROM DUAN WHERE DUAN.MaDA = {MaDA}";
                if (MaNV == Convert.ToString(dbconn.ExecuteQuery(sqlStr).Rows[0][0]))
                {
                    sqlStr = $"SELECT MaNV FROM TRUONGNHOM WHERE MaDA = {MaDA} AND TenNhom = '{TenNhom}'";
                    if (MaNV == Convert.ToString(dbconn.ExecuteQuery(sqlStr).Rows[0][0]))
                    {
                        return 3;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    sqlStr = $"SELECT MaNV FROM TRUONGNHOM WHERE MaDA = {MaDA} AND TenNhom = '{TenNhom}'";
                    if (MaNV == Convert.ToString(dbconn.ExecuteQuery(sqlStr).Rows[0][0]))
                    {
                        return 2;
                    }
                    else
                    {
                        return 4;
                    }
                }
            }
        }
    }
}
