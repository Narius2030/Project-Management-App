using System;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLCongTy.QLDuAn
{
    public partial class fQLDuAn : Form
    {  
        
        public fQLDuAn()
        {
            InitializeComponent();
            //Ẩn dòng cuối cùng của DatagridView
            gvQLDuAn.AllowUserToAddRows = false;
            gvNhanLuc.AllowUserToAddRows = false;
            gvPCDuAn.AllowUserToAddRows = false;
        }

        private void fQLDuAn_Load(object sender, EventArgs e)
        {
           
            
        }
        public void LoadCboFind()
        {
            
        }
        public void ReLoadPCDuAn()
        {
           
        }

        private void gvPCDuAn_Row_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow r = gvPCDuAn.SelectedRows[0];
            txtMaNV.Texts = r.Cells[1].Value.ToString();
        }

        private void gvNhanLuc_Row_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow r = gvNhanLuc.SelectedRows[0];
            txtMaNV.Texts = r.Cells[0].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            
        }

        private bool CheckQuyen(string MaTruongDA)
        {
            if (fMainMenu.MaNV == MaTruongDA)
            {
                return true;
            }
            return false;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            
        }

        private void btnXoaNVkhoiDA_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThemNVvaoDA_Click(object sender, EventArgs e)
        {
           
        }

        private void cboTrinhDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void ReloadCboFind_Click(object sender, EventArgs e)
        {
            
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            tmShowTiendo.Start();
            chartTiendoCN.Series.Clear();
            chartTienDoDA.Series.Clear();
            chartTongTiendo.Series.Clear();
            VeBDTienDoCN();
            VeBDTienDoDA();
            VeBDTongTienDoDA();
        }

        private void cboFindMaDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(cboFindMaDA.SelectedValue.ToString());
        }

        #region Tuong tác DataGridView

        private void gvQLDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void gvPCDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void gvNhanLuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        #endregion

        #region Timer cho Sidebar 

        bool sidebarExpand = false;
        private void tmShowTiendo_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                pnlTiendo.Height -= 50;
                if (pnlTiendo.Height == pnlTiendo.MinimumSize.Height)
                {
                    sidebarExpand = false;
                    tmShowTiendo.Stop();
                }
            }
            else
            {
                pnlTiendo.Height += 50;
                if (pnlTiendo.Height == pnlTiendo.MaximumSize.Height)
                {
                    sidebarExpand = true;
                    tmShowTiendo.Stop();
                }
            }
        }


        #endregion

        #region Vẽ biểu đồ tiến dộ

        public void VeBDTienDoCN()
        {
            
        }

        public void VeBDTienDoDA()
        {
        }

        //Vẽ biểu đồ tổng tiến độ dự án
        public void VeBDTongTienDoDA()
        {
            
        }

        #endregion

        #region Adjust Form

        void DoiTen()
        {
            gvQLDuAn.Columns[0].HeaderText = "Mã Dự Án";
            gvQLDuAn.Columns[1].HeaderText = "Tên Dự Án";
            gvQLDuAn.Columns[2].HeaderText = "Mã Phong Ban";
            gvQLDuAn.Columns[3].HeaderText = "Vốn Điều Hành";
            gvQLDuAn.Columns[4].HeaderText = "Mã Trưởng Dự Án";
            gvQLDuAn.Columns[5].HeaderText = "Bắt Đầu";
            gvQLDuAn.Columns[6].HeaderText = "Kết Thúc";
            gvQLDuAn.Columns[7].HeaderText = "Trạng Thái";
            gvNhanLuc.Columns[0].HeaderText = "Mã Nhân Viên";
            gvNhanLuc.Columns[1].HeaderText = "Trình Độ";
        }
        private void gvQLDuAn_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in gvQLDuAn.Rows)
            {
                DateTime Deadline = Convert.ToDateTime(row.Cells["NgayKT"].Value);
                bool OutDeadLine = DateTime.Now > Deadline;
                if (OutDeadLine)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    int TienDo = Convert.ToInt32(row.Cells["Tiendo"].Value);
                    if (TienDo == 100)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        #endregion
      
    }
}
