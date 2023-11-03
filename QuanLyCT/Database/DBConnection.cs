using QLCongTy.DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCongTy
{
    internal class DBConnection
    {
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
<<<<<<< HEAD
        

=======
>>>>>>> d0a845557af48923a7114dddfc282bb9c463914c

        public void ExecuteCommand(string sqlStr)
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
        public DataTable ExecuteProcedure(string procedureName, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(procedureName, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            DataTable resultTable = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    resultTable.Load(reader);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Thực thi thất bại\n" + exc.Message, "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                conn.Close();
            }

            return resultTable;
        }
    
        public DataTable ExecuteQuery(string sqlStr)
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
        public object ExecuteScalar(string sqlStr)
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
