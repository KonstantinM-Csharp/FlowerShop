using FlowerShop.Data.Models;
using FlowerShop.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Serialization;

namespace FlowerShop.UI.Pg.AdminPgs
{
    /// <summary>
    /// Логика взаимодействия для EditBoquetPg.xaml
    /// </summary>
    public partial class EditBoquetPg : Page
    {
        private Boquet _currentBoquet;
        private int[] stars = { 1, 2, 3, 4, 5 };
        public EditBoquetPg()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentBoquet.Name))
                errors.AppendLine("Укажите название букета.");
            else
            {
                Regex regex = new Regex(@"[^а-яА-Яa-zA-Z\s""]");
                if (regex.Match(NameTxtBx.Text).Success)
                    errors.AppendLine("Название букета не должно содержать небуквенных данных.");
            }
            string priceText = PriceTxtBx.Text.Replace('.', ',');
            if (string.IsNullOrEmpty(priceText))
                errors.AppendLine("Укажите цену букета.");
            if (!decimal.TryParse(priceText, out decimal price))
                errors.AppendLine("Неверный формат цены букета.");
            else if (price <= 0)
                errors.AppendLine("Цена не может быть меньше и равна нулю.");

            if (string.IsNullOrEmpty(CountTxtBx.Text))
                errors.AppendLine("Укажите цену букета.");
            if (!int.TryParse(CountTxtBx.Text, out int instock))
                errors.AppendLine("Неверный формат количества букетов.");
            if (instock <0)
                errors.AppendLine("Количество не может быть меньше нуля."); 
            if (SizeCmBx.SelectedItem==null)
                errors.AppendLine("Выберите размер букета.");

            _currentBoquet.Status = ParseStringToStatus(StatusCmBx.SelectedItem.ToString());
            _currentBoquet.Size = SizeCmBx.SelectedItem as Data.Models.Size;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentBoquet.Id == 0)
            {
                FlowerMagicEntities.GetContext().Boquets.Add(_currentBoquet);
            }

            try
            {
                FlowerMagicEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SizeCmBx.ItemsSource = FlowerMagicEntities.GetContext().Sizes.ToList();
            SizeCmBx.SelectedItem = _currentBoquet.Size;
            StatusCmBx.ItemsSource = new string[] { "Активен", "Неактивен" };
            StatusCmBx.SelectedItem = ParseStatusToString(_currentBoquet.Status);
            UpdatePhoto();
        }
        public void SetBoquet(Boquet boquet)
        {
            if (boquet == null)
                _currentBoquet = new Boquet();
            else
                _currentBoquet = boquet;
            DataContext = _currentBoquet;
        }

        private bool ParseStringToStatus(string status)
        {
            if (status == "Активен")
                return true;
            else return false;
        }
        private string ParseStatusToString(bool status)
        {
            if (status == true)
                return "Активен";
            else return "Неактивен";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            VisibilityWindowsService.OpenAdminCatalogPg();
        }

        private void btnPhotoBoquet_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                var photoData = File.ReadAllBytes(openFileDialog.FileName);
                _currentBoquet.Photo = photoData;
            }
            UpdatePhoto();
        }
        private void UpdatePhoto()
        {
            if (_currentBoquet.Photo == null)
            {
                PhotoBoquet.Source = null;
                return;
            }

            try
            {
                // Create a BitmapImage to display the selected photo
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(_currentBoquet.Photo);
                bitmap.EndInit();

                // Assuming you have an Image control named PhotoBoquet to display the photo
                PhotoBoquet.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying image: {ex.Message}");
            }
        }
    }
}
