using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using QLCongTy.DTO;
using System.Windows;
using System.Data.SqlClient;

namespace QLCongTy.DAO
{
    internal class UocLuongDao
    {
        DBConnection dbconn = new DBConnection();
        NhiemVuDao nvDao = new NhiemVuDao();
        public int GetTimeSprint(int mada, string manhanvien)
        {
            string sqlStr = $"SELECT TimeSprint FROM UOCLUONG WHERE MaDA = {mada} AND MaNV = '{manhanvien}'";
            DataTable result = dbconn.ExecuteQuery(sqlStr);
            if (result.Rows.Count > 0)
            {
                return Convert.ToInt32(result.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        public void CapNhatTimeTask(int MaDA, string MaGiaiDoan, string MaNV, int timeTask)
        {
            //string sqlStr = $"UPDATE UOCLUONG SET TimeTasks = {timeTask} WHERE MaNV = '{MaNV}' AND MaDA = {MaDA} AND MaGiaiDoan = '{MaGiaiDoan}'";
            //dbconn.ExecuteQuery(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int){Value = MaDA},
                new SqlParameter("@giaidoan ",SqlDbType.VarChar,20){Value =MaGiaiDoan},
                new SqlParameter("@manv",SqlDbType.VarChar, 10) {Value = MaNV},
                new SqlParameter("@timetask",SqlDbType.Int) {Value = timeTask}
            };
            dbconn.ExecuteProcedure("sp_capnhatTimeTask", parameters);
        }
    }
}
