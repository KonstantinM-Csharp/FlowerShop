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
    /// Логика взаимодействия для AdminCatalogPg.xaml
    /// </summary>
    public partial class AdminCatalogPg : Page
    {
        public AdminCatalogPg()
        {
            InitializeComponent();
            LViewBoquets.ItemsSource = FlowerMagicEntities.GetContext().Boquets.ToList();
            var sizesBoquets = FlowerMagicEntities.GetContext().Sizes.ToList();
            sizesBoquets.Insert(0, new Size
            {
                Name = "Все размеры"
            });
            SizeBoquetLstBx.ItemsSource = sizesBoquets;
        }
        private void UpdateBoquets()
        {
            List<Boquet> filterBoquets;
            if (SizeBoquetLstBx.SelectedIndex != 0)
                filterBoquets = FilterService.FilterBoquets(SearchBoquetTxtBx.Text, (Size)SizeBoquetLstBx.SelectedItem);
            else
                filterBoquets = FilterService.FilterBoquets(SearchBoquetTxtBx.Text, null);

            if (filterBoquets.Count == 0)
            {

            }

            LViewBoquets.ItemsSource = filterBoquets;
        }

        private void SearchBoquetTxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateBoquets();
        }

        private void SizeBoquetLstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBoquets();
        }



        private void BttnOrdersBoquet_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenOrdersPg();
        }

        private void BttnCreateBoquet_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenAdminEditBoquetPg(null);
        }
    }
}
