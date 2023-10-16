using QLCongTy.ChamCong;
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
        CheckInOutDAO ciod = new CheckInOutDAO();
        CheckInOut cio = new CheckInOut();
        DuAnDAO dad = new DuAnDAO();
        PCNhanLuc pc = null;
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
            cio.MaNV = txtManvsang.Texts;
            cio.Macv = txtMacvsang.Texts;
            cio.Ngay = dtpCheckIn.Value.Date;
            ciod.SubmitSang(cio);
            //Thông báo
            MessageBox.Show("Đã check in thành công");
            ReLoad();
        }
    }
}
