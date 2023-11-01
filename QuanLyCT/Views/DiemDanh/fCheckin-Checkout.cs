using QLCongTy.DAO;
using QLCongTy.DTO;
using QLCongTy.QLDuAn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCongTy
{
    public partial class fCheckin_Checkout : Form
    {
        DiemDanhDao diemdanhDao = new DiemDanhDao();
        DIEMDANH dd = new DIEMDANH();
        GIAIDOAN gd = new GIAIDOAN();
        NHOM nhom = new NHOM();
        public fCheckin_Checkout()
        {
            InitializeComponent();
        }

        private void fCheckin_Checkout_Load(object sender, EventArgs e)
        {
            LoadDataDiemDanh();
        }

        public void LoadDataDiemDanh()
        {
            gvChecksang.DataSource = diemdanhDao.layDanhSachDiemDanh();
            gvChecksang.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void tpCheckSang_Click(object sender, EventArgs e)
        {

        }

        private void gvChecksang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = gvChecksang.SelectedRows[0];
            dd = new DIEMDANH() { 
                Ngay = DateTime.Parse(row.Cells["Ngay"].Value.ToString()),
                MaNV = row.Cells["MaNV"].Value.ToString(),
                NoiDung = row.Cells["MaNV"].Value.ToString()
            };
            gd = new GIAIDOAN()
            {
                MaDA = int.Parse(row.Cells["MaDA"].Value.ToString()),
                MaGiaiDoan = row.Cells["MaGiaiDoan"].Value.ToString()
            };
            nhom = new NHOM()
            {
                TenNhom = row.Cells["TenNhom"].Value.ToString(),
                SoGioMotNg = int.Parse(row.Cells["SoGioMotNg"].Value.ToString())
            };
        }

        private void btnSubmitNghi_Click(object sender, EventArgs e)
        {
            dd = new DIEMDANH()
            {
                Ngay = dtpNgayNghi.Value.Date,
                MaNV = txtMaNV.Texts,
                NoiDung = cboNoiDungNghi.Texts
            };
            try
            {
                diemdanhDao.ThemNgayNghi(dd);
                diemdanhDao.TinhTimeSprint(dd.MaNV);
                MessageBox.Show("Điểm danh thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadDataDiemDanh();
        }
    }
}
