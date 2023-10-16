using System;

namespace QLCongTy.NhanSu
{
    public class Nhansu
    {
        private string maNV;
        private string hoDem;
        private string ten;
        private DateTime ngaySinh;
        private string diaChi;
        private string cccd;
        private string maPB;
        private string maCV;
        private string gioiTinh;
        private string sdt;
        private string email;
        private string trinhdo;

        public Nhansu(string MaNV, string HoDem, string Ten, DateTime NgaySinh, string DiaChi, string CCCD, string MaPB, string MaCV, string GioiTinh, string SDT, string Email, string trinhdo)
        {
            this.maNV = MaNV;
            this.hoDem = HoDem;
            this.ten = Ten;
            this.ngaySinh = NgaySinh;
            this.diaChi = DiaChi;
            this.cccd = CCCD;
            this.maPB = MaPB;
            this.maCV = MaCV;
            this.gioiTinh = GioiTinh;
            this.sdt = SDT;
            this.email = Email;
            Trinhdo = trinhdo;
        }
        public Nhansu()
        {

        }
        public string MaNV
        {
            get { return this.maNV; }
        }
        public string HoDem
        {
            get { return this.hoDem; }
        }
        public string Ten
        {
            get { return this.ten; }
        }
        public DateTime NgaySinh
        {
            get { return this.ngaySinh; }
        }
        public string DiaChi
        {
            get { return this.diaChi; }
        }
        public string CCCD
        {
            get { return this.cccd; }
        }
        public string MaPB
        {
            get { return this.maPB; }
        }
        public string MaCV
        {
            get { return this.maCV; }
        }
        public string GioiTinh
        {
            get { return this.gioiTinh; }
        }
        public string SDT
        {
            get { return this.sdt; }
        }
        public string Email
        {
            get { return this.email; }
        }

        public string Trinhdo { get => trinhdo; set => trinhdo = value; }
    }
}
