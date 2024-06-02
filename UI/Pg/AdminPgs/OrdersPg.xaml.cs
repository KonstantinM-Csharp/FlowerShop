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
using Size = FlowerShop.Data.Models.Size;

namespace FlowerShop.UI.Pg.AdminPgs
{
    /// <summary>
    /// Логика взаимодействия для OrdersPg.xaml
    /// </summary>
    public partial class OrdersPg : Page
    {
        public OrdersPg()
        {
            InitializeComponent();
            LViewBoquets.ItemsSource = FilterService.FilterOrders();
        }
        private void UpdateOrders()
        {
            LViewBoquets.ItemsSource = FilterService.FilterOrders();
        }

        private void SearchBoquetTxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOrders();
        }

        private void SizeBoquetLstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOrders();
        }

        private void BttnBack_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenAdminCatalogPg();
        }

        private void BttnAnalys_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenAnalysPg();
        }
    }
}
