using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using QLCongTy.DTO;

namespace QLCongTy.DAO
{
    public class CongViecDao
    {
        DBConnection dbC = new DBConnection();
        public DataTable GetListJob(int mada, string magiaidoan)
        {
            return dbC.ExecuteQuery($"select MaCV,TrangThai,CVTienQuyet,TenCV,TienDo,TenNhom From CONGVIEC where CONGVIEC.MaDA={mada} and CONGVIEC.MaGiaiDoan='{magiaidoan}'");
        }
        public Double UpdateProgress(int macongviec, string magiaidoan)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCV",SqlDbType.Int){Value =macongviec},
                new SqlParameter("@magiaidoan ",SqlDbType.VarChar,20){Value =magiaidoan},
                new SqlParameter("@ketqua",SqlDbType.Real){Direction = ParameterDirection.Output }
            };
            dbC.ExecuteProcedure("sp_TinhTienDoCv", parameters);
            double ketqua = Convert.ToDouble(parameters[2].Value);
            return ketqua;
        }
        public string UpdateStatus(int macongviec)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@macongviec",SqlDbType.Int){Value=macongviec},
                new SqlParameter("@trangthai",SqlDbType.VarChar,20){Direction=ParameterDirection.Output}
            };
            dbC.ExecuteProcedure("sp_UpdateTrangThai", parameters);
            string ketqua = Convert.ToString(parameters[1].Value);
            return ketqua;
        }
        public void AddJob(CONGVIEC cv)
        {
            using (QLDAEntities qlhs = new QLDAEntities())
            {
                
                qlhs.CONGVIECs.Add(cv);
                qlhs.SaveChanges();
            }
        }
        public int RemoveJob(CONGVIEC cv)
        {
            using (QLDAEntities entityf = new QLDAEntities())
            {
                var query = from q in entityf.CONGVIECs
                            where q.MaCV == cv.MaCV
                            select q;
                CONGVIEC cvketqua = query.FirstOrDefault();
                if (cvketqua != null)
                {
                    entityf.CONGVIECs.Remove(cvketqua);
                    entityf.SaveChanges();
                    return 1;
                }
                return 0;

            }
        }
        public int UpdateJob(CONGVIEC cv)
        {
            using (QLDAEntities entityf = new QLDAEntities())
            {
                var query = from q in entityf.CONGVIECs
                            where q.MaCV == cv.MaCV
                            select q;
                CONGVIEC cvketqua = query.FirstOrDefault();
                if (cvketqua != null)
                {
                    cvketqua.TrangThai = cv.TrangThai;
                    cvketqua.CVTienQuyet = cv.CVTienQuyet;
                    cvketqua.TenCV = cv.TenCV;
                    cvketqua.TienDo = cv.TienDo;
                    cvketqua.TenNhom = cv.TenNhom;
                    cvketqua.MaGiaiDoan = cv.MaGiaiDoan;
                    entityf.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
