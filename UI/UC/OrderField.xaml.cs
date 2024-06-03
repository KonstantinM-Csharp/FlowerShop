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
    /// Логика взаимодействия для OrderField.xaml
    /// </summary>
    public partial class OrderField : UserControl
    {
        public OrderField()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            var order = DataContext as Order;
            if(order != null) 
                    LViewBoquets.ItemsSource = FilterService.GetOrderItemsByOrder(order);
        }
    }
}
