using QLCongTy.DTO;
using System.Data;
using System.Data.SqlClient;

namespace QLCongTy.DAO
{
    public class DuAnDao
    {
        DBConnection dbconn = new DBConnection();
        public DataTable getProjectList()
        {
            string sqlStr = "SELECT * FROM DUAN";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable getNhanLucDA(int mada)
        {
            //Funtion return a table
            string sqlStr = $@"SELECT 
	                            nv.MaNV, CONCAT(nv.HovaTenDem,' ',nv.Ten) AS HoTen, n.TenNhom, nv.Email
                            FROM NHANVIEN nv
                            JOIN NHOM n ON n.MaNV = nv.MaNV
                            WHERE n.MaDA = {mada}";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable getNhanLucCty()
        {
            // View
            string sqlStr = $@"SELECT
	                            MaNV, CONCAT(HovaTenDem,' ',Ten), Levels, Email
                            FROM NHANVIEN";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable FilterLevel(string level)
        {
            // Function return a table
            string sqlStr = $@"SELECT
	                            MaNV, CONCAT(HovaTenDem,' ',Ten), Levels, Email
                            FROM NHANVIEN WHERE Levels='{level}'";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public DataTable DSDuAn()
        {
            string sqlStr = $"SELECT CONCAT(MaDA, ' - ', TenDA) as TenDA, MaDA FROM DUAN";
            return dbconn.ExecuteQuery(sqlStr);
        }
        public void insertDuAn(DUAN da)
        {
            //string sqlStr = $@"INSERT INTO DUAN(TenDA, TienDo, NgayKT, NgayBD, ChiPhi, TrangThai, MaPM) VALUES(N'{da.TenDA}', {da.TienDo}, '{da.NgayKT}', '{da.NgayBD}', '{da.ChiPhi}', '{da.TrangThai}', '{da.MaPM}')";
            //dbconn.ExecuteCommand(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@tenda",SqlDbType.NVarChar, 50) {Value = da.TenDA},
                new SqlParameter("@tiendo ",SqlDbType.Real) {Value = da.TienDo},
                new SqlParameter("@ngaykt",SqlDbType.Date) {Value = da.NgayKT},
                new SqlParameter("@ngaybd",SqlDbType.Date) {Value = da.NgayBD},
                new SqlParameter("@chiphi",SqlDbType.NVarChar, 30) {Value = da.ChiPhi},
                new SqlParameter("@trangthai",SqlDbType.NVarChar, 30) {Value = da.TrangThai},
                new SqlParameter("@mapm",SqlDbType.VarChar, 10) {Value = da.MaPM}
            };
            dbconn.ExecuteProcedure("sp_themDuAn", parameters);
        }
        public void editDuAn(DUAN da)
        {
            //string sqlStr = $@"UPDATE DUAN SET TenDA=N'{da.TenDA}', TienDo={da.TienDo}, NgayKT='{da.NgayKT}', NgayBD='{da.NgayBD}', ChiPhi='{da.ChiPhi}', TrangThai='{da.TrangThai}', MaPM='{da.MaPM}' WHERE MaDA={da.MaDA}";
            //dbconn.ExecuteCommand(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = da.MaDA},
                new SqlParameter("@tenda",SqlDbType.NVarChar, 50) {Value = da.TenDA},
                new SqlParameter("@tiendo ",SqlDbType.Real) {Value = da.TienDo},
                new SqlParameter("@ngaykt",SqlDbType.Date) {Value = da.NgayKT},
                new SqlParameter("@ngaybd",SqlDbType.Date) {Value = da.NgayBD},
                new SqlParameter("@chiphi",SqlDbType.NVarChar, 30) {Value = da.ChiPhi},
                new SqlParameter("@trangthai",SqlDbType.NVarChar, 30) {Value = da.TrangThai},
                new SqlParameter("@mapm",SqlDbType.VarChar, 10) {Value = da.MaPM}
            };
            dbconn.ExecuteProcedure("sp_capnhatDuAn", parameters);
        }
        public void removeDuAn(int mada)
        {
            //string sqlStr = $@"DELETE FROM DUAN WHERE MaDA={mada}";
            //dbconn.ExecuteCommand(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = mada}
            };
            dbconn.ExecuteProcedure("sp_xoaDuAn", parameters);
        }
        public void removeThanhVienDA(NHOM nhom)
        {
            //string sqlStr = $@"DELETE FROM NHOM WHERE MaNV='{nhom.MaNV}' AND MaDA={nhom.MaDA} AND TenNhom='{nhom.TenNhom}'";
            //dbconn.ExecuteCommand(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = nhom.MaDA},
                new SqlParameter("@tennhom",SqlDbType.NVarChar, 20) {Value = nhom.TenNhom},
                new SqlParameter("@manv",SqlDbType.VarChar, 10) {Value = nhom.MaNV},
            };
            dbconn.ExecuteProcedure("sp_xoaNhanVienDuAn", parameters);
        }
        public void removeNhomDA(TRUONGNHOM tn)
        {
            //string sqlStr = $@"DELETE FROM TRUONGNHOM WHERE TenNhom='{tn.TenNhom}' AND MaDA={tn.MaDA}";
            //dbconn.ExecuteCommand(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = tn.MaDA},
                new SqlParameter("@tennhom",SqlDbType.NVarChar, 20) {Value = tn.TenNhom}
            };
            dbconn.ExecuteProcedure("sp_xoaNhomDuAn", parameters);
        }
    }
}
