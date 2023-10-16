using System;

namespace QLCongTy.QLDuAn
{
    public class DuAn
    {
        private string mada;
        private string tenda;
        private string mapb;
        private double vondh;
        private string truongda;
        private DateTime ngaybd;
        private DateTime ngaykt;
        private string trangthai;

        public DuAn()
        {

        }
        public DuAn(string mada, string tenda, string mapb, string truongda, DateTime ngaybd, DateTime ngaykt, string trangthai)
        {
            this.Mada = mada;
            this.Tenda = tenda;
            this.Mapb = mapb;
            this.Truongda = truongda;
            this.Ngaybd = ngaybd;
            this.Ngaykt = ngaykt;
            this.Trangthai = trangthai;
        }

        public string Mada { get => mada; set => mada = value; }
        public string Tenda { get => tenda; set => tenda = value; }
        public string Mapb { get => mapb; set => mapb = value; }
        public double Vondh { get => vondh; set => vondh = value; }
        public string Truongda { get => truongda; set => truongda = value; }
        public DateTime Ngaybd { get => ngaybd; set => ngaybd = value; }
        public DateTime Ngaykt { get => ngaykt; set => ngaykt = value; }
        public string Trangthai { get => trangthai; set => trangthai = value; }
    }
}
