using QLCongTy.DAO;
using QLCongTy.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCongTy.Views.NhanSu
{
    public partial class fNhiemVu : Form
    {
        private string MaNV;
        private int MaDA;
        private string MaGiaiDoan;
        private int MaCV;
        private string TenNhom;
        NhomDao nDao = new NhomDao();
        NhiemVuDao nvDao = new NhiemVuDao();
        public fNhiemVu(string MaNV, int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            InitializeComponent();
            this.MaNV = MaNV;
            this.MaDA = MaDA;
            this.MaGiaiDoan = MaGiaiDoan;
            this.MaCV = MaCV;
            this.TenNhom = TenNhom;
        }

        public fNhiemVu()
        {

        }

        private void fNhiemVu_Load(object sender, EventArgs e)
        {
            txtMaDA.Texts = this.MaDA.ToString();

            //cboMaDA.Text = ;
            //cboMaGiaiDoan.Text = ;
            LoadGVDSPhanNhiemVu();
        }

        private void LoadGVDSPhanNhiemVu()
        {
            gvDSNhiemVu.DataSource = nvDao.DSNhiemVuNhom(8, "01DA08", 1, this.TenNhom);
            gvDSNhiemVu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnPhanCV_Click(object sender, EventArgs e)
        {

            NHIEMVU nv = new NHIEMVU(txtMaNhiemVu.Texts, cboNhiemVuTienQuyet.SelectedValue.ToString(), "Pending", Convert.ToInt32(nudThoiGianUocTinh.Value), txtNhiemVu.Texts, null, ltlTitleNhiemVu.Text.Substring(0, 5), Convert.ToInt32(txtCongViec.Texts));
            nvDao.ThemNhiemVu(nv);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            //Xử lý tạo một mã nhiệm vụ mới từ mã nhiệm vụ mới nhất
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //Delete NhiemVU 
            //KiemTra có phải tiên quyết của cải khác kh
            //Nếu tiên quyết thì set tiên quyết = null trước
        }
    }
}
