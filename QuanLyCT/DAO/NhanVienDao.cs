using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCongTy.DTO;
namespace QLCongTy.DAO
{
    public class NhanVienDao
    {
        DBConnection Dbc = new DBConnection();
        //public DataTable CheckTaiKhoan(NHANVIEN nv )
        //{
        //    string sql = string.Format("declare @var int  \n " +
        //        "exec sp_ktrDangNhap " +
        //        "'{0}' ,'{1}'," +
        //        "@check=@var output\r\n" +
        //        "print @var", nv.MaTaiKhoan,nv.MatKhau);
        //    return Dbc.FormLoad(sql);
        //}
        public int CheckTaiKhoan(NHANVIEN nv) 
        {
            int check = 0;

            using (SqlConnection conn = Dbc.conn)
            using (SqlCommand cmd = new SqlCommand("sp_ktrDangNhap", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@matk", SqlDbType.NVarChar, 20).Value = nv.MaTaiKhoan;
                cmd.Parameters.Add("@matkhau", SqlDbType.NVarChar, 20).Value = nv.MatKhau;

                cmd.Parameters.Add("@check", SqlDbType.Int).Direction = ParameterDirection.Output;

                conn.Open();
                cmd.ExecuteNonQuery();

                check = Convert.ToInt32(cmd.Parameters["@check"].Value.ToString());
            }
            return check;
        }

    }
}
