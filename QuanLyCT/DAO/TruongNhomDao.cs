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
            //string sqlStr = $@"UPDATE TRUONGNHOM SET MaNV=NULL WHERE MaDA = {nhom.MaDA} AND TenNhom = '{nhom.TenNhom}'";
            //dbconn.ExecuteCommand(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = nhom.MaDA},
                new SqlParameter("@tennhom",SqlDbType.NVarChar, 20) {Value = nhom.TenNhom}
            };
            dbconn.ExecuteProcedure("sp_xoaTruongNhomDuAn", parameters);
        }

        public DataTable timTruongNhom(NHOM nhom)
        {
            return dbconn.ExecuteQuery($"select * From dbo.sfn_TimTruongNhom('{nhom.TenNhom}',{nhom.MaDA})");
        }

        public void DoiTruongNhom(string MaTruongNhomMoi, NHOM nhom)
        {
            //string sqlStr = $@"UPDATE TRUONGNHOM SET MaNV = '{MaTruongNhomMoi}' WHERE TenNhom = '{nhom.TenNhom}' AND MaDA = '{nhom.MaDA}'";
            //dbconn.ExecuteCommand(sqlStr);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@mada",SqlDbType.Int) {Value = nhom.MaDA},
                new SqlParameter("@tennhom",SqlDbType.NVarChar, 20) {Value = nhom.TenNhom},
                new SqlParameter("@truongnhommoi",SqlDbType.VarChar, 10) {Value = MaTruongNhomMoi}
            };
            dbconn.ExecuteProcedure("sp_doiTruongNhomDuAn", parameters);
        }
    }
}
