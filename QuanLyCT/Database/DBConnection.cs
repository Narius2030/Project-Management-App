using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCongTy
{
   public class DBConnection
    {
        private SqlConnection conn=new SqlConnection ($"Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLDA;User ID=NV002;Password=nv002");
        public DBConnection()
        {

        }
        public DBConnection(string userid,string pass)
        {
            Conn=new SqlConnection($"Data Source=(localdb)\\mssqllocaldb;Initial Catalog=QLDA;User ID={userid};Password={pass}");
        }

        public SqlConnection Conn { get => conn; set => conn = value; }

        public void ExecuteCommand(string sqlStr) // Tham số là một chuỗi câu lệnh SQL
        {
            try
            {
                Conn.Open();    //Mở kết nối đến SQL Server
                SqlCommand cmd = new SqlCommand(sqlStr, Conn); // Tạo đối tượng câu lệnh và truyền vào constructor là chuỗi lệnh và biến kết nối (conn)
                cmd.ExecuteNonQuery();  //Lệnh thực thi, phương thức ExecuteNonQuery không có kết quả trả về là một bảng nhưng trả về số dòng bị ảnh hưởng
            }
            catch (SqlException exc)
            {
                MessageBox.Show("Thuc thi that bai\n" + exc.Message); // Bắt exception trong quá trình thực thi
            }
            finally
            {
                Conn.Close(); // Đóng kết nối
            }
        }
        public DataTable ExecuteProcedure(string procedureName, SqlParameter[] parameters)// Tham số đầu vào là 1 chuỗi lệnh thực thi procedure ,
                                                                                          // biến parameters là mảng các tham số input và output đầu vào kiểu SqlParameter
        {
            SqlCommand cmd = new SqlCommand(procedureName, Conn);   // Truyền tham số procedure và conn vào contructor SqlCommand

            cmd.CommandType = CommandType.StoredProcedure;//chọn hàm thực thi trong lớp CommandType ở đây là thực thi procedure 
            if (parameters != null)// nếu tồn tại tham số 
            {
                foreach (SqlParameter parameter in parameters)// lặp qua các tham số 
                {
                    cmd.Parameters.Add(parameter);// đưa nó vào SqlCommand
                }
            }
            DataTable resultTable = new DataTable();//tạo 1 biến để lưu trữ kết quả kiểu DataTable
            try
            {
                Conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();//sử dụng phương thưc ExecuteReader trả ra kiểu SqlDataReader
                {
                    resultTable.Load(reader);// đưa dữ liệu vào biến resultTable
                }
            }
            catch (SqlException exc)// nếu có lỗi thì sẽ bắt lỗi và trả về null
            {
                MessageBox.Show("Thực thi thất bại\n" + exc.Message, "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);// bắt lỗi
                return null;
            }
            finally
            {
                Conn.Close();//đóng kết nối 
            }

            return resultTable;//trả về kết quả
        }
    
        public DataTable ExecuteQuery(string sqlStr) // Hàm thực thi câu lệnh sql và trả về một bảng, đầu vào là chuỗi câu lệnh SQL
        {
            DataTable dataSet = new DataTable(); // Tạo biến trả về kiểu DataTable
            try
            {
                Conn.Open();    // Mở kết nối SQL Server
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, Conn);  // Sử dụng SqlDataAdapter tạo câu lệnh thực thi SQL 
                adapter.Fill(dataSet);  // Điền kết quả từ việc thực thi vào biến dataSet
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);   // Message Box hiện lỗi
                return null;
            }
            finally
            {
                Conn.Close();   // Đóng kết nối
            }
            return dataSet; // Trả về kết quả là một DataTable
        }
        public object ExecuteScalar(string sqlStr) //Hàm trả về một giá trị đối tượng object được trích xuất từ một ô (Scalar)
        {
            object result = new object();   //Tạo đối tượng lưu giá trị đầu ra kiểu object
            try
            {
                Conn.Open();    // Mở kết nối đến SQL Server
                SqlCommand command = new SqlCommand(sqlStr, Conn);  // Tạo câu lệnh SQL
                result = command.ExecuteScalar();   //Thực thi câu lệnh và gán kết quả bằng biến result
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);   //Bắt exception và show exception
            }
            finally
            {
                Conn.Close();   // Đóng kết nối
            }   
            return result;
        }
    }
}
