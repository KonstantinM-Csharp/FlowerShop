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

namespace FlowerShop.UI.Pg.ClientPgs
{
    /// <summary>
    /// Логика взаимодействия для CatalogPg.xaml
    /// </summary>
    public partial class CatalogPg : Page
    {

        public CatalogPg()
        {
            InitializeComponent();
            LViewBouquets.ItemsSource = FlowerMagicEntities.GetContext().Bouquets.ToList();
            var sizesBouquets = FlowerMagicEntities.GetContext().Sizes.ToList();
            sizesBouquets.Insert(0, new Size
            {
                Name = "Все размеры"
            });
            SizeBouquetLstBx.ItemsSource = sizesBouquets;
        }


        private void UpdateBouquets()
        {
            List<Bouquet> filterBouquets;
            if(SizeBouquetLstBx.SelectedIndex!=0)
               filterBouquets =  FilterService.FilterBouquets(SearchBouquetTxtBx.Text, (Size)SizeBouquetLstBx.SelectedItem);
            else
               filterBouquets = FilterService.FilterBouquets(SearchBouquetTxtBx.Text, null);

            filterBouquets = filterBouquets.Where(x=>x.Status==true).ToList();

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

        private void BttnOpenBasket_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenBasketPg();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateBouquets();
        }
    }
}
