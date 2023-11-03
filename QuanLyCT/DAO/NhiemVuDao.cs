using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLCongTy.DTO;
using QLCongTy.Views.NhanSu;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace QLCongTy.DAO
{
    internal class NhiemVuDao
    {
        public SqlConnection conn = new SqlConnection(Properties.Settings.Default.cnnStr);
        DBConnection dbconn = new DBConnection();

        public DataTable DSNhiemVuNhom(string MaNV, int MaDA, string MaGiaiDoan, int MaCV, string TenNhom)
        {
            string sqlStr = $@" select 
                                    MaNhiemVu as [Nhiệm Vụ],TenNhiemVu as [Tên Nhiệm Vụ],TrangThai as [Trạng Thái],MaTienQuyet as [Mã Tiên Quyết],
<<<<<<< HEAD
                                    ThoiGianUocTinh as [Giờ Ước Tính],ThoiGianLamThucTe as [Giờ Thực Tế] From v_DanhSachNhiemVuNhom
=======
                                    ThoiGianUocTinh as [Giờ Ước Tính],ThoiGianLamThucTe as [Giờ Thực Tế] 
                                From v_DanhSachNhiemVuNhom
>>>>>>> d0a845557af48923a7114dddfc282bb9c463914c
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
        public int SuaNhiemVu(NHIEMVU nv)
        {
            using(QLDAEntities entity = new QLDAEntities())
            {
                try
                {
                    var query=from q in entity.NHIEMVUs
                              where q.MaNhiemVu == nv.MaNhiemVu
                               select q;
                    NHIEMVU kq=query.FirstOrDefault();
                    if(kq != null) 
                    {
                        kq.TrangThai = nv.TrangThai;
                        kq.ThoiGianLamThucTe = nv.ThoiGianLamThucTe;
                        kq.TenNhiemVu = nv.TenNhiemVu;
                        entity.SaveChanges() ;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }    
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Thuc Thi That Bai: {ex.Message}");
                    return 0;
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
            DataTable result = dbconn.ExecuteQuery(sqlStr);
            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["MaNhiemVu"].ToString();
            }
            else
            {
                MessageBox.Show("Nhân viên chưa được phân công nhiệm vụ nào", "Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return "00" + "CV" + MaCV.ToString("D2") + "DA" + MaDA.ToString("D2");
            }
        }
        public int KiemTraNhiemVuTienQuyet(string manhiemvu)
        {
            SqlParameter[] sp = new SqlParameter[]
            {
                  new SqlParameter("@manv",SqlDbType.VarChar, 10){Value=manhiemvu},
                  new SqlParameter("@check",SqlDbType.Real){Direction = ParameterDirection.Output}

            };
            dbconn.ExecuteProcedure("sp_KiemTraNhiemVuTienQuyet", sp);
            int ketqua = Convert.ToInt32(sp[1].Value);
            return ketqua;
            
        }
        public void SetNullTienQuyet(NHIEMVU nv)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@manhiemvu",SqlDbType.VarChar, 10){Value=nv.MaNhiemVu}
            };
            dbconn.ExecuteProcedure("sp_KiemTraNhiemVu", parameters);
        }
        public int CapNhatTimeTask(string manv, int maduan, string magiaidoan)
        {
            int ketqua;
<<<<<<< HEAD
=======

>>>>>>> d0a845557af48923a7114dddfc282bb9c463914c
            try
            {
                ketqua = Convert.ToInt32(dbconn.ExecuteScalar($"SELECT dbo.sfn_CapNhatTimeTask('{manv}',{maduan},'{magiaidoan}')"));
            }
            catch
            (Exception)
            {
                ketqua = 0;
            }
            return ketqua;
        }
        public int TongTimeTask(string manv, int maduan, string magiaidoan)
        {
            int ketqua;
            try
            {
                ketqua = Convert.ToInt32(dbconn.ExecuteScalar($"SELECT dbo.sfn_SumTimeTask('{manv}',{maduan},'{magiaidoan}')"));
            }
            catch
            (Exception)
            {
                ketqua = 0;
            }
            return ketqua;
        }
    }
}
