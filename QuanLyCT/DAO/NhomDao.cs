using QLCongTy.DTO;
using System;
using System.Collections.Generic;
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
    }
}
