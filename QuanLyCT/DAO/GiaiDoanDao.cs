using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace QLCongTy.DAO
{
    public  class GiaiDoanDao
    {
         DBConnection dbC=new DBConnection();
         public DataTable GetListSprint()
        {
            return dbC.ExecuteQuery("Select *From GiaiDoan");
        }
    }
}
