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

namespace QLCongTy.Views.QLDuAn
{
    public partial class fTaiNguyen : Form
    {
        TaiNguyenDao tnguyenDao = new TaiNguyenDao();
        private int MaDA;
        public fTaiNguyen(int MaDA)
        {
            this.MaDA = MaDA;
            InitializeComponent();
            LoadGVCapTaiNguyen();
            LoadGVTaiNguyen();
        }

        private void cboNhiemVuTienQuyet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fTaiNguyen_Load(object sender, EventArgs e)
        {

        }

        private void LoadGVCapTaiNguyen()
        {
            gvTaiNguyenDA.DataSource = tnguyenDao.LoadTNDuAn(MaDA);
        }

        private void LoadGVTaiNguyen()
        {
            gvDSTaiNguyen.DataSource = tnguyenDao.DSTaiNguyen();
        }

        private void btnCapTN_Click(object sender, EventArgs e)
        {
            TAINGUYEN tnguyen = new TAINGUYEN()
            { 
               MaTN= txtMaTaiNguyen.Texts, 
               TenTN= txtTenTaiNguyen.Texts, 
               LoaiTaiNguyen=  txtLoaiTaiNguyen.Texts
            };
            tnguyenDao.ThemTaiNguyen(this.MaDA, tnguyen.MaTN);
            LoadGVCapTaiNguyen();
        }

        private void gvDSTaiNguyen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                DataGridViewRow row = gvDSTaiNguyen.Rows[e.RowIndex];
                txtMaTaiNguyen.Texts = row.Cells[0].Value.ToString();
                txtTenTaiNguyen.Texts = row.Cells[1].Value.ToString();
                txtLoaiTaiNguyen.Texts = row.Cells[2].Value.ToString();
            }
        }
    }
}
