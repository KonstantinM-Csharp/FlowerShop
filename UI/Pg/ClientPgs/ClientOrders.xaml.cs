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

namespace FlowerShop.UI.Pg.ClientPgs
{
    /// <summary>
    /// Логика взаимодействия для ClientOrders.xaml
    /// </summary>
    public partial class ClientOrders : Page
    {
        public ClientOrders()
        {
            InitializeComponent();
        }

        private void UpdateOrders()
        {
            LViewBoquets.ItemsSource = FilterService.GetOrders(AutorizationService.User);
        }

        private void BttnBack_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenCatalogPg();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateOrders();
        }
    }
}
