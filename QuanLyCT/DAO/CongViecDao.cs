using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using QLCongTy.DTO;

namespace QLCongTy.DAO
{
     public class CongViecDao
    {
        DBConnection dbC=new DBConnection();
        public DataTable LayDanhSach(int mada,string magiaidoan)
        {
            return dbC.ExecuteQuery($"select MaCV,TrangThai,CVTienQuyet,TenCV,TienDo,TenNhom From CONGVIEC where CONGVIEC.MaDA={mada} and CONGVIEC.MaGiaiDoan='{magiaidoan}'");
        }
        public void Them (CONGVIEC cv)
        {
            using(QLDAEntities qlhs=new QLDAEntities())
            {
                CONGVIEC congviec = new CONGVIEC()
                {
                    TrangThai=cv.TrangThai,
                    CVTienQuyet=cv.CVTienQuyet,
                    TenCV=cv.TenCV,
                    TienDo=cv.TienDo,
                    TenNhom=cv.TenNhom,
                    MaDA=cv.MaDA,
                    MaGiaiDoan=cv.MaGiaiDoan,

                };
                qlhs.CONGVIECs.Add( congviec );
                qlhs.SaveChanges();
            }    
        }
        public int Xoa(CONGVIEC cv)
        {
            using(QLDAEntities entityf=new QLDAEntities()) 
            {
                 var query=from q in entityf.CONGVIECs
                           where q.MaCV==cv.MaCV
                           select q;
                 CONGVIEC cvketqua=query.FirstOrDefault();
                if(cvketqua!=null)
                {
                    entityf.CONGVIECs.Remove(cvketqua);
                    entityf.SaveChanges();
                    return 1;
                }    
                return 0;
                            
            }
        }
    }
}
