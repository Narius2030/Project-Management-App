using QLCongTy.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLCongTy.DAO
{
    public class TruongNhomDao
    {
        DBConnection dbconn = new DBConnection();
        public DataTable laydanhsachnhomtruong(int mada)
        {
            string sqlStr = $"SELECT MaNV, TenNhom FROM TRUONGNHOM WHERE MaDA = {mada} ";
            return dbconn.ExecuteQuery(sqlStr);
        }

        public void xoaTruongNhom(NHOM nhom)
        {
            string sqlStr = $@"UPDATE TRUONGNHOM SET MaNV=NULL WHERE MaDA = {nhom.MaDA} AND TenNhom = '{nhom.TenNhom}'";
            dbconn.ExecuteCommand(sqlStr);
        }

        public DataTable timTruongNhom(NHOM nhom)
        {
<<<<<<< HEAD
            return dbconn.ExecuteQuery($"select *From dbo.sfn_TimTruongNhom('{nhom.TenNhom}',{nhom.MaDA})");
=======
            return dbconn.ExecuteQuery($"select * From dbo.sfn_TimTruongNhom('{nhom.TenNhom}',{nhom.MaDA})");
>>>>>>> d0a845557af48923a7114dddfc282bb9c463914c
        }

        public void DoiTruongNhom(string MaTruongNhomMoi, NHOM nhom)
        {
            string sqlStr = $@"UPDATE TRUONGNHOM SET MaNV = '{MaTruongNhomMoi}' WHERE TenNhom = '{nhom.TenNhom}' AND MaDA = '{nhom.MaDA}'";
            dbconn.ExecuteCommand(sqlStr);
        }
    }
}
