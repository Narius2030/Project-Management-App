using System.Data;
using System.Data.SqlClient;

namespace QLCongTy.DAO
{
    internal class TaiNguyenDao
    {
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        DBConnection dbconn = new DBConnection();

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
