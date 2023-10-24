using QLCongTy.DTO;
using QLCongTy.Views.NhanSu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace QLCongTy.DAO
{
    public class NhomDao
    {
        DBConnection dbconn = new DBConnection();
        public void ThemThanhVien(NHOM nhom)
        {
            using(QLDAEntities entity = new QLDAEntities())
            {
                try
                {
                    entity.NHOMs.Add(nhom);
                    entity.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Thuc Thi That Bai: {ex.Message}");
                }
            }
        }
        public void ThemTruongNhom(TRUONGNHOM tn)
        {
            using (QLDAEntities entity = new QLDAEntities())
            {
                try
                {
                    entity.TRUONGNHOMs.Add(tn);
                    entity.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Thuc Thi That Bai: {ex.Message}");
                }
            }
        }
        public void FindTruongNhom()
        {
            //Tìm trưởng nhóm để hiển thị lên checkbox
            //...
        }
    }
}
