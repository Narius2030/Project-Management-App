using QLCongTy.DAO;
using QLCongTy.DTO;
using QLCongTy.Views.QLDuAn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QLCongTy.QLDuAn
{
    public partial class fQLDuAn : Form
    {  
        DuAnDao daDao = new DuAnDao();
        GiaiDoanDao gdD =new GiaiDoanDao();
        DUAN da = new DUAN();
        NHOM nhom = new NHOM();
        NhomDao nd = new NhomDao();
        CongViecDao cvd=new CongViecDao();
        TruongNhomDao tnDao = new TruongNhomDao();
        public fQLDuAn()
        {
            InitializeComponent();
            //Ẩn dòng cuối cùng của DatagridView
            gvQLDuAn.AllowUserToAddRows = false;
            gvNhanSu.AllowUserToAddRows = false;
            gvNLDA.AllowUserToAddRows = false;
            if (fMainMenu.MaNV != "NV002")
            {
                showCEOButton();
            }
        }

        private void fQLDuAn_Load(object sender, EventArgs e)
        {
            LoadDataDA();
            LoadDataCboTimKiem();
        }
        public void LoadCboFind()
        {
            
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
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

        private void cboTrinhDo_SelectedIndexChanged(object sender, EventArgs e)
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

        #region ReLoad Something

        void LoadDataGiaiDoan()
        {
            gvDSGiaiDoan.DataSource = gdD.GetListSprint(da.MaDA, 1);
        }
        void LoadDataNhanLuc()
        {
            gvNLDA.DataSource = daDao.getNhanLucDA(da.MaDA);
            gvNhanSu.DataSource = daDao.getNhanLucCty();
        }
        void LoadDuLieuNhom()
        {
            cbbtennhom.DataSource = nd.laydanhsachnhom(da.MaDA);
            cbbtennhom.DisplayMember = "TenNhom";
        }
        void LoadCongViec()
        {
            txtmagiaidoan.Texts = txtMaGD.Texts;
            txtmavaten.Text = lblDuAn.Text;
            txtmaduan.Texts = (da.MaDA).ToString();
            gvDSPhanCong.DataSource = cvd.GetListJob(da.MaDA, txtMaGD.Texts);
        }
        public void LoadDataDA()
        {
            gvQLDuAn.DataSource = daDao.getProjectList(fMainMenu.MaNV);
        }
        void LoadTabPages()
        {
            foreach (TabPage tab in tpNhom.TabPages)
            {
                if (tab.TabIndex != 0)
                    tpNhom.Controls.Remove(tab);
            }
        }
        void LoadDataCboTimKiem()
        {
            foreach (DataGridViewRow row in gvQLDuAn.Rows)
            {
                cboFindMaDA.Items.Add($"{row.Cells["MaDA"].Value} - {row.Cells["TenDA"].Value}");
            }
        }

        #endregion

        #region Khởi động tab mới

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            LoadTabPages();
            tpNhom.Controls.Add(tpChiaGianDoan);
            tpNhom.SelectedIndex = 1;

            //Điền thông tin giai đoạn
            lblDuAn.Text = da.MaDA.ToString() + "_" + da.TenDA;
            LoadDataGiaiDoan();
        }
        private void btnTuyenNV_Click(object sender, EventArgs e)
        {
            LoadTabPages();
            tpNhom.Controls.Add(tpTuyenNL);
            tpNhom.SelectedIndex = 1;

            //Điền thông tin giai đoạn
            txtMaDA.Texts = da.MaDA.ToString();
            lblThongtinDA.Text=da.MaDA.ToString();
            LoadDataNhanLuc();
        }

        #endregion

        #region Xủ lý nhân lực dự án

        private void btnXoaNVkhoiDA_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Bạn chắc chắn muốn loại nhân viên {nhom.MaNV} khỏi dự án {nhom.MaDA}", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (cbNhomTruong.Checked == false)
                {
                    daDao.removeThanhVienDA(nhom);
                }
                else
                {
                    TRUONGNHOM tn = new TRUONGNHOM() {TenNhom=nhom.TenNhom, MaDA=nhom.MaDA, MaNV=nhom.MaNV};
                    //xoá thằng trưởng nhóm  trong bảng nhóm
                    daDao.removeNhomDA(tn);
                }
            }
            LoadDataNhanLuc();
        }
        private void btnXoaNT_Click(object sender, EventArgs e)
        {
            tnDao.xoaTruongNhom(nhom);
            LoadDataNhanLuc();
        }
        private void btnThemVaoNhom_Click(object sender, EventArgs e)
        {           
                nhom.MaNV = txtNhomTruong.Texts;
                nhom.MaDA = int.Parse(txtMaDA.Texts);
                nhom.TenNhom = cboNhom.Text;
                nhom.SoGioMotNg = int.Parse(txtSoGioMotNg.Texts);
                if (cbNhomTruong.Checked == true)
                {
                    TRUONGNHOM tn = new TRUONGNHOM();
                    tn.MaNV = txtNhomTruong.Texts;
                    tn.MaDA = int.Parse(txtMaDA.Texts);
                    tn.TenNhom = cboNhom.Text;
                    nd.ThemTruongNhom(tn);
                    nd.ThemThanhVien(nhom);
                    MessageBox.Show("Thêm nhóm trưởng thành công");
                }
                else
                {
                    if (nd.KiemTraTonTaiNhomTruong(nhom))
                    {
                        nd.ThemThanhVien(nhom);
                    }
                    else
                    {
                        MessageBox.Show("Cần có nhóm trưởng trước khi thêm thành viên vào nhóm");
                    }
                }
                LoadDataNhanLuc();
            
            
        }
        private void ReloadCboFind_Click(object sender, EventArgs e)
        {
            LoadDataDA();
        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            gvNhanSu.DataSource = daDao.FilterLevel(cboTrinhDo.Text);
        }

        #endregion

        #region Xử lý dự án
        private void btnThem_Click(object sender, EventArgs e)
        {
            FTaoDuAn fTaoDuAn = new FTaoDuAn(da, btnThem.Text);
            fTaoDuAn.Show();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                daDao.removeDuAn(da);
                MessageBox.Show("Thao tác thành công");
            }
            catch (Exception ex)
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

        #endregion

        #region Tuong tác DataGridView

        private void showCEOButton()
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = false;
            btnTuyenNV.Enabled = false; 
            btnCapTaiNguyen.Enabled = false;
        }
        private void showButton(int temp)
        {
            if (temp == 0)
            {
                //PM
                btnadd.Enabled = true;
                btnremove.Enabled = true;
                btnupdate.Enabled = true;
                btnthemcv.Enabled = true;
                btnxoacv.Enabled = true;
                btnupdatepc.Enabled = true;
            }
            else
            {
                //LEAD
                btnadd.Enabled = false;
                btnremove.Enabled = false;
                btnupdate.Enabled = false;
                btnthemcv.Enabled = false;
                btnxoacv.Enabled = false;
                btnupdatepc.Enabled = false;
            }
        }

        private void gvQLDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Để data ra đối tượng DuAn để lưu trữ
            DataGridViewRow r = gvQLDuAn.SelectedRows[0];
            if (fMainMenu.MaNV != "NV002")
            {
                if (r.Cells["MaPM"].Value.ToString() == fMainMenu.MaNV)
                {
                    showButton(0);
                }
                else
                {
                    showButton(1);
                }
            }
            Type type = da.GetType();
            int i = 0;
            foreach (var propertyInfo in type.GetProperties())
            {
                //MessageBox.Show(propertyInfo.Name);
                if(propertyInfo.PropertyType!=typeof(ICollection<GIAIDOAN>)
                    && propertyInfo.PropertyType != typeof(ICollection<TRUONGNHOM>) 
                    && propertyInfo.PropertyType!=typeof(ICollection<TAINGUYEN>) && propertyInfo.PropertyType != typeof(NHANVIEN))
                //MessageBox.Show(propertyInfo.Name.ToString());
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
                }
                i++;
            }
            //Đổ data ra Datagridview TTPhancong
            LoadDataGiaiDoan();
        }
        private void gvPCDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = gvNLDA.SelectedRows[0];
                nhom.MaNV = row.Cells["MaNV"].Value.ToString();
                nhom.TenNhom = row.Cells["TenNhom"].Value.ToString();
                nhom.MaDA = da.MaDA;
                try
                {
                    if (nhom.MaNV == nd.FindTruongNhom(nhom).Rows[0]["MaNV"].ToString())
                    {
                        txtNhomTruong.Texts = nhom.MaNV;
                    }
                }
                catch (Exception)
                {
                    cbNhomTruong.Checked = false;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Bạn Hãy làm theo quy trình","Thông Báo",MessageBoxButtons.RetryCancel,MessageBoxIcon.Warning);
            }
        }


        private void cbNhomTruong_CheckedChanged(object sender, EventArgs e)
        {
            ShowNhomTruong_ThanhVien();
        }

        public void ShowNhomTruong_ThanhVien()
        {
            if (cbNhomTruong.Checked == false)
            {
                gvNLDA.DataSource = daDao.getNhanLucDA(da.MaDA);
            }
            else
            {
                gvNLDA.DataSource = tnDao.laydanhsachnhomtruong(da.MaDA);
            }
        }

        private void gvNhanSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = gvNhanSu.SelectedRows[0];
            txtNhomTruong.Texts = row.Cells["MaNV"].Value.ToString();
            txtSoGioMotNg.Texts = nd.FindSoGioMotNg(row.Cells["MaNV"].Value.ToString(), int.Parse(txtMaDA.Texts)).ToString();
        }
        #endregion

        #region Timer cho Sidebar 

        bool sidebarExpand = false;
        private void tmShowTiendo_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                pnlTiendo.Height -= 5;
                if (pnlTiendo.Height == pnlTiendo.MinimumSize.Height)
                {
                    sidebarExpand = false;
                    tmShowTiendo.Stop();
                }
            }
            else
            {
                pnlTiendo.Height += 5;
                if (pnlTiendo.Height == pnlTiendo.MaximumSize.Height)
                {
                    sidebarExpand = true;
                    tmShowTiendo.Stop();
                }
            }
        }

        double valuePercent = 0;
        private void tmProgressBar_Tick(object sender, EventArgs e)
        {
            if (pbTienDoGD.Value == valuePercent)
            {
                tmProgressBar.Stop();
            }
            if (pbTienDoGD.Value + 5 <= valuePercent)
            {
                pbTienDoGD.Value += 5;
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
            gvNhanSu.Columns[0].HeaderText = "Mã Nhân Viên";
            gvNhanSu.Columns[1].HeaderText = "Trình Độ";
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

        #region Xử lý giai doạn và công việc

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
                double ketqua = daDao.UpdateTienDo(da.MaDA, row.Cells[0].Value.ToString());
                MessageBox.Show($"Tiến Độ Dự Án Trong Sprint {txtMaGD.Texts} Là:{ketqua}%", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    NgayBD = dtpNgayBD.Value.Date,
                    NgayKT = dtpNgayKT.Value.Date,
                    MaDA = da.MaDA
                };
                DataTable kq = gdD.CheckGiaiDoan(gd);

                if (gdD.CheckGiaiDoanTruoc(gd))
                {
                    if (kq.Rows.Count == 0)
                    {
                        gdD.ThemGiaiDoan(gd);
                        LoadDataGiaiDoan();
                    }
                    else
                    {
                        MessageBox.Show($"Thêm Thất Bại Rồi", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Giai đoạn trước chưa được phân công việc, không thể tạo giai đoạn mới","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Thêm Thất Bại.Bạn Hãy Làm theo quy trình", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
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
                    gdD.XoaUocLuong(da.MaDA, txtMaGD.Texts);
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
                MessageBox.Show("Xoá Thất Bại.Bạn Hãy Làm theo quy trình", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
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
                    MaDA = da.MaDA
                };
                gdD.SuaGiaiDoan(gd);
                MessageBox.Show("Thao tác thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cập Nhật Thất Bại: {ex.Message}", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }

        private void cboTrinhDo_SelectedValueChanged(object sender, EventArgs e)
        {
            // Insert TRUONGNHOM
        }
        private void vbTaoCV_Click(object sender, EventArgs e)
        {
            LoadTabPages();
            tpNhom.Controls.Add(tpPhanCongViec);
            tpNhom.SelectedIndex = 1;
            LoadDuLieuNhom();
            LoadCongViec();
        }

        private void gvDSPhanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = gvDSPhanCong.Rows[e.RowIndex];
                txtmacongviec.Texts = row.Cells[0].Value.ToString();
                txttienquyet.Texts = row.Cells[2].Value.ToString();
                txttencongviec.Texts = row.Cells[3].Value.ToString();
                if (int.TryParse(txtmacongviec.Texts, out int macongviec) && !String.IsNullOrEmpty(txtmagiaidoan.Texts))
                {
                    double ketqua = cvd.UpdateProgress((macongviec), txtmagiaidoan.Texts);
                    string trangthai = cvd.UpdateStatus(macongviec);
                    txttrangthai.Texts = trangthai;
                    txttiendo.Texts = ketqua.ToString();
                    double tiendoValue;
                    if (Double.TryParse(txttiendo.Texts, out tiendoValue))
                    {
                        pbTienDoGD.Value = 0;
                        valuePercent = (double)tiendoValue;
                        tmProgressBar.Start();
                    }
                }
                LoadCongViec();
            }
        }

        private void lblreload_Click(object sender, EventArgs e)
        {
            List<CTTextBox> list = new List<CTTextBox>()
            {
                txttiendo, txtmacongviec,txttrangthai,txttienquyet
            };
            foreach(CTTextBox t in list) 
            {
                t.Texts = "";
            }
            pbTienDoGD.Value = 0;
        }

        private void btnthemcv_Click(object sender, EventArgs e)
        {
            if(txtmaduan.Texts==null || txtmagiaidoan==null || txtmagiaidoan.Texts== "VD: 01DA01")
            {
                MessageBox.Show("Bạn Hãy Làm Theo Quy Trình", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                CONGVIEC cv = new CONGVIEC()
                {
                    TrangThai = "Pending",
                    CVTienQuyet = !string.IsNullOrEmpty(txttienquyet.Texts) ? Convert.ToInt32(txttienquyet.Texts) : (int?) null,
                    TenCV = txttencongviec.Texts,
                    TienDo = 0,
                    TenNhom = cbbtennhom.Texts,
                    MaDA = Convert.ToInt32(txtmaduan.Texts),
                    MaGiaiDoan = txtmagiaidoan.Texts
                };
                cvd.AddJob(cv);
                LoadCongViec();
            }
            catch(Exception )
            {
                MessageBox.Show("Bạn Hãy Làm Theo Quy Trình", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }

        }

        private void btnupdatepc_Click(object sender, EventArgs e)
        {
            if (txtmaduan.Texts == null || txtmagiaidoan == null || txtmagiaidoan.Texts == "VD: 01DA01")
            {
                MessageBox.Show("Bạn Hãy Làm Theo Quy Trình", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                CONGVIEC cv = new CONGVIEC()
                {
                    MaCV = !string.IsNullOrEmpty(txtmacongviec.Texts) ? Convert.ToInt32(txtmacongviec.Texts) : 0,
                    TrangThai = txttrangthai.Texts,
                    CVTienQuyet = !string.IsNullOrEmpty(txttienquyet.Texts) ? Convert.ToInt32(txttienquyet.Texts) : (int?)null,
                    TenCV = txttencongviec.Texts,
                    TienDo = float.Parse(txttiendo.Texts),
                    TenNhom = cbbtennhom.Texts,
                    MaDA = Convert.ToInt32(txtmaduan.Texts),
                    MaGiaiDoan = txtmagiaidoan.Texts
                };
                cvd.UpdateJob(cv);
                MessageBox.Show("Thao tác thành công", "Thông Báo");
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn Hãy Làm Theo Quy Trình", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }

        private void btnCapTaiNguyen_Click(object sender, EventArgs e)
        {
            fTaiNguyen ftnguyen = new fTaiNguyen(da.MaDA);
            ftnguyen.TopLevel = false;
            tpTaiNguyen.Controls.Add(ftnguyen);
            ftnguyen.FormBorderStyle = FormBorderStyle.None;
            ftnguyen.Show();
            LoadTabPages();
            tpNhom.Controls.Add(tpTaiNguyen);
            tpNhom.SelectedIndex = 1;
        }

        private void btnxoacv_Click(object sender, EventArgs e)
        {
            try
            {
                CONGVIEC cv = new CONGVIEC()
                {
                    MaCV = !string.IsNullOrEmpty(txtmacongviec.Texts) ? Convert.ToInt32(txtmacongviec.Texts) : 0
                };
                cvd.KiemTraCongViecTienQuyet(cv);
                if (cvd.RemoveJob(cv) == 1)
                {
                    MessageBox.Show("Xoá Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCongViec();
                }
                else
                {
                    MessageBox.Show("Xoá Thất Bại", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn Hãy Làm Theo Quy Trình", "Thông Báo", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        
        }

        #endregion
    }
}
