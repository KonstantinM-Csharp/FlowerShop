using FlowerShop.Data;
using FlowerShop.Data.Models;
using FlowerShop.Services;
using FlowerShop.UI.UC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FlowerShop.UI.Pg.ClientPgs
{
    /// <summary>
    /// Логика взаимодействия для BasketPg.xaml
    /// </summary>
    public partial class BasketPg : Page
    {
        private ObservableCollection<BasketItem> _basketItems;
        private Order _order;
        public decimal CommonPrice { get => _basketItems.Select(x => x.Cost).Sum(); set { } }
        public BasketPg()
        {
            InitializeComponent();
        }


        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TakeOrder();
                TakeOrderItems();
                AutorizationService.LastOrder = _order;
                MessageBox.Show("Заказ успешно оформлен.");
                VisibilityWindowsService.OpenCheckPg();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void TakeOrder()
        {
            _order = new Order();
            _order.Date = DateTime.Now;
            _order.UserId = AutorizationService.User.Id;
            _order.TotalAmount = _basketItems.Select(x => x.Cost).Sum();
            FlowerMagicEntities.GetContext().Orders.Add(_order);
            FlowerMagicEntities.GetContext().SaveChanges();
        }
        private void TakeOrderItems()
        {
            try
            {
                foreach (var item in _basketItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = _order.Id;
                    orderItem.BouquetId = item.Bouquet.Id;
                    orderItem.Price = item.Bouquet.Price;
                    orderItem.Quantity = item.Count;
                    FlowerMagicEntities.GetContext().OrderItems.Add(orderItem);
                    FlowerMagicEntities.GetContext().SaveChanges();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BackBttn_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenCatalogPg();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadBasketItems();

        }
        private void LoadBasketItems()
        {
            _basketItems = BasketService.GetBasket();
            CommonPrice = _basketItems.Select(x => x.Cost).Sum();

            LVBasket.ItemsSource = null;

            LVBasket.ItemsSource = _basketItems;

            CommonCostBasketTxtBx.Text = CommonPrice.ToString();
        }

        private void BasketItemCard_UpdateData(object sender, EventArgs e)
        {
            // Ваш метод для обновления данных
            LoadBasketItems();
        }

    }
}
