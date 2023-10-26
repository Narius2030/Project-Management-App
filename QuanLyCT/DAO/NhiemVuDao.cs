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
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        DBConnection dbconn = new DBConnection();

        public DataTable DSNhiemVuNhom(int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@"SELECT NV.MaNhiemVu AS 'Nhiệm Vụ', TenNhiemVu AS 'Tên Nhiệm Vụ', NV.TrangThai AS 'Trạng Thái', MaTienQuyet AS 'Tiên Quyết', NV.ThoiGianUocTinh AS 'Giờ Ước Tính', NV.ThoiGianLamThucTe AS 'Giờ Thực Tế'
                            FROM NHIEMVU NV
                            INNER JOIN CONGVIEC CV ON NV.MaCV = CV.MaCV
                            INNER JOIN NHOM N ON CV.TenNhom = N.TenNhom AND CV.MaDA = N.MaDA AND NV.MaNV = N.MaNV
                            INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan AND CV.MaDA = GD.MaDA
                            INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
                            WHERE DA.MaDA = {MaDA} AND GD.MaGiaiDoan = '{MaGiaiDoan}' AND CV.MaCV = {MaCV} AND N.TenNhom = '{TenNhom}'";
            return dbconn.ExecuteQuery(sqlStr);
        }

        //Kiem tra xem nhiem vu tien quyet co ton tai chua
        //public Boolean CheckMaTienQuyet()
        //{
        //    string sqlStr = $"SELECT dbo.CheckFKNhiemVuTienQuyet('{MaDA}', {MaGiaiDoan}, {MaCV}, {TenNhom}, {MaNV}, {MaTienQuyet})";
        //    SqlCommand cmd = new SqlCommand(sqlStr, conn);
        //    conn.Open();
        //    int result = Convert.ToInt32(cmd.ExecuteScalar());
        //    conn.Close();
        //    if (result == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        public void ThemNhiemVu(NHIEMVU nv)
        {
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

        public int XoaNhiemVu(NHIEMVU nv)
        {
            using (QLDAEntities entityf = new QLDAEntities())
            {
                var query = from q in entityf.NHIEMVUs
                            where q.MaNhiemVu == nv.MaNhiemVu
                            select q;
                NHIEMVU cvketqua = query.FirstOrDefault();
                if (cvketqua != null)
                {
                    entityf.NHIEMVUs.Remove(cvketqua);
                    entityf.SaveChanges();
                    return 1;
                }
                return 0;
            }
        }

        public DataTable DSNhiemVu(int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@"SELECT CONCAT(NV.MaNhiemVu, ' - ' , NV.TenNhiemVu) AS NhiemVu, NV.MaNhiemVu
                            FROM NHIEMVU NV
                            INNER JOIN CONGVIEC CV ON NV.MaCV = CV.MaCV
                            INNER JOIN NHOM N ON CV.TenNhom = N.TenNhom AND CV.MaDA = N.MaDA AND NV.MaNV = N.MaNV
                            INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan AND CV.MaDA = GD.MaDA
                            INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
                            WHERE DA.MaDA = {MaDA} AND GD.MaGiaiDoan = '{MaGiaiDoan}' AND CV.MaCV = {MaCV} AND N.TenNhom = '{TenNhom}'
                            ORDER BY NV.MaNhiemVu";
            return dbconn.ExecuteQuery(sqlStr);
        }

        public string NhiemVuMoiNhat(int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@"SELECT Top 1 NV.MaNhiemVu
                            FROM NHIEMVU NV
                            INNER JOIN CONGVIEC CV ON NV.MaCV = CV.MaCV
                            INNER JOIN NHOM N ON CV.TenNhom = N.TenNhom AND CV.MaDA = N.MaDA AND NV.MaNV = N.MaNV
                            INNER JOIN GIAIDOAN GD ON CV.MaGiaiDoan = GD.MaGiaiDoan AND CV.MaDA = GD.MaDA
                            INNER JOIN DUAN DA ON GD.MaDA = DA.MaDA
                            WHERE DA.MaDA = {MaDA} AND GD.MaGiaiDoan = '{MaGiaiDoan}' AND CV.MaCV = {MaCV} AND N.TenNhom = '{TenNhom}'
                            ORDER BY NV.MaNhiemVu DESC";
            DataTable result =  dbconn.ExecuteQuery(sqlStr);
            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["MaNhiemVu"].ToString();
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được phân công nhiệm vụ nào");
                return "00" + "CV" + MaCV.ToString("D2") + "DA" + MaDA.ToString("D2");
            }
        }

        public void SetNullTienQuyet(NHIEMVU nv)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@manhiemvu",SqlDbType.Int){Value=nv.MaNhiemVu}
            };
            dbconn.ExecuteProcedure("sp_UpdateTrangThai", parameters);
        }
    }
}
