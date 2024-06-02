using FlowerShop.Data;
using FlowerShop.Data.Models;
using FlowerShop.UI.Wnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services
{
    internal class VisibilityWindowsService
    {
        private static UserWnd _userWnd;
        private static AutorizationWnd _auturizationWnd ;
        
        public static void OpenAutorizationWindow()
        {
            _auturizationWnd =new AutorizationWnd();
            _auturizationWnd.Show();
        }
        
        public static void OpenUserWindowForUser()
        {
            _userWnd = new UserWnd();
            _userWnd.Show();
            OpenCatalogPg();
        }
        public static void OpenUserWindowForAdmin()
        {
            _userWnd = new UserWnd();
            _userWnd.Show();
            OpenAdminCatalogPg();
        }
        public static void OpenBasketPg()
        {
            PageNavigationService.NavigateToBasket(_userWnd.MainFrame);
        }
        public static void OpenCatalogPg()
        {
            PageNavigationService.NavigateToCatalog(_userWnd.MainFrame);
        }
        public static void OpenCheckPg()
        {
            PageNavigationService.NavigateToCheckPg(_userWnd.MainFrame);
        }
        public static void OpenAdminCatalogPg()
        {
            PageNavigationService.NavigateToAdminCatalog(_userWnd.MainFrame);
        }
        public static void OpenAdminEditBouquetPg(Bouquet bouquet)
        {
            PageNavigationService.NavigateToEditBouquetPg(_userWnd.MainFrame, bouquet);
        }
        public static void OpenOrdersPg()
        {
            PageNavigationService.NavigateToOrders(_userWnd.MainFrame);
        }
        public static void OpenAnalysPg()
        {
            PageNavigationService.NavigateToAnalys(_userWnd.MainFrame);
        }
    }
}
