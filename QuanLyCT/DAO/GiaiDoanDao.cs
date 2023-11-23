using Microsoft.VisualBasic;
using QLCongTy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QLCongTy.DAO
{
    public class GiaiDoanDao
    {
        DBConnection dbC = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);
        public DataTable GetListSprint(int mada,int check)
        {
            if (check == 1)
            {
                return dbC.ExecuteQuery($"Select * From GiaiDoan WHERE MaDA={mada}");
            }
            else
            {
                return dbC.ExecuteQuery($"select * From GiaiDoan Where MaDA={mada} order by GIAIDOAN.MaGiaiDoan desc");
            }
        }
        public void ThemGiaiDoan(GIAIDOAN giaidoan)
        {
            string sqlStr = $@"INSERT INTO GIAIDOAN VALUES ('{giaidoan.MaGiaiDoan}', N'{giaidoan.NoiDung}', '{giaidoan.NgayBD}', '{giaidoan.NgayKT}', {giaidoan.MaDA})";
            dbC.ExecuteCommand(sqlStr);
        }
        public void SuaGiaiDoan(GIAIDOAN giaidoan)
        {
            string sqlStr = $@"UPDATE GIAIDOAN SET NoiDung = N'{giaidoan.NoiDung}', NgayBD = '{giaidoan.NgayBD}', NgayKT = '{giaidoan.NgayKT}', MaDA = { giaidoan.MaDA } WHERE MaGiaiDoan = '{giaidoan.MaGiaiDoan}'";
            dbC.ExecuteCommand(sqlStr);
        }
        public void XoaUocLuong(int maduan,string magiaidoan)
        {
            SqlParameter[] parame = new SqlParameter[]
            {
                new SqlParameter("@magd", SqlDbType.VarChar) { Value = magiaidoan },
                new SqlParameter("@mada",SqlDbType.Int){Value=maduan}
            };
            dbC.ExecuteProcedure("sp_XoaUocLuong_GD_DA", parame);
        }
        public int XoaGiaiDoan(GIAIDOAN giaidoan)
        {
            try
            {
                string sqlStr = $@"DELETE GIAIDOAN WHERE GIAIDOAN.MaGiaiDoan = '{giaidoan.MaGiaiDoan}'";
                dbC.ExecuteCommand(sqlStr);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public Boolean CheckGiaiDoanTruoc(GIAIDOAN gd)
        {
            String MaGiaiDoanTruoc = getMaGiaiDoanTruoc(gd.MaGiaiDoan);
            if (MaGiaiDoanTruoc == "Không có giai đoạn trước đó")
            {
                return true;
            }
            SqlParameter[] parame = new SqlParameter[]
            {
                new SqlParameter("@maduan",SqlDbType.Int){Value=gd.MaDA},
                new SqlParameter("@MaGiaiDoan", SqlDbType.VarChar) { Value = MaGiaiDoanTruoc }
            };
            string result = string.Empty;

            DataTable dt = dbC.ExecuteProcedure("sp_KiemTraGiaiDoanTruoc", parame);

            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["Result"].ToString();
            }
            if (result == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable CheckGiaiDoan(GIAIDOAN gd)
        {
            String MaGiaiDoanTruoc = getMaGiaiDoanTruoc(gd.MaGiaiDoan);
            return dbC.ExecuteQuery($"SELECT * FROM dbo.sfn_KiemTraGiaiDoan({gd.MaDA}, '{MaGiaiDoanTruoc}')");
        }
        public String getMaGiaiDoanTruoc(String maGD)
        {
            int id = Int32.Parse(maGD.Substring(0, 2));
            if (id == 1)
            {
                return "Không có giai đoạn trước đó";
            }
            else
            {
                id--;
                string prefix = id.ToString("D2");
                string suffix = maGD.Substring(2);
                return prefix + suffix;
            }
        }
    }
}
