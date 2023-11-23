using QLCongTy.DTO;
using QLCongTy.Views.NhanSu;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace QLCongTy.DAO
{
    public class DuAnDao
    {
        DBConnection dbconn = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);

        public DataTable getProjectList(string MaTK)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
               new SqlParameter("@mataikhoan",SqlDbType.VarChar,20){Value=MaTK}
           };
           return dbconn.ExecuteProcedure("sp_Check_PM_TeamLead_CEO",parameters);
       }
        public DataTable getNhanLucDA(int mada)
        {
            //Funtion return a table
            string sqlStr = $@"SELECT 
	                            nv.MaNV, CONCAT(nv.HovaTenDem,' ',nv.Ten) AS HoTen, n.TenNhom, nv.Email
                            FROM NHANVIEN nv
                            JOIN NHOM n ON n.MaNV = nv.MaNV
                            WHERE n.MaDA = {mada}";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable getNhanLucCty()
        {
            // View
            string sqlStr = $@"SELECT
	                            MaNV, CONCAT(HovaTenDem,' ',Ten), Levels, Email
                            FROM NHANVIEN
                            WHERE MaNV <> 'NV002'";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable FilterLevel(string level)
        {
            // Function return a table
            string sqlStr = $@"SELECT
	                            MaNV, CONCAT(HovaTenDem,' ',Ten), Levels, Email
                            FROM NHANVIEN WHERE Levels='{level}'";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable DSDuAn(string matk)
        {
            return getProjectList(matk);
        }
        public void insertDuAn(DUAN da)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@tenda",SqlDbType.NVarChar, 50) {Value = da.TenDA},
                new SqlParameter("@tiendo ",SqlDbType.Real) {Value = da.TienDo},
                new SqlParameter("@ngaykt",SqlDbType.Date) {Value = da.NgayKT},
                new SqlParameter("@ngaybd",SqlDbType.Date) {Value = da.NgayBD},
                new SqlParameter("@chiphi",SqlDbType.NVarChar, 30) {Value = da.ChiPhi},
                new SqlParameter("@trangthai",SqlDbType.NVarChar, 30) {Value = da.TrangThai},
                new SqlParameter("@mapm",SqlDbType.VarChar, 10) {Value = da.MaPM}
            };
            dbconn.ExecuteProcedure("sp_themDuAn", parameters);
        }
        public void editDuAn(DUAN da)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = da.MaDA},
                new SqlParameter("@tenda",SqlDbType.NVarChar, 50) {Value = da.TenDA},
                new SqlParameter("@tiendo ",SqlDbType.Real) {Value = da.TienDo},
                new SqlParameter("@ngaykt",SqlDbType.Date) {Value = da.NgayKT},
                new SqlParameter("@ngaybd",SqlDbType.Date) {Value = da.NgayBD},
                new SqlParameter("@chiphi",SqlDbType.NVarChar, 30) {Value = da.ChiPhi},
                new SqlParameter("@trangthai",SqlDbType.NVarChar, 30) {Value = da.TrangThai},
                new SqlParameter("@mapm",SqlDbType.VarChar, 10) {Value = da.MaPM}
            };
            dbconn.ExecuteProcedure("sp_capnhatDuAn", parameters);
        }
        public void removeDuAn(DUAN da)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = da.MaDA}
            };
            try
            {
                dbconn.ExecuteProcedure("sp_xoaDuAn", parameters);
            }
            catch (Exception ex)
            {
                SqlException sqlEx = ex.GetBaseException() as SqlException;
                if (sqlEx != null)
                {
                    MessageBox.Show(sqlEx.Message);
                }
                else
                {
                    MessageBox.Show("Lỗi xảy ra khi: \n" + ex.Message);
                }
            }
        }
        public void removeThanhVienDA(NHOM nhom)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = nhom.MaDA},
                new SqlParameter("@tennhom",SqlDbType.NVarChar, 20) {Value = nhom.TenNhom},
                new SqlParameter("@manv",SqlDbType.VarChar, 10) {Value = nhom.MaNV},
            };
            dbconn.ExecuteProcedure("sp_xoaNhanVienDuAn", parameters);
        }
        public void removeNhomDA(TRUONGNHOM tn)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = tn.MaDA},
                new SqlParameter("@tennhom",SqlDbType.NVarChar, 20) {Value = tn.TenNhom}
            };
            dbconn.ExecuteProcedure("sp_xoaNhomDuAn", parameters);
        }
        public double UpdateTienDo(int mada, string magiaidoan)
        {

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("mada",SqlDbType.Int){Value =mada},
                    new SqlParameter("@magiaidoan ",SqlDbType.VarChar,10){Value =magiaidoan},
                    new SqlParameter("@ketqua",SqlDbType.Real){Direction = ParameterDirection.Output}
                };
                dbconn.ExecuteProcedure("sp_TinhTienDoDuAn", parameters);
              double  ketqua = Convert.ToDouble(parameters[2].Value);

            
            return ketqua;
        }
    }
}
