using QLCongTy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void insertDuAn(DUAN da)
        {
            string sqlStr = $@"INSERT INTO DUAN(TenDA, TienDo, NgayKT, NgayBD, ChiPhi, TrangThai, MaPM) VALUES(N'{da.TenDA}', {da.TienDo}, '{da.NgayKT}', '{da.NgayBD}', '{da.ChiPhi}', '{da.TrangThai}', '{da.MaPM}')";
            dbconn.ExecuteCommand(sqlStr);
        }
        public void editDuAn(DUAN da)
        {
            string sqlStr = $@"UPDATE DUAN SET TenDA=N'{da.TenDA}', TienDo={da.TienDo}, NgayKT='{da.NgayKT}', NgayBD='{da.NgayBD}', ChiPhi='{da.ChiPhi}', TrangThai='{da.TrangThai}', MaPM='{da.MaPM}' WHERE MaDA={da.MaDA}";
            dbconn.ExecuteCommand(sqlStr);
        }
        public void removeDuAn(int mada)
        {
            string sqlStr = $@"DELETE FROM DUAN WHERE MaDA={mada}";
            dbconn.ExecuteCommand(sqlStr);
        }
        public void removeThanhVienDA(NHOM nhom)
        {
            string sqlStr = $@"DELETE FROM NHOM WHERE MaNV='{nhom.MaNV}' AND MaDA={nhom.MaDA} AND TenNhom='{nhom.TenNhom}'";
            dbconn.ExecuteCommand(sqlStr);
        }
        public void removeTruongNhomDA(TRUONGNHOM tn)
        {
            string sqlStr = $@"DELETE FROM TRUONGNHOM WHERE TenNhom='{tn.TenNhom}' AND MaDA={tn.MaDA}";
            dbconn.ExecuteCommand(sqlStr);
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
    }
}
