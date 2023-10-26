using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCongTy.DTO;
using System.Windows;
using QLCongTy.Views.NhanSu;
using System.Data.SqlClient;

namespace QLCongTy.DAO
{
    internal class NhiemVuDao
    {
        DBConnection dbconn = new DBConnection();

        public DataTable DSNhiemVuNhom(int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@"SELECT NV.MaNV AS 'Nhân Viên', TenNhiemVu AS 'Tên Nhiệm Vụ', NV.TrangThai AS 'Trạng Thái', MaTienQuyet AS 'Tiên Quyết', NV.ThoiGianUocTinh AS 'Giờ Ước Tính', NV.ThoiGianLamThucTe AS 'Giờ Thực Tế'
                            FROM NHIEMVU NV
                            INNER JOIN CONGVIEC CV ON NV.MaCV = CV.MaCV
                            INNER JOIN NHOM N ON CV.TenNhom = N.TenNhom AND CV.MaDA = N.MaDA AND NV.MaNV = N.MaNV
                            INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan AND CV.MaDA = GD.MaDA
                            INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
                            WHERE DA.MaDA = {MaDA} AND GD.MaGiaiDoan = '{MaGiaiDoan}' AND CV.MaCV = {MaCV} AND N.TenNhom = '{TenNhom}'";
            return dbconn.ExecuteQuery(sqlStr);
        }

        //public Boolean CheckMaTienQuyet()
        //{
        //    //string sqlStr = $"SELECT dbo.CheckFKNhiemVuTienQuyet('{nhom.TenNhom}', {nhom.MaDA})";
        //    //SqlCommand cmd = new SqlCommand(sqlStr, conn);
        //    //conn.Open();
        //    //int result = Convert.ToInt32(cmd.ExecuteScalar());
        //    //conn.Close();
        //    //if (result == 0)
        //    //{
        //    //    return false;
        //    //}
        //    //else
        //    //{
        //    //    return true;
        //    //}
        //}

        public void ThemNhiemVu(NHIEMVU nv)
        {
            if (nv.MaTienQuyet != null)
            {
                
                //Check đã tồn tại công việc
            }
            using (QLDAEntities entity = new QLDAEntities())
            {
                try
                {
                    entity.NHIEMVUs.Add(nv);
                    entity.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Thuc Thi That Bai: {ex.Message}");
                }
            }
        }
    }
}
