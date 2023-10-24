using System;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QLCongTy.DAO;
using QLCongTy.DTO;
using System.Collections.Generic;

namespace QLCongTy.QLDuAn
{
    public partial class fQLDuAn : Form
    {  
        DuAnDao daDao = new DuAnDao();
        GiaiDoanDao gdD =new GiaiDoanDao();
        DUAN da = new DUAN();
        NHOM nhom = new NHOM();
        NhomDao nd = new NhomDao();
        public fQLDuAn()
        {
            InitializeComponent();
            //Ẩn dòng cuối cùng của DatagridView
            gvQLDuAn.AllowUserToAddRows = false;
            gvNLCTy.AllowUserToAddRows = false;
            gvNLDA.AllowUserToAddRows = false;
        }

        #region ReLoad Something
        void LoadDataGiaiDoan()
        {
            gvDSGiaiDoan.DataSource = gdD.GetListSprint(da.MaDA);
        }
        void LoadDataNhanLuc()
        {
            gvNLDA.DataSource = daDao.getNhanLucDA(da.MaDA);
            gvNLCTy.DataSource = daDao.getNhanLucCty();
        }
        public void LoadDataDA()
        {
            gvQLDuAn.DataSource = daDao.getProjectList();
        }
        void LoadTabPages()
        {
            foreach (TabPage tab in tabQLDA.TabPages)
            {
                if (tab.TabIndex != 0)
                    tabQLDA.Controls.Remove(tab);
            }
        }
        void LoadDataCboTimKiem()
        {
            foreach(DataGridViewRow row in gvQLDuAn.Rows)
            {
                cboFindMaDA.Items.Add($"{row.Cells["MaDA"].Value} - {row.Cells["TenDA"].Value}");
            }
        }

        #endregion

        private void fQLDuAn_Load(object sender, EventArgs e)
        {
            LoadDataDA();
            LoadDataCboTimKiem();
        }
        public void LoadCboFind()
        {
            
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            FTaoDuAn fTaoDuAn = new FTaoDuAn(da, btnThem.Text);
            fTaoDuAn.Show();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                daDao.removeDuAn(da.MaDA);
                MessageBox.Show("Thao tác thành công");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadDataDA();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            FTaoDuAn fTaoDuAn = new FTaoDuAn(da, btnSua.Text);
            fTaoDuAn.Show();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }
        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            LoadTabPages();
            tabQLDA.Controls.Add(tpChiaGianDoan);
            tabQLDA.SelectedIndex = 1;

            //Điền thông tin giai đoạn
            lblDuAn.Text = da.MaDA.ToString() + "_" + da.TenDA;
            LoadDataGiaiDoan();
        }
        private void btnTuyenNV_Click(object sender, EventArgs e)
        {
            LoadTabPages();
            tabQLDA.Controls.Add(tpTuyenNL);
            tabQLDA.SelectedIndex = 1;

            //Điền thông tin giai đoạn
            txtMaDA.Texts = da.MaDA.ToString();
            LoadDataNhanLuc();
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
            gvNLCTy.DataSource = daDao.FilterLevel(cboTrinhDo.Text);
        }
        private void btnXoaNVkhoiDA_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn loại nhân viên khỏi dự án ?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                daDao.removeThanhVienDA(nhom);
                //Xoa UOCLUONG nếu ko error
                //...
            }
            LoadDataNhanLuc();
        }
        private void btnThemNVvaoDA_Click(object sender, EventArgs e)
        {
            nhom.MaNV = txtNhomTruong.Texts;
            nhom.MaDA = int.Parse(txtMaDA.Texts);
            nhom.TenNhom = cboNhom.Text;
            nhom.SoGioMotNg = 0;
            if (cbNhomTruong.Checked == true)
            {
                TRUONGNHOM tn = new TRUONGNHOM();
                tn.MaNV = txtNhomTruong.Texts;
                tn.MaDA = int.Parse(txtMaDA.Texts);
                tn.TenNhom = cboNhom.Text;
                nd.ThemTruongNhom(tn);
            }
            nd.ThemThanhVien(nhom);
            MessageBox.Show("Thêm thành viên thành công");
            LoadDataNhanLuc();
        }

        private void cboTrinhDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void ReloadCboFind_Click(object sender, EventArgs e)
        {
            LoadDataDA();
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
            //Để data ra đối tượng DuAn để lưu trữ
            DataGridViewRow r = gvQLDuAn.SelectedRows[0];

            Type type = da.GetType();
            int i = 0;
            foreach (var propertyInfo in type.GetProperties())
            {
                MessageBox.Show(propertyInfo.Name.ToString());
                if (propertyInfo.PropertyType != typeof(ICollection<GIAIDOAN>) && propertyInfo.PropertyType != typeof(ICollection<TRUONGNHOM>) && propertyInfo.PropertyType != typeof(ICollection<TAINGUYEN>) && propertyInfo.PropertyType != typeof(NHANVIEN)) 
                {
                    if (propertyInfo.PropertyType == typeof(Nullable<System.DateTime>))
                    {
                        propertyInfo.SetValue(da, DateTime.Parse(r.Cells[i].Value.ToString()));
                    }
                    else if (propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(da, r.Cells[i].Value.ToString());
                    }
                    else if (propertyInfo.PropertyType == typeof(Nullable<float>))
                    {
                        propertyInfo.SetValue(da, float.Parse(r.Cells[i].Value.ToString()));
                    }
                    else
                    {
                        propertyInfo.SetValue(da, int.Parse(r.Cells[i].Value.ToString()));
                    }
                    i++;
                }
            }
            //Đổ data ra Datagridview TTPhancong
            LoadDataGiaiDoan();
        }
        private void gvPCDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = gvNLDA.SelectedRows[0];
            nhom.MaNV = row.Cells["MaNV"].Value.ToString();
            nhom.TenNhom = row.Cells["TenNhom"].Value.ToString();
            nhom.MaDA = da.MaDA;
        }
        private void gvNLCTy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = gvNLCTy.SelectedRows[0];
            txtNhomTruong.Texts = row.Cells["MaNV"].Value.ToString();
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
            gvNLCTy.Columns[0].HeaderText = "Mã Nhân Viên";
            gvNLCTy.Columns[1].HeaderText = "Trình Độ";
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

        private void gvDSGiaiDoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }    
            else
            {
                DataGridViewRow row = gvDSGiaiDoan.Rows[e.RowIndex];
                txtMaGD.Texts = row.Cells[0].Value.ToString();
                dtpNgayBD.Value = Convert.ToDateTime(row.Cells[2].Value.ToString());
                dtpNgayKT.Value = Convert.ToDateTime(row.Cells[3].Value.ToString());
                txtNoiDung.Texts = row.Cells[1].Value.ToString();
                lblDuAn.Text = row.Cells[4].Value.ToString();   
            }   
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                GIAIDOAN gd = new GIAIDOAN()
                {
                    MaGiaiDoan = txtMaGD.Texts,
                    NoiDung = txtNoiDung.Texts,
                    NgayBD = dtpNgayBD.Value,
                    NgayKT = dtpNgayKT.Value,
                    MaDA = Convert.ToInt32(lblDuAn.Text)
                };
                DataTable kq = gdD.CheckGiaiDoan(gd);
                if (kq.Rows.Count > 0)
                {
                    gdD.ThemGiaiDoan(gd);
                    LoadDataGiaiDoan();
                }
                else
                {
                    MessageBox.Show($"Thêm Thất Bại ", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }    
            }
            catch(Exception)
            { 
                MessageBox.Show("Thêm Thất Bại","Thông Báo",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
            }
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            try
            {
                GIAIDOAN gd = new GIAIDOAN()
                {
                    MaGiaiDoan = txtMaGD.Texts,
                };
                if (gdD.XoaGiaiDoan(gd) == 1)
                {
                    LoadDataGiaiDoan();
                    MessageBox.Show("Xoá Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xoá Thất Bại", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Xoá Thất Bại", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                GIAIDOAN gd = new GIAIDOAN()
                {
                    MaGiaiDoan = txtMaGD.Texts,
                    NoiDung = txtNoiDung.Texts,
                    NgayBD = dtpNgayBD.Value,
                    NgayKT = dtpNgayKT.Value,
                    MaDA = Convert.ToInt32(lblDuAn.Text)
                };
                 DataTable kq = gdD.CheckGiaiDoan(gd);
                if (gdD.SuaGiaiDoan(gd) == 1 && kq.Rows.Count>0)
                {
                    LoadDataGiaiDoan();
                    MessageBox.Show("Cập Nhật Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập Nhật Thất Bại", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }    
            }
            catch (Exception)
            {
                MessageBox.Show("Cập Nhật Thất Bại", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void cboTrinhDo_SelectedValueChanged(object sender, EventArgs e)
        {
            // Insert TRUONGNHOM
        }
    }
}
