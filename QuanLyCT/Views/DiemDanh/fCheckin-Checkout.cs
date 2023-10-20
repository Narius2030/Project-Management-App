using QLCongTy.QLDuAn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCongTy
{
    public partial class fCheckin_Checkout : Form
    {
      
        public fCheckin_Checkout()
        {
            InitializeComponent();
        }

        private void fCheckin_Checkout_Load(object sender, EventArgs e)
        {
            ReLoad();
            txtManvsang.Texts = fMainMenu.MaNV;
            txtMacvsang.Texts = fMainMenu.MaCV;
        }

        public void ReLoad()
        {

        }

        private void btnSubmitSang_Click(object sender, EventArgs e)
        {
           
            //Thông báo
            MessageBox.Show("Đã check in thành công");
            ReLoad();
        }

        private void tpCheckSang_Click(object sender, EventArgs e)
        {

        }
    }
}
