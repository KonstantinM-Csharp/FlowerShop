using FlowerShop.Data;
using FlowerShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services
{
    class BasketService
    {
        private static ObservableCollection<BasketItem> _basket = new ObservableCollection<BasketItem>();

        public static void AddBouquetToBasket(Bouquet bouquet)
        {
            if (_basket.Any(x => x.Bouquet == bouquet))
            {
                _basket.FirstOrDefault(x => x.Bouquet == bouquet).Count++;
            }
            else
            {
                _basket.Add(new BasketItem() { Bouquet = bouquet, Count = 1 });
            }
        }
        public static void DeleteBouquetFromBasket(Bouquet bouquet)
        {
            if (_basket.FirstOrDefault(x => x.Bouquet == bouquet).Count > 1)
            {
                _basket.FirstOrDefault(x => x.Bouquet == bouquet).Count--;
            }
            else
            {
                _basket.Remove(_basket.FirstOrDefault(x => x.Bouquet == bouquet));
            }
        }
        public static ObservableCollection<BasketItem> GetBasket()
        {
            return _basket;
        }
        public static void ClearBasket()
        {
            _basket.Clear();
        }
    }
}
