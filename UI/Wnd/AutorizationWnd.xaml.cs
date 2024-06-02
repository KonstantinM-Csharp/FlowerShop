
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
using System.Windows.Threading;

namespace FlowerShop.UI.Wnd
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AutorizationWnd : Window
    {
        private int loginAttempts = 0;
        private const int maxLoginAttempts = 3;
        private DispatcherTimer timer;

        public AutorizationWnd()
        {
            InitializeComponent();
            enterBttn.IsEnabled = false;

            pswrdBxPassword.IsEnabled = false;
            chckPass.IsEnabled = false;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(15);
            timer.Tick += Timer_Tick;
        }
        private void ShowPasswordChecked(object sender, RoutedEventArgs e)
        {
            tbPassword.Text = pswrdBxPassword.Password;
            tbPassword.Visibility = Visibility.Visible;
            pswrdBxPassword.Visibility = Visibility.Hidden;
        }
        private void ShowPasswordUnchecked(object sender, RoutedEventArgs e)
        {
            pswrdBxPassword.Password = tbPassword.Text;
            tbPassword.Visibility = Visibility.Hidden;
            pswrdBxPassword.Visibility = Visibility.Visible;
        }

        private void LogInTxtBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(logInTxtBx.Text) /*|| string.IsNullOrWhiteSpace(pswrdBxPassword.Password)*/)
            {
                enterBttn.IsEnabled = false;
                pswrdBxPassword.Password = "";
            }
            else
            {    /* enterBttn.IsEnabled = true;*/
                pswrdBxPassword.IsEnabled = true;
                }
        }
        private void EnterBttn_Click(object sender, RoutedEventArgs e)
        {
            if (pswrdBxPassword.Password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать не менее 6 знаков. Попробуйте ещё раз.", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
                loginAttempts++;
            }
            else
            {
                if (AutorizationService.LogIn(logInTxtBx.Text, pswrdBxPassword.Password))
                {

                    if (AutorizationService.Role.RoleCode=="ADM")
                    {
                        MessageBoxResult result = MessageBox.Show("Вы успешно авторизированы! Ваша роль: Администратор", "Авторизация", MessageBoxButton.OK);
                        VisibilityWindowsService.OpenUserWindowForAdmin();
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Вы успешно авторизированы! Ваша роль: Клиент", "Авторизация", MessageBoxButton.OK);
                        VisibilityWindowsService.OpenUserWindowForUser();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Данные введены неверно!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearPasswordAndLogin();
                }
            }
            if (loginAttempts >= maxLoginAttempts)
            {
                enterBttn.IsEnabled = false;
                timer.Start();
                MessageBox.Show("Достигнуто максимальное количество попыток. Попробуйте позже.", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            enterBttn.IsEnabled = true;
            loginAttempts = 0;
        }
        private void ClearPasswordAndLogin()
        {
            pswrdBxPassword.Password = null;
            logInTxtBx.Text = null;
        }

        

        private void pswrdBxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (/*string.IsNullOrWhiteSpace(logInTxtBx.Text) || */string.IsNullOrEmpty(pswrdBxPassword.Password))
            {
                chckPass.IsEnabled = false;
                chckPass.IsChecked = false;
                enterBttn.IsEnabled = false;
            }
            else
            {
                chckPass.IsEnabled = true;
                enterBttn.IsEnabled = true;
            }
        }
        private void TbPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                chckPass.IsEnabled = false;
                chckPass.IsChecked = false;
            }
            else
            {
                chckPass.IsEnabled = true;
            }
            pswrdBxPassword.Password = tbPassword.Text;
        }
        private void FogotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция на данный момент не доступна. Мы уже работаем над этим.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция на данный момент не доступна. Мы уже работаем над этим.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
