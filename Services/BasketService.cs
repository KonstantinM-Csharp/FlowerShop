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

        public static void AddBoquetToBasket(Boquet boquet)
        {
            if (_basket.Any(x => x.Boquet == boquet))
            {
                _basket.FirstOrDefault(x => x.Boquet == boquet).Count++;
            }
            else
            {
                _basket.Add(new BasketItem() { Boquet = boquet, Count = 1 });
            }
        }
        public static void DeleteBoquetFromBasket(Boquet boquet)
        {
            if (_basket.FirstOrDefault(x => x.Boquet == boquet).Count > 1)
            {
                _basket.FirstOrDefault(x => x.Boquet == boquet).Count--;
            }
            else
            {
                _basket.Remove(_basket.FirstOrDefault(x => x.Boquet == boquet));
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
