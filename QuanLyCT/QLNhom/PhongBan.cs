namespace QLCongTy.QLPhongBan
{
    internal class PhongBan
    {
        string mapb;
        string tenpb;
        string matp;       


        public PhongBan(string mapb, string tenpb, string matp)
        {
            this.Mapb = mapb;
            this.Tenpb = tenpb;
            this.Matp = matp;        
        }

        public string Mapb { get => mapb; set => mapb = value; }
        public string Tenpb { get => tenpb; set => tenpb = value; }
        public string Matp { get => matp; set => matp = value; }
    }
}
