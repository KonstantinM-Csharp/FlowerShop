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
        public static List<Bouquet> FilterBouquets(string nameBouquetSearch = null, Size sizeBouquet = null, string status = null)
        {
            var bouquets = FlowerMagicEntities.GetContext().Bouquets.ToList();

            if (!string.IsNullOrEmpty(nameBouquetSearch))
                bouquets = bouquets.Where(x => x.Name.ToLower().Contains(nameBouquetSearch.ToLower())).ToList();
            if (sizeBouquet != null)
                bouquets = bouquets.Where(x => x.Size == sizeBouquet).ToList();
            if (status != null)
                bouquets = bouquets.Where(x => x.Status == ParseService.ParseStringToStatus(status)).ToList();
            return bouquets;
        }
        public static List<Order> FilterOrders()
        {
            var orders = FlowerMagicEntities.GetContext().Orders.ToList();
            return orders;
        }

        public static List<OrderItem> GetOrderItemsByOrder(Order order)
        {
            return FlowerMagicEntities.GetContext().OrderItems.Where(x => x.Order.Id == order.Id).ToList();

        }
    }
}
