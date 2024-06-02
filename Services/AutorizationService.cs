using FlowerShop.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlowerShop.Services
{
    internal class AutorizationService
    {
        public static User User { get; private set; }
        public static UserRole Role { get; private set; }
        public static Order LastOrder { get; set; }

        public static bool LogIn(string login, string password)
        {
            var userRole = FlowerMagicEntities.GetContext().UserRoles.FirstOrDefault(x => x.Login == login);
            var userId = userRole.User.Id;
            if (userRole == null)
            {
                return false;
            }
            if (userRole.Login == login && userRole.Password == password)
            {
                Role = userRole;
                User = FlowerMagicEntities.GetContext().Users.FirstOrDefault(x=>x.Id==userId);
                return true;
            }
            else return false;
        }
    }
}
