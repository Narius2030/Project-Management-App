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
            string sqlStr = $@" select MaNhiemVu as [Nhiệm Vụ],TenNhiemVu as [Tên Nhiệm Vụ],TrangThai as [Trạng Thái],MaTienQuyet as [Mã Tiên Quyết],
                            ThoiGianUocTinh as [Giờ Ước Tính],ThoiGianLamThucTe as [Giờ Thực Tế] From v_DanhSachNhiemVuNhom
                           WHERE MaDA = {MaDA} AND MaGiaiDoan = '{MaGiaiDoan}' AND MaCV = {MaCV} AND TenNhom = '{TenNhom}'";
            return dbconn.ExecuteQuery(sqlStr);
        }

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
            string sqlStr = $@"SELECT CONCAT(MaNhiemVu, ' - ' , TenNhiemVu) AS NhiemVu, MaNhiemVu
                            FROM v_DanhSachNhiemVuNhom
                            WHERE MaDA = {MaDA} AND MaGiaiDoan = '{MaGiaiDoan}' AND MaCV = {MaCV} AND TenNhom = '{TenNhom}'
                            ORDER BY MaNhiemVu";
            return dbconn.ExecuteQuery(sqlStr);
        }

        public string NhiemVuMoiNhat(int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@"SELECT Top 1 MaNhiemVu
                            FROM v_DanhSachNhiemVuNhom
                            WHERE MaDA = {MaDA} AND MaGiaiDoan = '{MaGiaiDoan}' AND MaCV = {MaCV} AND TenNhom = '{TenNhom}'
                            ORDER BY MaNhiemVu DESC";
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
                new SqlParameter("@manhiemvu",SqlDbType.VarChar, 10){Value=nv.MaNhiemVu}
            };
            dbconn.ExecuteProcedure("sp_KiemTraNhiemVu", parameters);
        }
    }
}
