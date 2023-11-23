using System.Data;
using System.Data.SqlClient;

namespace QLCongTy.DAO
{
    internal class TaiNguyenDao
    {
        DBConnection dbconn = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);

        public DataTable LoadTNDuAn(int MaDA)
        {
            string sqlStr = $"SELECT * FROM CAP WHERE MaDA = '{MaDA}'";
            return dbconn.ExecuteQuery(sqlStr);
        }

        public DataTable DSTaiNguyen()
        {
            string sqlStr = "SELECT * FROM TAINGUYEN";
            return dbconn.ExecuteQuery(sqlStr);
        }

        public void ThemTaiNguyen(int MaDA, string MaTNguyen)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int){Value = MaDA},
                new SqlParameter("@matn ",SqlDbType.VarChar, 10) {Value = MaTNguyen},
            };
            dbconn.ExecuteProcedure("sp_themTaiNguyen", parameters);
        }
    }
}
