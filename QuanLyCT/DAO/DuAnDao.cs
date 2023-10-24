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
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable getNhanLucDA(int mada)
        {
            string sqlStr = $@"SELECT 
	                            nv.MaNV, CONCAT(nv.HovaTenDem,' ',nv.Ten), n.TenNhom, nv.Email
                            FROM NHANVIEN nv
                            JOIN NHOM n ON n.MaNV = nv.MaNV
                            WHERE n.MaDA = {mada}";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable getNhanLucCty()
        {
            string sqlStr = $@"SELECT
	                            MaNV,  CONCAT(HovaTenDem,' ',Ten), Levels, Email
                            FROM NHANVIEN";
            return dbconn.ExecuteQuery(sqlStr);
        }
    }
}
