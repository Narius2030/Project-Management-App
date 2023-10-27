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
        GiaiDoanDao gdDao = new GiaiDoanDao();
        CongViecDao cvDao = new CongViecDao();
        NhanVienDao nvienDao = new NhanVienDao();
        public fNhiemVu(string MaNV, int MaDA, string TenNhom)
        {
            InitializeComponent();
            this.MaNV = MaNV;
            this.MaDA = MaDA;
            this.TenNhom = TenNhom;
        }
        public void ReadLoadTimeTask()
        {
            pgbThucTeNV.Value = nvDao.CapNhatTimeTask(this.MaNV);
        }
        public fNhiemVu()
        {

        }
        private void fNhiemVu_Load(object sender, EventArgs e)
        {
            nv.MaCV = this.MaCV;
            nv.MaNV = this.MaNV;
            lblTitleNhiemVu.Text = nvienDao.HoTenNhanVien(this.MaNV);
            txtMaDA.Texts = this.MaDA.ToString();
            txtTenNhom.Texts = this.TenNhom;
            LoadCboGiaiDoan();
            LoadCboCongViec();
            LoadCboTienQuyet();
            LoadGVDSPhanNhiemVu();
            ReadLoadTimeTask();
        }

        private void LoadGVDSPhanNhiemVu()
        {
            gvDSNhiemVu.DataSource = nvDao.DSNhiemVuNhom(this.MaDA, this.MaGiaiDoan, this.MaCV, this.TenNhom);
            gvDSNhiemVu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnPhanNV_Click(object sender, EventArgs e)
        {
            NHIEMVU nv = new NHIEMVU(txtMaNhiemVu.Texts, null, "Pending", null, txtNhiemVu.Texts, Convert.ToInt32(nudThoiGianUocTinh.Value), this.MaNV, Convert.ToInt32(cboCongViec.SelectedValue.ToString()));
            if (cbTienQuyet.Checked)
            {
                nv.MaTienQuyet = cboNhiemVuTienQuyet.SelectedValue.ToString();
            }
            nvDao.ThemNhiemVu(nv);
            ReLoad();
            ReadLoadTimeTask();
        }

        private void LoadCboGiaiDoan()
        {
            DataTable source = gdDao.GetListSprint(this.MaDA,2);
            cboMaGiaiDoan.DisplayMember = "MaGiaiDoan";
            cboMaGiaiDoan.ValueMember = "MaGiaiDoan";
            cboMaGiaiDoan.DataSource = source;
        }

        private void LoadCboCongViec()
        {
            DataTable source = cvDao.GetListJob(this.MaDA, this.MaGiaiDoan);
            cboCongViec.DisplayMember = "TenCV";
            cboCongViec.ValueMember = "MaCV";
            cboCongViec.DataSource = source;
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
            nvDao.SetNullTienQuyet(nv);

            if (nvDao.XoaNhiemVu(nv) == 1)
            {
                ReLoad();
                MessageBox.Show("Xoá Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Xoá Thất Bại", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            ReadLoadTimeTask();
        }

        private void btnTaoNhiemVu_Click(object sender, EventArgs e)
        {
            if (cboCongViec.Text == "--Chưa có công việc--")
            {
                DialogResult dialogResult = MessageBox.Show("Phân công việc trước khi giao nhiệm vụ", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                   
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Chọn công việc khác để phân công","Thông Báo",MessageBoxButtons.RetryCancel,MessageBoxIcon.Warning);
                }
            }
            else
            {
                string maNVSauCung = nvDao.NhiemVuMoiNhat(this.MaDA, this.MaGiaiDoan, this.MaCV, this.TenNhom);
                string MaNVMoi = getMaNhiemVuMoi(maNVSauCung);
                txtMaNhiemVu.Texts = MaNVMoi;
                txtNhiemVu.Texts = "";
            }
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
                numthoigianthucte.Value = 0;
                nv.ThoiGianUocTinh = 0;
                numthoigianthucte.Value = string.IsNullOrEmpty(row.Cells[5].Value.ToString()) ? 0 : Convert.ToInt32(row.Cells[5].Value);
                nv.ThoiGianUocTinh = string.IsNullOrEmpty(row.Cells[4].Value.ToString()) ? 0 : Convert.ToInt32(row.Cells[4].Value);
                nudThoiGianUocTinh.Value = Convert.ToInt32(nv.ThoiGianUocTinh);
                ReadLoadTimeTask();

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
            ClearTextBox();
            LoadCboGiaiDoan();
            LoadCboCongViec();
            LoadCboTienQuyet();
            LoadGVDSPhanNhiemVu();
        }

        private void ClearTextBox()
        {
            txtMaNhiemVu.Texts = "";
            txtNhiemVu.Texts = "";
        }

        private void cboMaGiaiDoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCongViec.Text = "--Chưa có công việc--";
            this.MaGiaiDoan = cboMaGiaiDoan.SelectedValue.ToString();
            LoadCboCongViec();
            ClearTextBox();
            if(cboMaGiaiDoan.SelectedIndex==0)
            {
                btnPhanNV.Enabled = true;
            }
            else
            {
                btnPhanNV.Enabled= false;
            }    
        }

        private void cboCongViec_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MaCV = Convert.ToInt32(cboCongViec.SelectedValue);
            LoadCboTienQuyet();
            ClearTextBox();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form nhom = Application.OpenForms["fNhom"];
            nhom.Controls["pnlShowNhiemVu"].SendToBack();
            this.Close();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string trangthai = "Pending";
            if(nvDao.KiemTraNhiemVuTienQuyet(txtMaNhiemVu.Texts)==1)
            {
                trangthai = "Doing";
            }
            if (Convert.ToInt32(numthoigianthucte.Value) !=0)
            {
                trangthai = "Done";
            }
            NHIEMVU nv = new NHIEMVU()
            {
                ThoiGianLamThucTe = Convert.ToInt32(numthoigianthucte.Value),
                TenNhiemVu = txtNhiemVu.Texts,
                MaNhiemVu = txtMaNhiemVu.Texts,
                TrangThai=trangthai
            };
            if (cbTienQuyet.Checked)
            {
                nv.MaTienQuyet = cboNhiemVuTienQuyet.SelectedValue.ToString();
            }
            if (nvDao.SuaNhiemVu(nv) == 1)
            {
                ReLoad();
                ReadLoadTimeTask();
            }
        }
    }
}
