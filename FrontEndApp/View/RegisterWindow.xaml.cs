using FrontEndApp.Models;
using FrontEndApp.Services;
using FrontEndApp.Utilites;
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
using System.Windows.Shapes;

namespace FrontEndApp.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public static RegisterWindow c;
        public RegisterWindow()
        {
            InitializeComponent();
            c = this;
        }

        private async void Button_CreateNewAccount(object sender, RoutedEventArgs e)
        {
            string[] insertedDate = RegisterDateOfBirth.Text.Split('-').Reverse().ToArray();
            string date = string.Join("-", insertedDate);

            if (DateTime.TryParse(date, out DateTime dateOfBirth))
            {
                RegisterUserDto registerUserDto = new RegisterUserDto()
                {
                    FirstName = RegisterFirstName.Text,
                    LastName = RegisterLastName.Text,
                    Email = RegisterEmail.Text,
                    Password = RegisterPassword.Password,
                    ConfirmPassword = RegisterConfirmPassword.Password,
                    Nationality = RegisterNationality.Text,
                    DateOfBirth = dateOfBirth
                };
                IRegisterService registerService = new RegisterService();
                bool result = await registerService.Register(registerUserDto);
                if (result)
                {
                    MainLoginWindow login = new MainLoginWindow();
                    this.Visibility = Visibility.Hidden;
                    login.Show();
                }

            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Invalid Date Of Birth -> correct format is: day-month-year");
                return;
            }
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            Reset.ClearValuesOfUserRegisterWindow();
        }

        private void Button_ReturnToLogin(object sender, RoutedEventArgs e)
        {
            MainLoginWindow mainLoginWindow = new MainLoginWindow();
            this.Visibility = Visibility.Hidden;
            mainLoginWindow.Show();
        }
    }
}
