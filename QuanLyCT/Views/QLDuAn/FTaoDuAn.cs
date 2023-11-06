using QLCongTy.DAO;
using QLCongTy.DTO;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QLCongTy.QLDuAn
{
    public partial class FTaoDuAn : Form
    {
        DuAnDao daDao = new DuAnDao();
        DUAN da = new DUAN();
        private string mode = "";
        public FTaoDuAn(DUAN da, string mode)
        {
            InitializeComponent();
            this.mode = mode;
            this.da = da;
        }
        private void TaoDuAn_Load(object sender, EventArgs e)
        {
           lblTitle.Text = mode;
           if(mode.ToUpper() == "CẬP NHẬT")
           {
                DoDLTextBox();
           } 
        }
        private void GetCboMaTruongDA()
        {
           
        }

        public void GetCboPhongBan()
        {
           
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode.ToUpper() == "THÊM")
                {
                    DUAN daNew = new DUAN() { MaDA=da.MaDA, TenDA=txtTenDA.Texts, TienDo=0, NgayKT=dtpNgayKetThuc.Value, NgayBD=dtpNgayBatDau.Value, ChiPhi=txtChiPhi.Texts, TrangThai="Pending", MaPM=cboMaTruongDA.Text };
                    daDao.insertDuAn(daNew);
                }
                else
                {
                    MessageBox.Show(txtChiPhi.Texts);
                    DUAN daNew = new DUAN() { MaDA = da.MaDA, TenDA = txtTenDA.Texts, TienDo = int.Parse(txtTienDoDA.Texts), NgayKT = dtpNgayKetThuc.Value, NgayBD = dtpNgayBatDau.Value, ChiPhi = txtChiPhi.Texts, TrangThai = txtTrangThai.Texts, MaPM = cboMaTruongDA.Text };
                    daDao.editDuAn(daNew);
                }
                MessageBox.Show("Thao tác thành công");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            
        }
        public void DoDLTextBox()
        {
            txtTenDA.Texts = da.TenDA;
            txtChiPhi.Texts = da.ChiPhi;
            txtTrangThai.Texts = da.TrangThai;
            txtTienDoDA.Texts = da.TienDo.ToString();
            cboMaTruongDA.Text = da.MaPM;
            dtpNgayBatDau.Value = (DateTime)da.NgayBD;
            dtpNgayKetThuc.Value = (DateTime)da.NgayKT;
        }
    }
}
