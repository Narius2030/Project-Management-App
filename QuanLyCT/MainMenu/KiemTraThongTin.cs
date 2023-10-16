using System.Text.RegularExpressions;

namespace QLCongTy.MainMenu
{
    public class KiemTraThongTin
    {
        public bool CheckEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return Check(email, regex);
        }

        public bool CheckSdt(string sdt)
        {
            Regex regex = new Regex(@"^0\d{9}$");
            return Check(sdt, regex);
        }

        public bool Check(string value, Regex regex)
        {
            Match match = regex.Match(value);
            if (match.Success)
                return true;
            else
                return false;
        }
    }
}
