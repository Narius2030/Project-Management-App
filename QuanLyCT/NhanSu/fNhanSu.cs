using QLCongTy.ChamCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace QLCongTy.NhanSu
{
    public partial class FNhanSu : Form
    {
        Nhansu ns = new Nhansu();
        NhanSuDAO nsDao = new NhanSuDAO();
        public FNhanSu()
        {
            InitializeComponent();
        }

        private void FNhanSu_Load(object sender, EventArgs e)
        {
            gvNhanSu.DataSource = nsDao.DanhSach();
            DoiTenGV();
            GetCboPB();
            GetCboCV();

            //Thống kê
            ThongKeLuong();
            ThongKeCLNL();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            gvNhanSu.DataSource = nsDao.DanhSach();
            cboChucVu.Text = "";
            cboGioiTinh.Text = "";
            cboPhongBan.Text = "";
        }

        #region Tương tác với Datagridview

        private void DoiTenGV()
        {
            string[] thuoctinh = { "Mã Nhân Viên", "Họ Đệm", "Tên", "Ngày Sinh", "Địa Chỉ", "CCCD", "Mã Phòng Ban", "Giới Tính", "SĐT", "Email", "MaCV" };
            for (int i = 0; i < thuoctinh.Length; i++)
            {
                gvNhanSu.Columns[i].HeaderText = thuoctinh[i];
            }
        }
        #endregion

        #region Các chức năng phổ biến
        private void cboPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = cboPhongBan.Text.ToString().Substring(0, 4);
            gvNhanSu.DataSource = nsDao.Loc("MaPB", value);
        }

        private void cboChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvNhanSu.DataSource = nsDao.Loc("MaCV", cboChucVu.Text);
        }

        private void cboGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvNhanSu.DataSource = nsDao.Loc("GioiTinh", cboGioiTinh.Text);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ns = new Nhansu(txtMaNV.Texts, txtHoDem.Texts, txtTenNV.Texts, dtpNgaySinh.Value.Date, txtDiaChi.Texts, txtCCCD.Texts, cboPB.SelectedValue.ToString(), cboCV.SelectedValue.ToString(), cboGTinh.Text, txtSDT.Texts, txtEmail.Texts, cboTrinhdo.Text);
            nsDao.Them(ns);

            gvNhanSu.DataSource = nsDao.DanhSach();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ns = new Nhansu(txtMaNV.Texts, txtHoDem.Texts, txtTenNV.Texts, dtpNgaySinh.Value.Date, txtDiaChi.Texts, txtCCCD.Texts, cboPB.SelectedValue.ToString(), cboCV.SelectedValue.ToString(), cboGTinh.Text, txtSDT.Texts, txtEmail.Texts, cboTrinhdo.Text);
            nsDao.Xoa(ns);
            gvNhanSu.DataSource = nsDao.DanhSach();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ns = new Nhansu(txtMaNV.Texts, txtHoDem.Texts, txtTenNV.Texts, dtpNgaySinh.Value.Date, txtDiaChi.Texts, txtCCCD.Texts, cboPB.SelectedValue.ToString(), cboCV.SelectedValue.ToString(), cboGTinh.Text, txtSDT.Texts, txtEmail.Texts, cboTrinhdo.Text);
            nsDao.Sua(ns);
            gvNhanSu.DataSource = nsDao.DanhSach();
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
            var listPB = nsDao.LaySLPhong();
            var listNam = nsDao.LaySLNam();
            
            //Thêm từng cột vào bar-chart
            for (int i = 0; i < listNam.Count; i++)
            {
                for (int j = 0; j < listPB.Count; j++)
                {
                    chartLayLuongThang.Series[j].Points.AddXY(listNam[i], nsDao.LuongTheoThang(listPB[j], listNam[i]));
                }
            }
        }

        //Vẽ biểu đồ thống kế tỉ lệ trình độ trong công ty (tròn)
        public void ThongKeCLNL()
        {
            var clnlList = nsDao.LaySLTrinhDo();
            //Thêm từng phần vào pie-chart
            for (int i = 1; i < clnlList.Count; i++)
            {
                chartCLNL.Series[0].Points.AddXY(clnlList[i].Key, clnlList[i].Value);
            }
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
            txtTenNV.Texts = r.Cells[2].Value.ToString();
            dtpNgaySinh.Text = r.Cells[3].Value.ToString();
            txtDiaChi.Texts = r.Cells[4].Value.ToString();
            txtCCCD.Texts = r.Cells[5].Value.ToString();
            cboPB.Text = r.Cells[6].Value.ToString();
            cboGTinh.Text = r.Cells[7].Value.ToString();
            txtSDT.Texts = r.Cells[8].Value.ToString();
            txtEmail.Texts = r.Cells[9].Value.ToString();
            cboCV.Text = r.Cells[10].Value.ToString();
            cboTrinhdo.Text = r.Cells[11].Value.ToString();
        }

        private void GetCboPB()
        {
            cboPB.DataSource = nsDao.GetNameDept();
            cboPB.DisplayMember = "TenPB";
            cboPB.ValueMember = "MaPB";
        }

        private void GetCboCV()
        {
            cboCV.DataSource = nsDao.GetChucVu();
            cboCV.DisplayMember = "TenCV";
            cboCV.ValueMember = "MaCV";
        }
    }
}
