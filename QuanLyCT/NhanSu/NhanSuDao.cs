using QLCongTy.ChamCong;
using System;
using System.Collections.Generic;
using System.Data;

namespace QLCongTy.NhanSu
{
    internal class NhanSuDAO
    {
        DBConnection db = new DBConnection();
        public DataTable DanhSach()
        {
            return db.FormLoad("SELECT *FROM NHANSU");
        }

        public DataTable Loc(string col, string value)
        {
            string lenh = string.Format("SELECT *FROM NHANSU WHERE {0} = '{1}'", col, value);
            return db.FormLoad(lenh);
        }
        public void Them(Nhansu ns)
        {
            string sqlStr = string.Format("INSERT INTO NHANSU(MaNV, HovaTendem, Ten, NgaySinh, DiaChi, CCCD, MaPB, GioiTinh, SDT, Email, MaCV, TrinhDo) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')", ns.MaNV, ns.HoDem, ns.Ten, ns.NgaySinh, ns.DiaChi, ns.CCCD, ns.MaPB, ns.GioiTinh, ns.SDT, ns.Email, ns.MaCV, ns.Trinhdo);
            db.ThucThi(sqlStr);

            //Tao TAIKHOAN moi mac dinh
            sqlStr = string.Format("INSERT INTO TAIKHOAN VALUES('{0}', '{1}','{2}')", ns.MaNV, ns.MaNV, ns.MaCV);
            db.ThucThi(sqlStr);
        }

        public void Xoa(Nhansu ns)
        {
            string sqlStr = string.Format("DELETE FROM NHANSU WHERE MaNV = '{0}';", ns.MaNV);
            db.ThucThi(sqlStr);
            sqlStr = string.Format("DELETE FROM TAIKHOAN WHERE taikhoan = '{0}';", ns.MaNV);
            db.ThucThi(sqlStr);
        }

        public void Sua(Nhansu ns)
        {
            string sqlStr = string.Format("UPDATE NHANSU SET HovaTendem = '{0}', Ten = '{1}', NgaySinh = '{2}', DiaChi = '{3}', CCCD = '{4}', MaPB = '{5}', GioiTinh = '{6}', SDT = '{7}', Email = '{8}', MaCV = '{9}', TrinhDo = '{11}' WHERE MaNV = '{10}'", ns.HoDem, ns.Ten, ns.NgaySinh, ns.DiaChi, ns.CCCD, ns.MaPB, ns.GioiTinh , ns.SDT, ns.Email, ns.MaCV, ns.MaNV, ns.Trinhdo);
            db.ThucThi(sqlStr);
            sqlStr = string.Format("UPDATE TAIKHOAN SET MaCV = '{0}' WHERE taikhoan ='{1}'",ns.MaCV,ns.MaNV );
            db.ThucThi(sqlStr);
        }
        public float LuongTheoThang(string mapb, int year)
        {
            string sqlStr = $@"select sum(tl.LuongThucTe) as Luong
                                from NHANSU as ns inner join TIENLUONG as tl
	                                on ns.MaNV = tl.MaNV
                                where MaPB = '{mapb}' and Nam = {year}";
            DataTable dt = db.FormLoad(sqlStr);

            float luong = float.Parse(dt.Rows[0]["Luong"].ToString());
            return luong;
        }

        //Lấy số lượng phòng --> List<>
        public List<string> LaySLPhong()
        {
            string sqlStr = $@"select MaPB from PHONGBAN";
            DataTable dt = db.FormLoad(sqlStr);

            List<string> list = new List<string>();
            for (int i=0; i<dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i]["MaPB"].ToString());
            }
            return list;
        }

        //Lấy số lượng năm --> List<>
        public List<int> LaySLNam()
        {
            string sqlStr = $@"select distinct Nam from TIENLUONG";
            DataTable dt = db.FormLoad(sqlStr);

            List<int> list = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(int.Parse(dt.Rows[i]["Nam"].ToString()));
            }
            return list;
        }

        //Lấy số lượng trình độ --> Dictionary
        public List<KeyValuePair<string, int>> LaySLTrinhDo() 
        {
            string sqlStr = $@"select TrinhDo, count(TrinhDo) as so_luong from NHANSU group by TrinhDo";
            DataTable dt = db.FormLoad(sqlStr);

            var list = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new KeyValuePair<string, int>(dt.Rows[i]["TrinhDo"].ToString(), int.Parse(dt.Rows[i]["so_luong"].ToString())));
            }
            return list;
        }
        public double TiLeTGDA()
        {
            string sqlStr = $@"select distinct count(MaNV) as so_luong from PHANCONGDUAN";
            DataTable dt = db.FormLoad(sqlStr);
            var sl_thamgia = int.Parse(dt.Rows[0]["so_luong"].ToString());

            sqlStr = $@"select distinct count(MaNV) as so_luong from NHANSU";
            dt = db.FormLoad(sqlStr);
            var tong_nv = int.Parse(dt.Rows[0]["so_luong"].ToString());

            double tile = Math.Round(((double)sl_thamgia / (double)tong_nv), 3) * 100;
            return tile;
        }
       
        public DataTable GetNameDept()
        {
            string sqlStr = "SELECT TenPB, MaPB FROM PHONGBAN";
            return db.FormLoad(sqlStr);
        }

        public DataTable GetChucVu()
        {
            string sqlStr = "SELECT TenCV, MaCV FROM CHUCVU";
            return db.FormLoad(sqlStr);
        }
    }
}
