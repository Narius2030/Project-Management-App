using QLCongTy.DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QLCongTy.DAO
{
    public class CongViecDao
    {
        DBConnection dbC = new DBConnection(fMainMenu.MaNV, fMainMenu.MatKhau);
        public DataTable GetListJob(int mada, string magiaidoan)
        {
            return dbC.ExecuteQuery($"select MaCV,TrangThai,CVTienQuyet,TenCV,TienDo,TenNhom From CONGVIEC where CONGVIEC.MaDA={mada} and CONGVIEC.MaGiaiDoan='{magiaidoan}'");
        }
        public Double UpdateProgress(int macongviec, string magiaidoan)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCV",SqlDbType.Int){Value =macongviec},
                new SqlParameter("@magiaidoan ",SqlDbType.VarChar,20){Value =magiaidoan},
                new SqlParameter("@ketqua",SqlDbType.Real){Direction = ParameterDirection.Output}
            };
            dbC.ExecuteProcedure("sp_TinhTienDoCv", parameters);
            double ketqua = Convert.ToDouble(parameters[2].Value);
            return ketqua;
        }
        public string UpdateStatus(int macongviec)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@macongviec",SqlDbType.Int){Value=macongviec},
                new SqlParameter("@trangthai",SqlDbType.VarChar,20){Direction=ParameterDirection.Output}
            };
            dbC.ExecuteProcedure("sp_UpdateTrangThai", parameters);
            string ketqua = Convert.ToString(parameters[1].Value);
            return ketqua;
        }
        public void AddJob(CONGVIEC cv)
        {
            string sqlStr;

            if (cv.CVTienQuyet == null)
            {
                sqlStr = $@"INSERT INTO CONGVIEC VALUES ('{cv.TrangThai}', NULL, N'{cv.TenCV}', {cv.TienDo}, '{cv.TenNhom}', {cv.MaDA}, '{cv.MaGiaiDoan}')";
            }
            else
            {
                sqlStr = $@"INSERT INTO CONGVIEC VALUES ('{cv.TrangThai}', {cv.CVTienQuyet}, N'{cv.TenCV}', {cv.TienDo}, '{cv.TenNhom}', {cv.MaDA}, '{cv.MaGiaiDoan}')";
            }
            dbC.ExecuteCommand(sqlStr);
        }
        public int RemoveJob(CONGVIEC cv)
        {
            try
            {
                string sqlStr = $@"DELETE CONGVIEC WHERE CONGVIEC.MaCV = {cv.MaCV}";
                dbC.ExecuteCommand(sqlStr);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public void UpdateJob(CONGVIEC cv)
        {
            string sqlStr = $@"UPDATE CONGVIEC SET TrangThai='{cv.TrangThai}', CVTienQuyet={cv.CVTienQuyet}, TenCV=N'{cv.TenCV}', TienDo={cv.TienDo}, TenNhom='{cv.TenNhom}', MaDA={cv.MaDA}, MaGiaiDoan='{cv.MaGiaiDoan}' WHERE MaCV = {cv.MaCV}";
            if (cv.CVTienQuyet == null)
            {
                sqlStr = $@"UPDATE CONGVIEC SET TrangThai='{cv.TrangThai}', CVTienQuyet=NULL, TenCV=N'{cv.TenCV}', TienDo={cv.TienDo}, TenNhom='{cv.TenNhom}', MaDA={cv.MaDA}, MaGiaiDoan='{cv.MaGiaiDoan}' WHERE MaCV = {cv.MaCV}";
            }
            dbC.ExecuteCommand(sqlStr);
        }
        public void KiemTraCongViecTienQuyet(CONGVIEC cv)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@macongviec",SqlDbType.Int){Value=cv.MaCV}
            };
            dbC.ExecuteProcedure("sp_KiemTraCongViec", parameters);
        }
    }
}
