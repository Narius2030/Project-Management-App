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
using System.Windows.Media.Media3D;

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
        NHIEMVU nv = new NHIEMVU();
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
            nv.MaCV = this.MaCV;
            nv.MaNV = this.MaNV;
            lblTitleNhiemVu.Text = this.MaNV;
            txtMaDA.Texts = this.MaDA.ToString();
            txtMaGiaiDoan.Texts = this.MaGiaiDoan;
            txtTenNhom.Texts = this.TenNhom;
            txtCongViec.Texts = this.MaCV.ToString();
            LoadCboTienQuyet();
            LoadGVDSPhanNhiemVu();
        }

        private void LoadGVDSPhanNhiemVu()
        {
            gvDSNhiemVu.DataSource = nvDao.DSNhiemVuNhom(this.MaDA, this.MaGiaiDoan, this.MaCV, this.TenNhom);
            gvDSNhiemVu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnPhanCV_Click(object sender, EventArgs e)
        {
            NHIEMVU nv = new NHIEMVU(txtMaNhiemVu.Texts, null, "Pending", null, txtNhiemVu.Texts, Convert.ToInt32(nudThoiGianUocTinh.Value), this.MaNV, Convert.ToInt32(txtCongViec.Texts));
            if (cbTienQuyet.Checked)
            {
                nv.MaTienQuyet = cboNhiemVuTienQuyet.SelectedValue.ToString();
            }
            nvDao.ThemNhiemVu(nv);
            ReLoad();
        }

        private void LoadCboTienQuyet()
        {
            DataTable source = nvDao.DSNhiemVu(this.MaDA, this.MaGiaiDoan, this.MaCV, this.TenNhom);
            cboNhiemVuTienQuyet.DisplayMember = "NhiemVu";
            cboNhiemVuTienQuyet.ValueMember = "MaNhiemVu";
            cboNhiemVuTienQuyet.DataSource = source;
        }

        private void btnXoaNhiemVu_Click(object sender, EventArgs e)
        {
            //Procedure chưa setnull được và hiện một exception nhưng kh ảnh hưởng quá trình xóa
            //nvDao.SetNullTienQuyet(nv);
            nvDao.XoaNhiemVu(nv);
            ReLoad();
        }

        private void btnTaoNhiemVu_Click(object sender, EventArgs e)
        {
            string maNVSauCung = nvDao.NhiemVuMoiNhat(this.MaDA, this.MaGiaiDoan, this.MaCV, this.TenNhom);
            string MaNVMoi = getMaNhiemVuMoi(maNVSauCung);
            txtMaNhiemVu.Texts = MaNVMoi;
            txtNhiemVu.Texts = "";
        }

        public String getMaNhiemVuMoi(String MaNhiemVu)
        {
            int id = Int32.Parse(MaNhiemVu.Substring(0, 2));
            id++;
            string prefix = id.ToString("D2");
            string suffix = MaNhiemVu.Substring(2);
            return prefix + suffix;
        }

        private void gvDSNhiemVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = gvDSNhiemVu.Rows[e.RowIndex];
                txtMaNhiemVu.Texts = row.Cells[0].Value.ToString();
                nv.MaNhiemVu = row.Cells[0].Value.ToString();
                txtNhiemVu.Texts = row.Cells[1].Value.ToString();
                nv.TenNhiemVu = row.Cells[1].Value.ToString();
                nv.TrangThai = row.Cells[2].Value.ToString();
                nv.MaTienQuyet = row.Cells[3].Value.ToString();
                nv.ThoiGianUocTinh = Convert.ToInt32(row.Cells[4].Value);
                //nv.ThoiGianLamThucTe = Convert.ToInt32(row.Cells[5].Value);
            }
        }

        private void cbTienQuyet_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTienQuyet.Checked)
            {
                cboNhiemVuTienQuyet.Enabled = true;
            }
            else
            {
                cboNhiemVuTienQuyet.Enabled = false;
            }
        }

        private void ReLoad()
        {
            txtMaNhiemVu.Texts = "";
            txtNhiemVu.Texts = "";
            LoadCboTienQuyet();
            LoadGVDSPhanNhiemVu();
        }
    }
}
