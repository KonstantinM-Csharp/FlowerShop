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
using System.Windows.Shapes;
using FlowerShop.Data;
using FlowerShop.Data.Models;
using FlowerShop.Services;
using FlowerShop.UI.Pg;
using FlowerShop.UI.UC;

namespace FlowerShop.UI.Wnd
{
    /// <summary>
    /// Логика взаимодействия для UserWnd.xaml
    /// </summary>
    public partial class UserWnd : Window
	{
        
        // Коллекция для хранения товаров в корзине. С помощью static все карточки могут изменять корзину.
        private static List<Boquet> cartItems = new List<Boquet>();
		public UserWnd()
        {
            InitializeComponent();
		}
    }
}
