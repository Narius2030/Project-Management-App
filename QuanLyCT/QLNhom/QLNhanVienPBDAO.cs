using System.Data;

namespace QLCongTy.QLPhongBan
{
    internal class QLNhanVienPBDAO
    {
        DBConnection db = new DBConnection();
        public DataTable LDSTP(string mapb)
        {
            string sqlStr = string.Format("select NHANSU.MaPB, PHONGBAN.TenPB,NHANSU.MaNV,NHANSU.Ten  from NHANSU,PHONGBAN where NHANSU.MaNV = PHONGBAN.MaTP and PHONGBAN.MaPB = '{0}'", mapb);
            return db.FormLoad(sqlStr);
        }

        public void Sua(string mpb, string tpb, string mnv, string tnv)
        {
            string sqlStr = string.Format("UPDATE NHANSU SET MaPB = '{0}'  WHERE MaNV = '{1}'", mpb, mnv);
            db.ThucThi(sqlStr);
        }
        public DataTable TimKiem(string mpb, string tpb, string mnv, string tnv)
        {
            string sqlStr = string.Format("select NHANSU.MaPB, PHONGBAN.TenPB,NHANSU.MaNV,NHANSU.Ten  from NHANSU,PHONGBAN where NHANSU.MaPB = PHONGBAN.MaPB and NHANSU.MaNV <> PHONGBAN.MaTP and PHONGBAN.MaPB = '{0}'", mpb);
            return db.FormLoad(sqlStr);
        }

        public DataTable GetNameDept()
        {
            string sqlStr = "SELECT TenPB, MaPB FROM PHONGBAN";    //CONCAT(MaPB, ' - ', TenPB) AS PhongBan, MaPB FROM PHONGBAN";
            return db.FormLoad(sqlStr);
        }
    }
}
