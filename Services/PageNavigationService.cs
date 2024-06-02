using FlowerShop.Data.Models;
using FlowerShop.UI.Pg;
using FlowerShop.UI.Pg.AdminPgs;
using FlowerShop.UI.Pg.ClientPgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlowerShop.Services
{
    class PageNavigationService
    {
        private static BasketPg basketPg = new BasketPg();
        private static CatalogPg catalogPg= new CatalogPg();
        private static AdminCatalogPg adminCatalogPg = new AdminCatalogPg();
        private static EditBouquetPg editBouquetPg = new EditBouquetPg();
        private static CheckPg checkPg = new CheckPg();
        private static OrdersPg ordersPg = new OrdersPg();
        private static AnalysPg analysPg = new AnalysPg();
        public static void NavigateToBasket(Frame frame)
        {
            frame.Navigate(basketPg);
        }
        public static void NavigateToCatalog(Frame frame)
        {
            frame.Navigate(catalogPg);
        }
        public static void NavigateToAdminCatalog(Frame frame)
        {
            frame.Navigate(adminCatalogPg);
        }
        public static void NavigateToEditBouquetPg(Frame frame, Bouquet Bouquet)
        {
            editBouquetPg.SetBouquet(Bouquet);
            frame.Navigate(editBouquetPg);
        }
        public static void NavigateToCheckPg(Frame frame)
        {
            frame.Navigate(checkPg);
        }
        public static void NavigateToOrders(Frame frame)
        {
            frame.Navigate(ordersPg);
        }
        public static void NavigateToAnalys(Frame frame)
        {
            frame.Navigate(analysPg);
        }
    }
}
