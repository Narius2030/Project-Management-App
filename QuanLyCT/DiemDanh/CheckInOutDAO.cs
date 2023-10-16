using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCongTy.ChamCong
{
    internal class CheckInOutDAO
    {
        public CheckInOutDAO() { }
        public DBConnection dbconn = new DBConnection();
        public void SubmitSang(CheckInOut cio)
        {   
            //Điểm danh những nhân viên nghỉ
            //...
        }
        public void CapNhatTimeSprint(string manv, DateTime ngay, int count)
        {
            //Cập nhật lại Time Sprint trong UOCLUONG theo công thức: TimeSprint - Capacity per day (trong NHOM)
            //...
        }
    }
}
