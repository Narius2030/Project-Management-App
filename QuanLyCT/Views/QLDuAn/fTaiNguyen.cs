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
        public fTaiNguyen()
        {
            InitializeComponent();
        }

        private void cboNhiemVuTienQuyet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fTaiNguyen_Load(object sender, EventArgs e)
        {

        }

        private void LoadGVCapTaiNguyen()
        {
            gvTaiNguyenDA.DataSource = tnguyenDao.LoadTNDuAn();
        }

        private void LoadGVTaiNguyen()
        {
            gvDSTaiNguyen.DataSource = tnguyenDao.DSTaiNguyen();
        }
    }
}
