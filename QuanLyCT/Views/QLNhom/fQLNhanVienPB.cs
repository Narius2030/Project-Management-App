
using System;
using System.Windows.Forms;

namespace QLCongTy
{
    public partial class FQLNhanVienPB : Form
    {
      
        public FQLNhanVienPB()
        {
            InitializeComponent();

        }

        private void fQLNhanVienPB_Load(object sender, EventArgs e)
        {
            GetCboPhongBan();
        }

        void DoiTen()
        {
            gvTruongPhong.Columns[0].HeaderText = "Mã Phong Ban";
            gvTruongPhong.Columns[1].HeaderText = "Tên Phong Ban";
            gvTruongPhong.Columns[2].HeaderText = "Mã Trưởng Phòng";
            gvTruongPhong.Columns[3].HeaderText = "Tên Trưởng Phòng";

            gvNhanVienPB.Columns[0].HeaderText = "Mã Phong Ban";
            gvNhanVienPB.Columns[1].HeaderText = "Tên Phong Ban";
            gvNhanVienPB.Columns[2].HeaderText = "Mã Nhân Viên";
            gvNhanVienPB.Columns[3].HeaderText = "Tên Nhân Viên";
        }


        private void gvNhanVienPB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            DataGridViewRow row = gvNhanVienPB.Rows[e.RowIndex];
            txtMaNhanVien.Text = row.Cells[2].Value.ToString();
            txtTenNhanVien.Text = row.Cells[3].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            
            DoiTen();
        }

        private void txtPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            DoiTen();
        }

        private void GetCboPhongBan()
        {
          
            cboPhongBan.DisplayMember = "TenPB";
            cboPhongBan.ValueMember = "MaPB";
        }
    }
}
