using System.Data;

namespace QLCongTy.QLPhongBan
{
    internal class PhongBanDAO
    {
        DBConnection db = new DBConnection();

        public DataTable LDS()
        {
            string sqlStr = string.Format("select PHONGBAN.MaPB, TenPB, MaTP, Ten from PHONGBAN left outer join NHANSU on MaTP = MaNV");
            return db.FormLoad(sqlStr);
        }
        public void Them(PhongBan pb)
        {
            string sqlStr = string.Format("INSERT INTO PHONGBAN(MaPB,TenPB,MaTP) VALUES ('{0}', '{1}', '{2}')", pb.Mapb, pb.Tenpb, pb.Matp);
            db.ThucThi(sqlStr);
        }

        public void Xoa(PhongBan pb)
        {
            string sqlStr = string.Format("DELETE FROM PHONGBAN WHERE MaPB = '" + pb.Mapb + "'");
            db.ThucThi(sqlStr);
        }

        public void Sua(PhongBan pb)
        {
            string sqlStr = string.Format("UPDATE PHONGBAN SET  TenPB = '{0}', MaTP = '{1}' WHERE MaPB = '{2}'", pb.Tenpb, pb.Matp, pb.Mapb);
            db.ThucThi(sqlStr);
        }
        public DataTable TimKiem(PhongBan pb)
        {
            if (pb.Mapb == string.Empty)
            {
                return LDS();
            }
            string sqlStr = string.Format("select PHONGBAN.MaPB, TenPB, MaTP, Ten from PHONGBAN left outer join NHANSU on MaTP = MaNV WHERE PHONGBAN.MaPB = '{0}'", pb.Mapb);
            return db.FormLoad(sqlStr);
        }
    }
}
