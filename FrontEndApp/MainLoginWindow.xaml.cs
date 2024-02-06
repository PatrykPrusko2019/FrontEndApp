using FrontEndApp.Models;
using FrontEndApp.Services;
using FrontEndApp.View;
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

namespace FrontEndApp
{
    /// <summary>
    /// Interaction logic for MainLoginWindow.xaml
    /// </summary>
    public partial class MainLoginWindow : Window
    {
        public MainLoginWindow()
        {
            InitializeComponent();
        }

        private async void Button_SignIn(object sender, RoutedEventArgs e)
        {
            LoginDto loginDto = new LoginDto()
            {
                Email = LoginEmail.Text,
                Password = LoginPassword.Password
            };
            LoginEmail.Text = "";
            LoginPassword.Password = "";

            ILoginService loginService = new LoginService();
            var responseBody = loginService.LoginUser(loginDto);
            string tokenJWT = responseBody.Result;

            if (tokenJWT == null || tokenJWT.Count() == 0) return;

            //get detailed information about given user
            IUserService userService = new UserService();
            var loginUser = userService.GetUserByEmail(loginDto.Email);

            UserDto user = await loginUser;
            user.TokenJWT = tokenJWT;

            //if everything is ok, go to window music store for given user
            ProductStoreWindow.DetailsUser = user;
            ProductStoreWindow productStoreWindow = new ProductStoreWindow();
            this.Visibility = Visibility.Hidden;
            productStoreWindow.Show();
        }

        private void Button_Register(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            this.Visibility = Visibility.Hidden;
            register.Show();
        }
    }
}
