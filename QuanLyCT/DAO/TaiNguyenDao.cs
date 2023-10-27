using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string sqlStr = $"INSERT INTO CAP VALUES ({MaDA}, '{MaTNguyen}')";
            dbconn.ExecuteQuery(sqlStr);
        }
    }
}
