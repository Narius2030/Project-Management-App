using QLCongTy.DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCongTy.DAO
{
    public class TruongNhomDao
    {
        DBConnection dbconn = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);
        public DataTable laydanhsachnhomtruong(int mada)
        {
            string sqlStr = $"SELECT MaNV, TenNhom FROM TRUONGNHOM WHERE MaDA = {mada} ";
            return dbconn.ExecuteQuery(sqlStr);
        }

        public void xoaTruongNhom(NHOM nhom)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@mada",SqlDbType.Int) {Value = nhom.MaDA},
                    new SqlParameter("@tennhom",SqlDbType.NVarChar, 20) {Value = nhom.TenNhom},
                    new SqlParameter("@matn",SqlDbType.VarChar, 20) {Value = nhom.MaNV}
                };
                dbconn.ExecuteProcedure("sp_xoaTruongNhomDuAn", parameters);

                //parameters = new SqlParameter[]
                //{
                //    new SqlParameter("@manv",SqlDbType.NVarChar, 20) {Value = nhom.MaNV}
                //};
                //dbconn.ExecuteProcedure("sp_RemoveRoleTEAMLEAD", parameters);
            }
            catch (Exception ex) {
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

        public DataTable timTruongNhom(NHOM nhom)
        {
            return dbconn.ExecuteQuery($"select * From dbo.sfn_TimTruongNhom('{nhom.TenNhom}',{nhom.MaDA})");
        }

        public void DoiTruongNhom(string MaTruongNhomMoi, NHOM nhom)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = nhom.MaDA},
                new SqlParameter("@tennhom",SqlDbType.NVarChar, 20) {Value = nhom.TenNhom},
                new SqlParameter("@truongnhommoi",SqlDbType.VarChar, 10) {Value = MaTruongNhomMoi}
            };
            dbconn.ExecuteProcedure("sp_doiTruongNhomDuAn", parameters);

        }
    }
}
