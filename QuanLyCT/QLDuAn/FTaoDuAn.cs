using System;
using System.Linq;
using System.Windows.Forms;

namespace QLCongTy.QLDuAn
{
    public partial class FTaoDuAn : Form
    {
        DuAnDAO dad = new DuAnDAO();
        private string chedo;
        private DuAn da = new DuAn();
        public FTaoDuAn(DuAn da, string chedo)
        {
            InitializeComponent();
            this.chedo = chedo;
            this.da = da;
        }

        private void TaoDuAn_Load(object sender, EventArgs e)
        {
            lblTitle.Text = chedo;
            if (chedo.ToUpper() == "CẬP NHẬT")
            {
                DoDLTextBox();
            }
            GetCboPhongBan();
            GetCboMaTruongDA();
        }

        private void GetCboMaTruongDA()
        {
            cboMaTruongDA.DataSource = dad.GetNameDeptHead();
            cboMaTruongDA.DisplayMember = "TenNV";
            cboMaTruongDA.ValueMember = "MaNV";
        }

        public void GetCboPhongBan()
        {
            cboMaPB.DataSource = dad.GetNameDept();
            cboMaPB.DisplayMember = "TenPB";
            cboMaPB.ValueMember = "MaPB";
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            DuAn da = new DuAn(txtMaDA.Texts, txtTenDA.Texts, cboMaPB.SelectedValue.ToString(), cboMaTruongDA.SelectedValue.ToString(), dtpNgayBatDau.Value.Date, dtpNgayKetThuc.Value.Date, cboTrangThai.Text);
            try
            {
                if (chedo.ToUpper() == "THÊM")
                {
                    dad.Them(da);
                }
                else
                {
                    dad.Sua(da);
                }
                MessageBox.Show("Thao tác thành công", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            foreach (var control in pnlTTDuAn.Controls.OfType<TextBox>())
            {
                control.Clear();
            }
        }
        public void DoDLTextBox()
        {
            Type type = da.GetType();
            var props = type.GetProperties();
            int i = 0;
            foreach (var control in pnlTTDuAn.Controls.OfType<CTTextBox>())
            {
                control.Texts = props[i].GetValue(da, null).ToString();
            }
        }
    }
}
