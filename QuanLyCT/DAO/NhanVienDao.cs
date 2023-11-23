using Microsoft.VisualBasic.ApplicationServices;
using QLCongTy.DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace QLCongTy.DAO
{
    public class NhanVienDao
    {
        DBConnection Dbc = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);
        public int CheckTaiKhoan(NHANVIEN nv)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@matk", SqlDbType.NVarChar, 20) { Value = nv.MaTaiKhoan },
                new SqlParameter("@matkhau", SqlDbType.NVarChar, 20) { Value = nv.MatKhau },
                new SqlParameter("@check", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            
            Dbc.ExecuteProcedure("sp_ktrDangNhap", parameters);
            int check = Convert.ToInt32(parameters[2].Value);

            return check;
        }

        public DataTable DSNhanVien()
        {
            string sqlStr = "SELECT MaNV, HovaTenDem, Ten, ChucVu, Email, Levels, DiaChi, SDT FROM NHANVIEN";
            return Dbc.ExecuteQuery(sqlStr);
        }

        public string HoTenNhanVien(string manv)
        {
            string sqlStr = $"SELECT CONCAT(MaNV, '-', HovaTenDem,' ', Ten) AS HoTenNV FROM NHANVIEN WHERE MaNV = '{manv}'";
            DataTable result =  Dbc.ExecuteQuery(sqlStr);
            if (result.Rows.Count > 0)
            {
            }
            else
            {
                return $"Không tồn tại nhân viên có mã {manv}";
            }
            return result.Rows[0][0].ToString();
        }

        public DataTable GetDSLevels()
        {
            string sqlStr = $@"SELECT DISTINCT Levels FROM NHANVIEN";
            return Dbc.ExecuteQuery(sqlStr);
        }

        public DataTable GetDSChucVu()
        {
            string sqlStr = $@"SELECT DISTINCT ChucVu FROM NHANVIEN";
            return Dbc.ExecuteQuery(sqlStr);
        }
        public DataTable LocLevels(string levels)
        {
            string sqlStr = $"SELECT * FROM NHANVIEN WHERE Levels = '{levels}'";
            return Dbc.ExecuteQuery(sqlStr);
        }
        public DataTable LocChucVu(string chucvu)
        {
            string sqlStr = $"SELECT * FROM NHANVIEN WHERE ChucVu = '{chucvu}'";
            return Dbc.ExecuteQuery(sqlStr);
        }
        public void DoiMatKhau(string manv, string matkhaunhap, string matkhaumoi)
        {
            string matkhaucu = LayMatKhau(manv);

            if (matkhaunhap.Equals(matkhaucu))
            {
                string sqlStr = $"UPDATE NHANVIEN SET MatKhau = '{matkhaumoi}' WHERE MaTaiKhoan = '{manv}' AND MatKhau='{matkhaunhap}'";
                Dbc.ExecuteCommand(sqlStr);

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@user",SqlDbType.VarChar,20){Value =manv},
                    new SqlParameter("@newpass ",SqlDbType.VarChar,20){Value =matkhaumoi},
                    new SqlParameter("@oldpass ",SqlDbType.VarChar,20){Value =matkhaunhap},
                };
                Dbc.ExecuteProcedure("sp_UpdatePass", parameters);
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không hợp lệ");
            }
        }
        public string LayMatKhau(string manv)
        {
            string sqlStr = $"SELECT MatKhau FROM NHANVIEN WHERE MaNV='{manv}'";
            string matkhau = Dbc.ExecuteScalar(sqlStr).ToString();
            return matkhau;
        }

        public void ThemNhanVien(NHANVIEN nv)
        {
            string sqlStr = $@"INSERT INTO NHANVIEN VALUES ('{nv.MaNV}', '{nv.HovaTenDem}', '{nv.Ten}', '{nv.ChucVu}' , '{nv.Email}', '{nv.Levels}', N'{nv.DiaChi}', '{nv.SDT}', '{nv.MaTaiKhoan}', '{nv.MatKhau}');";
            try
            {
                Dbc.ExecuteCommand(sqlStr);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void XoaNhanVien(string manv)
        {
            string sqlStr = $@"DELETE FROM NHANVIEN WHERE MaNV='{manv}'";
            Dbc.ExecuteCommand(sqlStr);
        }
    }
}
