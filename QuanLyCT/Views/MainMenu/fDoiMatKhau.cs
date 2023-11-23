using QLCongTy.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCongTy.Views.MainMenu
{
    public partial class fDoiMatKhau : Form
    {
        NhanVienDao nvd = new NhanVienDao();
        public fDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnUpdateMatKhau_Click(object sender, EventArgs e)
        {
            pnlDoiMatKhau.Visible = false;
            nvd.DoiMatKhau(fMainMenu.MaNV, txtMatKhauCu.Text, txtMatKhauMoi.Text);
        }
    }
}
