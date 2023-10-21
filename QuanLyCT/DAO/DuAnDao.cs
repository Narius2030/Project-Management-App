using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCongTy.DAO
{
    public class DuAnDao
    {
        DBConnection dbconn = new DBConnection();
        public DataTable getProjectList()
        {
            string sqlStr = "SELECT * FROM DUAN";
            return dbconn.FormLoad(sqlStr);
        }
    }
}
