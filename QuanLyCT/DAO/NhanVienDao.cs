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
            string sqlStr = "SELECT * FROM NHANVIEN";
            return Dbc.ExecuteQuery(sqlStr);
        }
    }
}
