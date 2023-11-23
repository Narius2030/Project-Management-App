using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCongTy.DAO
{
    internal class UocLuongDao
    {
        DBConnection dbconn = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);
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
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int){Value = MaDA},
                new SqlParameter("@giaidoan ",SqlDbType.VarChar,20){Value =MaGiaiDoan},
                new SqlParameter("@manv",SqlDbType.VarChar, 10) {Value = MaNV},
                new SqlParameter("@timetask",SqlDbType.Int) {Value = timeTask}
            };
            try
            {
                dbconn.ExecuteProcedure("sp_capnhatTimeTask", parameters);
            }
            catch(Exception ex)
            {
                SqlException sqlEx = ex.GetBaseException() as SqlException;
                if (sqlEx != null)
                {
                    MessageBox.Show("Lỗi SQL xảy ra: " + sqlEx.Message);
                    // Xử lý lỗi SQL cụ thể tại đây
                }
                else
                {
                    MessageBox.Show("Lỗi xảy ra khi: " + ex.Message);
                }
            }
        }

        public void CapNhatTimeSprint()
        {
            dbconn.ExecuteProcedure("sp_UpdateTimeSprintTheoNgay", null);
        }
    }
}
