using QLCongTy.DAO;
using QLCongTy.DTO;
using System;
using System.Data;
using System.Data.Entity.Migrations.Infrastructure;
using System.Windows.Forms;

namespace QLCongTy.NhanSu
{
    public partial class FNhanSu : Form
    {
       NhanVienDao nvDao = new NhanVienDao();
        public FNhanSu()
        {
            InitializeComponent();
        }

        private void FNhanSu_Load(object sender, EventArgs e)
        {
            LoadGVNhanSu();
            DoiTenGV();
            GetCboLevels();
            GetCboChucVu();

            ThongKeLuong();
            ThongKeCLNL();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            cboChucVu.Text = string.Empty;
            cboLevels.Text = string.Empty;
            LoadGVNhanSu();
        }

        #region Tương tác với Datagridview

        private void DoiTenGV()
        {
            
        }

        private void LoadGVNhanSu()
        {
            gvNhanSu.DataSource = nvDao.DSNhanVien();
        }
        #endregion

        #region Nut chuc nang
        private void btnThem_Click(object sender, EventArgs e)
        {
            NHANVIEN nv = new NHANVIEN(txtMaNV.Texts, txtHoDem.Texts, txtTen.Texts, txtChucVu.Texts, txtEmail.Texts, txtLevels.Texts, txtDiaChi.Texts, txtSdt.Texts, txtMaNV.Texts, txtMaNV.Texts.ToLower());
            nvDao.ThemNhanVien(nv);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
          
        }
        #endregion

        #region Chức năng nâng cao
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            tmDangKy.Stop();
            pnlDangKy.Size = pnlDangKy.MinimumSize;
            tmThongKe.Start();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            tmThongKe.Stop();
            pnlThongKe.Size = pnlThongKe.MinimumSize;
            tmDangKy.Start();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            tmSidebar.Start();
        }
        #endregion

        #region Thống kê

        //Thống kế số lương của từng phòng theo năm
        public void ThongKeLuong()
        {
            
        }

        //Vẽ biểu đồ thống kế tỉ lệ trình độ trong công ty (tròn)
        public void ThongKeCLNL()
        {
          
        }
        #endregion

        #region Sidebar
        bool sidebarExpand;
        private void tmSidebar_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                pnlTool.Width -= 10;
                if (pnlTool.Width == pnlTool.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    tmSidebar.Stop();
                }
            }
            else
            {
                pnlTool.Width += 10;
                if (pnlTool.Width == pnlTool.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    tmSidebar.Stop();
                }
            }
        }

        private void tmThongKe_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                pnlThongKe.Width -= 50;
                if (pnlThongKe.Width == pnlThongKe.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    tmThongKe.Stop();
                }
            }
            else
            {
                pnlThongKe.Width += 50;
                if (pnlThongKe.Width == pnlThongKe.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    tmThongKe.Stop();
                }
            }
        }

        private void tmDangKy_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                pnlDangKy.Width -= 50;
                if (pnlDangKy.Width == pnlDangKy.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    tmDangKy.Stop();
                }
            }
            else
            {
                pnlDangKy.Width += 50;
                if (pnlDangKy.Width == pnlDangKy.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    tmDangKy.Stop();
                }
            }
        }

        #endregion

        private void gvNhanSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = gvNhanSu.SelectedRows[0];
            txtMaNV.Texts = r.Cells[0].Value.ToString();
            txtHoDem.Texts = r.Cells[1].Value.ToString();
        }

        private void GetCboLevels()
        {
            DataTable source = nvDao.GetDSLevels();
            cboLevels.DisplayMember = "Levels";
            cboLevels.ValueMember = "Levels";
            cboLevels.DataSource = source;
        }

        private void GetCboChucVu()
        {
            DataTable source = nvDao.GetDSChucVu();
            cboChucVu.DisplayMember = "ChucVu";
            cboChucVu.ValueMember = "ChucVu";
            cboChucVu.DataSource = source;
        }
        //Ham loi, chua loc duoc
        private void cboLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvNhanSu.DataSource = nvDao.LocLevels(cboLevels.SelectedItem.ToString());
        }

        //Ham loi, chua loc duoc
        private void cboChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvNhanSu.DataSource = nvDao.LocChucVu(cboChucVu.SelectedItem.ToString());
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            nvDao.XoaNhanVien(txtMaNV.Texts);
            MessageBox.Show("Xóa thảnh công");
        }
    }
}
