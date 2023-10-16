using QLCongTy.NhanSu;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLCongTy.MainMenu
{
    internal class DangNhapDAO
    {
        DBConnection db = new DBConnection();

        public Nhansu GetInfo(string taiKhoan)
        {
            try
            {
                //Get info staff
                string sqlStr = string.Format("SELECT * FROM NHANSU WHERE MaNV = '{0}'", taiKhoan);
                DataTable dataset = new DataTable();
                dataset = db.FormLoad(sqlStr);
                DataRow tkrow = dataset.Rows[0];

                Nhansu curStaff = new Nhansu(tkrow["MaNV"].ToString(), tkrow["HovaTendem"].ToString(), tkrow["Ten"].ToString(), Convert.ToDateTime(tkrow["NgaySinh"]), tkrow["DiaChi"].ToString(), tkrow["CCCD"].ToString(), tkrow["MaPB"].ToString(), tkrow["MaCV"].ToString(), tkrow["GioiTinh"].ToString(), tkrow["SDT"].ToString(), tkrow["Email"].ToString(), tkrow["TrinhDo"].ToString());
                return curStaff;
            }
            catch
            {
                MessageBox.Show("Tai khoan khong ton tai");
                return null;
            }
        }

        public Tuple<string, string, string> DangNhap(string taiKhoan, string matKhau)
        {
            try
            {
                string sqlStr = string.Format("select taikhoan, MaCV,matkhau from TAIKHOAN where taikhoan = '{0}'", taiKhoan);
                DataTable dataset = new DataTable();
                dataset = db.FormLoad(sqlStr);
                DataRow tkrow = dataset.Rows[0];

                //Get the password from database
                string mk = tkrow["matkhau"].ToString();
                string tk = tkrow["taikhoan"].ToString();
                string cv = tkrow["MaCV"].ToString();

                //Check the valid of mk and tk
                if (mk.Trim() == matKhau.Trim() && tk.Trim() == taiKhoan.Trim())
                {
                    return new Tuple<string, string, string>(taiKhoan, matKhau, cv);
                }
                else
                {
                    MessageBox.Show("Sai tai khoan hoac mat khau");
                    return new Tuple<string, string, string>(null, null, null);;
                }
            }
            catch
            {
                MessageBox.Show("Tai khoan khong ton tai");
                return  new Tuple<string, string, string>(null, null, null);
            }
        }
        public void DoiMatKhau(string taiKhoan,string matKhauMoi,string matKhauNhapLai)
        {
            if (matKhauMoi == matKhauNhapLai)
            {
                string sqlString = string.Format("UPDATE TAIKHOAN SET matkhau = '{0}' WHERE taikhoan = '{1}'",matKhauMoi, taiKhoan);
                db.ThucThi(sqlString);
                MessageBox.Show("Doi mat khau thanh cong");
            }
            else
            {
                MessageBox.Show("Mat khau nhap lai khong khop");
            }
        }              
    }
}
