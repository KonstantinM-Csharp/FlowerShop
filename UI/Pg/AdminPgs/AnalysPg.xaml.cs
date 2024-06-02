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

namespace FlowerShop.UI.Pg.AdminPgs
{
    /// <summary>
    /// Логика взаимодействия для AnalysOg.xaml
    /// </summary>
    public partial class AnalysPg : Page
    {
        public AnalysPg()
        {
            InitializeComponent();
            
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenOrdersPg();
        }
    }
}
