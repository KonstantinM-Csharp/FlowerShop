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
        private string[] statusBouquet = new string[] { "Все", "Активен", "Неактивен" };
        public AdminCatalogPg()
        {
            InitializeComponent();
            LViewBouquets.ItemsSource = FlowerMagicEntities.GetContext().Bouquets.ToList();
            var sizesBouquets = FlowerMagicEntities.GetContext().Sizes.ToList();
            sizesBouquets.Insert(0, new Size
            {
                Name = "Все размеры"
            });
            StatusLstBx.ItemsSource = statusBouquet;
            StatusLstBx.SelectedIndex = 1;
            SizeBouquetLstBx.SelectedIndex = 0;
            SizeBouquetLstBx.ItemsSource = sizesBouquets;
        }
        private void UpdateBouquets()
        {
            List<Bouquet> filterBouquets = new List<Bouquet>();
            if (SizeBouquetLstBx.SelectedIndex != 0&& StatusLstBx.SelectedIndex != 0)
                filterBouquets = FilterService.FilterBouquets(SearchBouquetTxtBx.Text, (Size)SizeBouquetLstBx.SelectedItem, StatusLstBx.SelectedValue.ToString());
            else
            {
                if (SizeBouquetLstBx.SelectedIndex == 0 && StatusLstBx.SelectedIndex == 0)
                    filterBouquets = FilterService.FilterBouquets(SearchBouquetTxtBx.Text, null, null);
                if (SizeBouquetLstBx.SelectedIndex != 0 && StatusLstBx.SelectedIndex == 0)
                    filterBouquets = FilterService.FilterBouquets(SearchBouquetTxtBx.Text, (Size)SizeBouquetLstBx.SelectedItem, null);
                if (SizeBouquetLstBx.SelectedIndex == 0 && StatusLstBx.SelectedIndex != 0)
                    filterBouquets = FilterService.FilterBouquets(SearchBouquetTxtBx.Text, null, StatusLstBx.SelectedValue.ToString());
            }
            

            LViewBouquets.ItemsSource = filterBouquets;
        }

        private void SearchBouquetTxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateBouquets();
        }

        private void SizeBouquetLstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBouquets();
        }



        private void BttnOrdersBouquet_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenOrdersPg();
        }

        private void BttnCreateBouquet_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenAdminEditBouquetPg(null);
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateBouquets();
        }

        private void StatusLstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBouquets();
        }
    }
}
