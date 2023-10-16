using System;
using System.Data;

namespace QLCongTy.QLDuAn
{
    internal class DuAnDAO
    {
        DBConnection db = new DBConnection();

        public DataTable LayDanhSachDuAn(string manv)
        {
            string sqlStr = $"select * from DUAN where TruongDA = '{manv}'";
            return db.FormLoad(sqlStr);
        } 

        public DataTable DSDuAn()
        {
            string sqlStr = $"select * from DUAN";
            return db.FormLoad(sqlStr);
        }

        public DataTable LayDanhSachNhanLuc(string matp)
        {
            string sqlStr = $@"select MaNV, TrinhDo from NHANSU as nv
                               join PHONGBAN as pb on pb.MaPB = nv.MaPB
                               where pb.MaTP = '{matp}' and nv.MaNV != '{matp}'";
            return db.FormLoad(sqlStr);
        }
        public DataTable LayDanhSachPhanCong(string col, string value)
        {
            string sqlStr = $"select MaDA, MaNV, CongViec, NgayBD, NgayKT, TienDo from PHANCONGDUAN WHERE {col} = '{value}'";
            return db.FormLoad(sqlStr);
        }

        public DataTable LayDanhSachNVRanh(string matp)
        {
            string sqlStr = $@"select nv.MaNV, nv.TrinhDo from NHANSU as nv
                                join PHONGBAN as pb on pb.MaPB = nv.MaPB
                                join TRANGTHAINHANVIEN as ttnv on nv.MaNV = ttnv.MaNV
                                where pb.MaTP = '{matp}' and nv.MaNV != '{matp}' and ttnv.TrangThai = 'Ranh'";
            return db.FormLoad(sqlStr);
        }

        public DataTable LayDSNVRanhVaDieuKien(string matp, string col, string value)
        {
            string sqlStr = $@"select nv.MaNV, nv.TrinhDo from NHANSU as nv
                                join PHONGBAN as pb on pb.MaPB = nv.MaPB
                                join TRANGTHAINHANVIEN as ttnv on nv.MaNV = ttnv.MaNV
                                where pb.MaTP = '{matp}' and nv.MaNV != '{matp}' and ttnv.TrangThai = 'Ranh' and nv.{col} = '{value}'";
            return db.FormLoad(sqlStr);
        }

        public void Them(DuAn da)
        {
            string sqlStr = $"INSERT INTO DUAN VALUES ('{da.Mada}', '{da.Tenda}', '{da.Mapb}', 0, '{da.Truongda}', '{da.Ngaybd}', '{da.Ngaykt}', '{da.Trangthai}', 0)";
            db.ThucThi(sqlStr);
        }

        public void Xoa(string mada)
        {
            // Tránh xung đột khóa ngoại
            string sqlStr = $"DELETE FROM PHANCONGDUAN WHERE MaDA = '{mada}'";
            db.ThucThi(sqlStr);
            sqlStr = $"DELETE FROM DUAN WHERE MaDA = '{mada}'";
            db.ThucThi(sqlStr);
        }

        public void Sua(DuAn da)
        {
            string sqlStr = string.Format("UPDATE DUAN SET TenDA = '{0}', MaPB = '{1}',TruongDA = '{2}', NgayBD = '{3}', NgayKT = '{4}', TrangThai = '{5}' WHERE MaDA = '{6}'", da.Tenda, da.Mapb, da.Truongda, da.Ngaybd, da.Ngaykt, da.Trangthai, da.Mada);
            db.ThucThi(sqlStr);
        }

        public DataTable TimKiem(string MaDA)
        {
            string sqlStr = string.Format("SELECT * FROM DUAN WHERE MaDA = '{0}'",MaDA);
            return db.FormLoad(sqlStr);
        }

        public void ThemNVvaoDA(PCNhanLuc pcnl)
        {
            string sqlStr = string.Format("INSERT INTO PHANCONGDUAN VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '0')", pcnl.Mada, pcnl.Manv, pcnl.Congviec, pcnl.Ngaybd, pcnl.Ngaykt);
            db.ThucThi(sqlStr);
        }

        public void XoaNVkhoiDA(PCNhanLuc pcnl)
        {
            string sqlStr = string.Format("DELETE FROM PHANCONGDUAN WHERE MaDA = '{0}' AND MaNV = '{1}'", pcnl.Mada, pcnl.Manv);
            db.ThucThi(sqlStr);
        }

        public void TienDoDuAn(string MaDA)
        {
            int result = int.Parse(db.GetItem($"SELECT AVG(Tiendo) AS TienDoDuAn FROM PHANCONGDUAN WHERE MaDA = '{MaDA}' GROUP BY MaDA").ToString());
            string sqlStr = $"UPDATE DUAN SET TienDo = {result} WHERE MaDA = '{MaDA}'";
            db.ThucThi(sqlStr);
        }
        public void InitStatusTB()
        {
            DataTable dataset = db.FormLoad("select * from TRANGTHAINHANVIEN");
            if (dataset.Rows.Count <= 0)
            {
                dataset = db.FormLoad("select * from NHANSU");
                foreach (DataRow row in dataset.Rows)
                {
                    db.ThucThi($"insert into TRANGTHAINHANVIEN values('{row["MaNV"]}', '{row["TrinhDo"]}', NULL, NULL)");
                }
            }
        }
        public void CommandUpdateStatus(string status, string manv)
        {
            string sqlStr = $"update TRANGTHAINHANVIEN set TrangThai = '{status}' where MaNV = '{manv}'";
            db.ThucThi(sqlStr);
        }
        public void UpdateStatus(DateTime start, DateTime end)
        {
            string sqlStr = $"select NHANSU.MaNV, NgayBD, NgayKT, MaPB from PHANCONGDUAN right join NHANSU on PHANCONGDUAN.MaNV = NHANSU.MaNV";
            DataTable dataset = db.FormLoad(sqlStr);

            foreach (DataRow dr in dataset.Rows)
            {
                try
                {
                    if ((start <= DateTime.Parse(dr["NgayKT"].ToString()) && start >= DateTime.Parse(dr["NgayBD"].ToString())) || (end <= DateTime.Parse(dr["NgayKT"].ToString()) && end >= DateTime.Parse(dr["NgayBD"].ToString())))
                    {
                        CommandUpdateStatus("Ban", (string)dr["MaNV"]);
                    }
                    else
                    {
                        CommandUpdateStatus("Ranh", (string)dr["MaNV"]);
                    }
                }
                catch
                {
                    CommandUpdateStatus("Ranh", (string)dr["MaNV"]);
                }
            }
        }
        public void DeleteBangTT()
        {
            string sqlStr = $"delete from TRANGTHAINHANVIEN";
            db.ThucThi(sqlStr);
        }

        public DataTable GetNameDept()
        {
            string sqlStr = "SELECT MaPB, TenPB FROM PHONGBAN";
            return db.FormLoad(sqlStr);
        }

        public DataTable GetNameDeptHead()
        {
            string sqlStr = "SELECT CONCAT(MaNV, ' - ', Ten) AS TenNV, MaNV FROM NHANSU WHERE MaCV LIKE 'TP%'";
            return db.FormLoad(sqlStr);
        }
        public DataTable GetNameProd()
        {
            string sqlStr = "SELECT CONCAT(MaDA, ' - ', TenDA) AS TenDA, MaDA FROM DUAN";
            return db.FormLoad(sqlStr);
        }
    }
}
