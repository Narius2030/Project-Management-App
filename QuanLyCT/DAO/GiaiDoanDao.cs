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
using System.Security.Cryptography;
using System.Windows.Forms;

namespace QLCongTy.DAO
{
    public class GiaiDoanDao
    {
        DBConnection dbC = new DBConnection();
        public DataTable GetListSprint(int mada)
        {
            return dbC.ExecuteQuery($"Select * From GiaiDoan WHERE MaDA={mada}");
        }
        public Boolean CheckGiaiDoanTruoc(GIAIDOAN gd)
        {
            String MaGiaiDoanTruoc = getMaGiaiDoanTruoc(gd.MaGiaiDoan);
            if (MaGiaiDoanTruoc == "Không có dự án trước đó")
            {
                return false;
            }
            SqlParameter[] parame = new SqlParameter[]
            {
                new SqlParameter("@maduan",SqlDbType.Int){Value=gd.MaDA},
                new SqlParameter("@MaGiaiDoan", SqlDbType.VarChar) { Value = MaGiaiDoanTruoc } 
            };
            string result = string.Empty;

            DataTable dt = dbC.ExecuteProcedure("sp_KiemTraGiaiDoanTruoc", parame);

            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["Result"].ToString();
            }
            if (result == "true")
            {
                return true;
            }
            else 
            {
                return false; 
            }
        }
        public DataTable CheckGiaiDoan(GIAIDOAN gd)
        {
            String MaGiaiDoanTruoc = getMaGiaiDoanTruoc(gd.MaGiaiDoan);
            SqlParameter[] parame = new SqlParameter[]
            {
                new SqlParameter("@maduan",SqlDbType.Int){Value=gd.MaDA},
                new SqlParameter("@MaGiaiDoan", SqlDbType.VarChar) { Value = MaGiaiDoanTruoc }
            };
            return dbC.ExecuteProcedure("sp_KiemTraGiaiDoan",parame);
        }
        public void ThemGiaiDoan(GIAIDOAN giaidoan)
        {
            using (QLDAEntities entityf = new QLDAEntities())
            { //Truyền vào đối tươngj kiểu GIAIDOAN rồi, thì cần gì tạo new vậy??
                //giaidoan = new GIAIDOAN()
                //{
                //    MaGiaiDoan = giaidoan.MaGiaiDoan,
                //    NoiDung = giaidoan.NoiDung,
                //    NgayBD = giaidoan.NgayBD,
                //    NgayKT = giaidoan.NgayKT,
                //    MaDA = giaidoan.MaDA
                //};
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
        public String getMaGiaiDoanTruoc(String maGD)
        {
            int id = Int32.Parse(maGD.Substring(0, 2));
            if (id == 1)
            {
                return "Không có dự án trước đó";
            }
            else
            {
                id--;
                string prefix = id.ToString("D2");
                string suffix = maGD.Substring(2);
                return prefix + suffix;
            }
        }
    }
}
