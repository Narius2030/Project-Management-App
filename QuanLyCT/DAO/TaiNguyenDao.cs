using System;
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

        public DataTable LoadTNDuAn()
        {

        }

        public DataTable DSTaiNguyen()
        {

        }
    }
}
