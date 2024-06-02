using FlowerShop.Data;
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
using FlowerShop.UI.UC;
using FlowerShop.Data.Models;
using FlowerShop.Services;

namespace FlowerShop.UI.UC
{
    /// <summary>
    /// Логика взаимодействия для FlowerCard.xaml
    /// </summary>
    public partial class FlowerCard : UserControl
    {
        public FlowerCard()
        {
            InitializeComponent();
        }
        private void AddToBasket_Clicked(object sender, RoutedEventArgs e)
        {
            var bouquet = DataContext as Bouquet;
            if (bouquet != null)
            {
                BasketService.AddBouquetToBasket(bouquet);
                MessageBox.Show("Товар добавлен в корзину.");
                UpdateCount();
            }
        }
        
        private void RemoveFromBasket_Clicked(object sender, RoutedEventArgs e)
        {
            var bouquet = DataContext as Bouquet;
            if (bouquet != null)
            {
                BasketService.DeleteBouquetFromBasket(bouquet);
                MessageBox.Show("Товар удален из корзины.");
                UpdateCount();
            }
        }
        private void UpdateCount()
        {
            var countBouquet = BasketService.GetBasket().FirstOrDefault(x => x.Bouquet == DataContext as Bouquet);
            if (countBouquet == null)
                CountTxtBx.Text = "";
            else CountTxtBx.Text = countBouquet.Count.ToString();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateCount();

        }

    }
}

