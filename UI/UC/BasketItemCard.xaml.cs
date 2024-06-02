using FlowerShop.Data;
using FlowerShop.Data.Models;
using FlowerShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlowerShop.UI.UC
{
    /// <summary>
    /// Логика взаимодействия для BasketItemCard.xaml
    /// </summary>
    public partial class BasketItemCard : UserControl
    {
        public event EventHandler UpdateBasketClicked;
        public BasketItemCard()
        {
            InitializeComponent();
        }
        private void AddToBasket_Clicked(object sender, RoutedEventArgs e)
        {
            var boquet = DataContext as BasketItem;
        
            BasketService.AddBoquetToBasket(boquet.Boquet);;
            UpdateBasketClicked?.Invoke(this, EventArgs.Empty);
        }
        private void RemoveFromBasket_Clicked(object sender, RoutedEventArgs e)
        {
            var boquet = DataContext as BasketItem;
            
                BasketService.DeleteBoquetFromBasket(boquet.Boquet);
            UpdateBasketClicked?.Invoke(this, EventArgs.Empty);

        }
    }
}
