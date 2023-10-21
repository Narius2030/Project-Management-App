using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using QLCongTy.DTO;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Windows.Navigation;

namespace QLCongTy.DAO
{
    public class GiaiDoanDao
    {
        DBConnection dbC = new DBConnection();
        public DataTable GetListSprint()
        {
            return dbC.ExecuteQuery("Select *From GiaiDoan");
        }
        public DataTable CheckGiaiDoan(GIAIDOAN gd)
        {
            SqlParameter[] parame = new SqlParameter[]
            {
                new SqlParameter("@maduan",SqlDbType.Int){Value=gd.MaDA}
            };
            return dbC.ExecuteProcedure("sp_KiemTraGiaiDoan",parame);
        }
        public void ThemGiaiDoan(GIAIDOAN giaidoan)
        {
            using (QLDAEntities entityf = new QLDAEntities())
            {
                giaidoan = new GIAIDOAN()
                {
                    MaGiaiDoan = giaidoan.MaGiaiDoan,
                    NoiDung = giaidoan.NoiDung,
                    NgayBD = giaidoan.NgayBD,
                    NgayKT = giaidoan.NgayKT,
                    MaDA = giaidoan.MaDA
                };
                entityf.GIAIDOANs.Add(giaidoan);
                entityf.SaveChanges();
            }
        }
        public int SuaGiaiDoan(GIAIDOAN giaidoan)
        {
            using (QLDAEntities entityf = new QLDAEntities())
            {
                var query = from q in entityf.GIAIDOANs
                            where q.MaGiaiDoan == giaidoan.MaGiaiDoan
                            select q;
                GIAIDOAN giaiDoanCanCapNhat = query.FirstOrDefault();
                if (giaiDoanCanCapNhat != null)
                {
                    giaiDoanCanCapNhat.NoiDung = giaidoan.NoiDung;
                    giaiDoanCanCapNhat.NgayBD = giaidoan.NgayBD;
                    giaiDoanCanCapNhat.NgayKT = giaidoan.NgayKT;
                    giaiDoanCanCapNhat.MaDA = giaidoan.MaDA;
                    entityf.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int XoaGiaiDoan(GIAIDOAN giaidoan)
        {
            using(QLDAEntities entityf =new QLDAEntities())
            {
                var query = from q in entityf.GIAIDOANs
                            where q.MaGiaiDoan == giaidoan.MaGiaiDoan
                            select q;

                GIAIDOAN gdkq = query.FirstOrDefault();
                if (gdkq != null) 
                {
                    entityf.GIAIDOANs.Remove(gdkq);
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
