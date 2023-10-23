using QLCongTy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCongTy.DAO
{
    public class NhomDao
    {
        DBConnection dbconn = new DBConnection();
        public void ThemThanhVien(NHOM nhom)
        {
            string sqlStr = $@"INSERT INTO NHOM VALUES('{nhom.MaNV}', '{nhom.TenNhom}', {nhom.MaDA}, {nhom.SoGioMotNg})";
            dbconn.ExecuteCommand(sqlStr);
        }
        public DataTable laydanhsachnhom(int mada)
        {
            string sqlStr = $"select distinct (TenNhom) From NHOM where NHOM.MaDA={mada} ";
            return dbconn.ExecuteQuery(sqlStr);
        }
    }
}
