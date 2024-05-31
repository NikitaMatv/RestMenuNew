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

namespace RestBoss.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void AutorBt_Click(object sender, RoutedEventArgs e)
        {
            var Boss = App.DB.Employee.FirstOrDefault(Cus => Cus.Login == LoginTb.Text);
            if (Boss == null)
            {
                LoginTb.Text = string.Empty;
                PasswordTb.Password = string.Empty;
                MessageBox.Show("Логин или пароль неверный");
                return;
            }
            if (Boss.RoleID != 8)
            {
                MessageBox.Show("У вас нету доступа к этому приложению. \n Приложение предназначено исключительно для владельца ретсорана");
                return;
            }
            if (Boss.Password != PasswordTb.Password)
            {
                MessageBox.Show("Логин или пароль неверный");
                return;
            }
            App.LoggedBoss = Boss;
            NavigationService.Navigate(new MainPage());
        }
    }
}
