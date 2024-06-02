﻿using FlowerShop.Data.Models;
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
    /// Логика взаимодействия для FlowerCardAdmin.xaml
    /// </summary>
    public partial class FlowerCardAdmin : UserControl
    {
        public FlowerCardAdmin()
        {
            InitializeComponent();
        }
        

        private void EditBoquet_Clicked(object sender, RoutedEventArgs e)
        {
            var boquet = DataContext as Boquet;
            VisibilityWindowsService.OpenAdminEditBoquetPg(boquet);
        }

        private void DeleteBoquet_Clicked(object sender, RoutedEventArgs e)
        {
            var boquet = DataContext as Boquet;
        }
    }
}
