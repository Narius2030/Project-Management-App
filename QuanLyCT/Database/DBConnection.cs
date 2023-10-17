using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCongTy
{
    internal class DBConnection
    {
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        public void ThucThi(string sqlStr)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Thuc thi that bai\n" + exc.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public DataTable FormLoad(string sqlStr)
        {
            DataTable dataSet = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                adapter.Fill(dataSet);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
            return dataSet;
        }
        public object GetItem(string sqlStr)
        {
            object result = new object();
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                result = command.ExecuteScalar();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
