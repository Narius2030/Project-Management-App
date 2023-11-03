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
            string sqlStr = $@"DELETE FROM TRUONGNHOM WHERE MaDA = {nhom.MaDA} AND TenNhom = '{nhom.TenNhom}' AND MaNV = '{nhom.MaNV}'";
            dbconn.ExecuteCommand(sqlStr);
        }

        public DataTable timTruongNhom(NHOM nhom)
        {
            return dbconn.ExecuteQuery($"select *From dbo.sfn_TimTruongNhom('{nhom.TenNhom}',{nhom.MaDA})");
        }

        public void DoiTruongNhom(string MaTruongNhomMoi, string MaTruongNhomCu, NHOM nhom)
        {
            string sqlStr = $@"UPDATE TRUONGNHOM SET MaNV = '{MaTruongNhomMoi}' WHERE MaNV = '{MaTruongNhomCu}' AND TenNhom = '{nhom.TenNhom}' AND MaDA = '{nhom.MaDA}'";
            dbconn.ExecuteCommand(sqlStr);
        }
    }
}
