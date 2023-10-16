using System.Collections.Generic;
using System.Data;

namespace QLCongTy.QLDuAn
{
    internal class TienDoDAO
    {
        public TienDoDAO() { }
        DBConnection dbconn = new DBConnection();

        public DataTable LayDanhSach(string mada)
        {
            string sqlStr = $@"select MaDA, Ten, CongViec, NgayBD, NgayKT, TienDo 
                                from PHANCONGDUAN join NHANSU on PHANCONGDUAN.MaNV = NHANSU.MaNV WHERE MaDA = '{mada}'";
            return dbconn.FormLoad(sqlStr);
        }

        //Lấy danh sách nhân viên tham gia dự án
        public List<List<string>> LayDsNhanvien(string mada)
        {
            string sqlStr = $@"select NHANSU.MaNV, Ten, CongViec, NgayBD, NgayKT, TienDo 
                            from PHANCONGDUAN join NHANSU on PHANCONGDUAN.MaNV = NHANSU.MaNV
                            where MaDA = '{mada}'";
            DataTable dt = dbconn.FormLoad(sqlStr);

            List<List<string>> lstparent = new List<List<string>>();
            List<string> lst;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lst = new List<string>();
                lst.Add(dt.Rows[i]["Ten"].ToString());
                lst.Add(dt.Rows[i]["CongViec"].ToString());
                lst.Add(dt.Rows[i]["TienDo"].ToString());

                //Add into dict
                lstparent.Add(lst);
            }
            return lstparent;
        }

        //  0< <100 --> Đang thực hiện AND NgayKT > Ngày hiện tại
        public int LaySlDangThucHien(string mada)
        {
            string sqlStr = $@"select count(*) as so_luong from PHANCONGDUAN where TienDo < 100 and TienDo > 0 and NgayKT > GETDATE() and MaDA = '{mada}'";
            DataTable dt = dbconn.FormLoad(sqlStr);
            return int.Parse(dt.Rows[0]["so_luong"].ToString());
        }

        // =100 --> đã hoàn thành
        public int LaySLHoanThanh(string mada)
        {
            string sqlStr = $@"select count(*) as so_luong from PHANCONGDUAN where TienDo = 100 and MaDA = '{mada}'";
            DataTable dt = dbconn.FormLoad(sqlStr);
            return int.Parse(dt.Rows[0]["so_luong"].ToString());
        }

        // =0 --> chưa thực hiện
        public int LaySLChuaThucHien(string mada)
        {
            string sqlStr = $@"select count(*) as so_luong from PHANCONGDUAN where TienDo = 0 and NgayKT > GETDATE() and MaDA = '{mada}'";
            DataTable dt = dbconn.FormLoad(sqlStr);
            return int.Parse(dt.Rows[0]["so_luong"].ToString());
        }

        // Ngày hiện tại > Ngày Kết thức AND TienDo < 100 ---> Quá hạn
        public int LaySLQuaHan(string mada)
        {
            string sqlStr = $@"select count(*) as so_luong from PHANCONGDUAN where TienDo < 100 and NgayKT < GETDATE() and MaDA ='{mada}'";
            DataTable dt = dbconn.FormLoad(sqlStr);
            return int.Parse(dt.Rows[0]["so_luong"].ToString());
        }

        //Lấy tiến độ dự án
        public int LayTienDoDA(string mada)
        {
            try
            {
                string sqlStr = $@"select Tiendo from DUAN where MaDA ='{mada}'";
                DataTable dt = dbconn.FormLoad(sqlStr);
                return int.Parse(dt.Rows[0]["Tiendo"].ToString());
            }
            catch
            {
                return 0;
            }
        }
    }
}
