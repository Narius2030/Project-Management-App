using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLCongTy
{
    public partial class FProfile : Form
    {
      
        bool sidebarExpand;
        Panel currentPanel = new Panel();
        public FProfile()
        {
            InitializeComponent();
            CustomizeDesing();
        }
        private void FProfile_Load(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void LoadProfile()
        {
           
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ///set timer interval to lowest to make it smoother
            sidebarTimer.Start();
        }

        private void btnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            HidePanel();

            currentPanel = pnlTTCN;
            ShowTTCN();

            ShowPanel();
        }

        private void btnDuAn_Click(object sender, EventArgs e)
        {
            HidePanel();

            currentPanel = pnlDuAn;

            ShowPanel();
        }

        private void btnLuong_Click(object sender, EventArgs e)
        {
            HidePanel();

            currentPanel = pnlLuong;
            ShowLuong(DateTime.Now.Year, DateTime.Now.Month);

            ShowPanel();
        }

        private void btnXinNghiPhep_Click(object sender, EventArgs e)
        {
            HidePanel();
            currentPanel = pnlXinNghiPhep;
            ShowPanel();
        }

        private void btnBaoMat_Click(object sender, EventArgs e)
        {
            HidePanel();
            currentPanel = pnlBaoMat;
            ShowPanel();
        }
        private void btnUpdateMatKhau_Click(object sender, EventArgs e)
        {
            //pnlDoiMatKhau.Visible = false;
            //pfd.DoiMatKhau(fMainMenu.currentStaff, txtMatKhauCu.Text, txtMatKhauMoi.Text);
            //lblMatKhau.Text = pfd.GetMatKhau(fMainMenu.currentStaff);
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            pnlDoiMatKhau.Visible = true;
        }

        private void ShowLuong(int nam, int thang)
        {
            //List<float> luong;
            //try
            //{
            //    luong = pfd.LayHoaDonLuong(fMainMenu.currentStaff.MaNV, thang, nam);
            //}
            //catch
            //{
            //    MessageBox.Show("Lương tháng này hiện chưa có");
            //    luong = Enumerable.Repeat<float>(0, 9).ToList();
            //}
            //int i = 0;
            //foreach (var control in pnlThongtinluong.Controls.OfType<CTTextBox>())
            //{
            //    control.Texts = luong[i].ToString();
            //    i++;
            //}
        }

        public void ShowTTCN()
        {
            //gvThongbao.DataSource = pfd.LayDSXinNghi(fMainMenu.currentStaff.MaNV);
            //var info = pfd.LayThonTinCN();
            //int i = 0;
            //foreach (var control in pnlThongTinCN.Controls.OfType<VBLabel>())
            //{
            //    control.Text = info[i];
            //    i++;
            //}
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNam.Text != "Năm")
            {
                ShowLuong(Convert.ToInt32(cboNam.Text), Convert.ToInt32(cboThang.Text));
            }
            else
            {
                MessageBox.Show("Vui lòng nhập năm");
            }
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            //chartLuongThangNV.Series.Clear();
            //lblLuongNam.Text = pfd.GetLuongNam(fMainMenu.currentStaff, Convert.ToInt32(cboNam.Text));
            //VeBDLuongThangNV(fMainMenu.currentStaff.MaNV, cboNam.Text);
        }

        #region Vẽ biểu đồ

        // Biểu đồ lương theo từng tháng trong 1 năm của nhân viên đó
        public void VeBDLuongThangNV(string manv, string year)
        {
            //var lstparent = pfd.LayLuongTT(manv, year);
            ////Add series
            //chartLuongThangNV.Series.Add("Lương");
            //chartLuongThangNV.Series[0].ChartType = SeriesChartType.Column;

            ////Adjust Chart
            ////...

            ////Add columns
            //foreach (var ele in lstparent)
            //{
            //    chartLuongThangNV.Series[0].Points.AddXY(ele.Key, ele.Value);
            //}
        }

        #endregion

        #region Adjust Panel
        private void CustomizeDesing()
        {
            pnlTTCN.Visible = false;
            pnlDuAn.Visible = false;
            pnlLuong.Visible = false;
            pnlXinNghiPhep.Visible = false;
            pnlBaoMat.Visible = false;
            pnlDoiMatKhau.Visible = false;
        }
        private void HidePanel()
        {
            if (currentPanel.Visible)
            {
                currentPanel.Visible = false;
            }
        }

        private void ShowPanel()
        {
            if (currentPanel.Visible == false)
            {
                currentPanel.Visible = true;
            }
        }

        private void sideBarTimer_Tick(object sender, EventArgs e)
        {
            //Set the minimum and maximum size of sidebar panel
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }
        #endregion
    }
}
