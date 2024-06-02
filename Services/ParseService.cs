using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services
{
    internal class ParseService
    {

        public  static bool ParseStringToStatus(string status)
        {
            if (status == "Активен")
                return true;
            else return false;
        }
        public static string ParseStatusToString(bool status)
        {
            if (status == true)
                return "Активен";
            else return "Неактивен";
        }
    }
}
