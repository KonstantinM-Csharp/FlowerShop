using FlowerShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services
{
    class FilterService
    {
        public static List<Boquet> FilterBoquets(string nameBoquetSearch = null, Size sizeBoquet = null)
        {
            var boquets = FlowerMagicEntities.GetContext().Boquets.ToList();

            if (!string.IsNullOrEmpty(nameBoquetSearch))
                boquets = boquets.Where(x => x.Name.ToLower().Contains(nameBoquetSearch.ToLower())).ToList();
            if (sizeBoquet != null)
                boquets = boquets.Where(x => x.Size == sizeBoquet).ToList();

            return boquets;
        }
        public static List<Order> FilterOrders()
        {
            var orders = FlowerMagicEntities.GetContext().Orders.ToList();
            return orders;
        }
    }
}
