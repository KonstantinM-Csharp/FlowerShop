using FlowerShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Data
{
    class BasketItem
    {
        public Boquet Boquet { get; set; }
        public int Count { get; set; }
        public decimal Cost { get { return Boquet.Price * Count; } }
    }
}
