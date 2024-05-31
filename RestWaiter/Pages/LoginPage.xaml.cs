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

namespace RestWaiter.Pages
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
            var employee = App.DB.Employee.FirstOrDefault(Cus => Cus.Login == LoginTb.Text);
            if (employee == null)
            {
                LoginTb.Text = string.Empty;
                PasswordTb.Password = string.Empty;
                MessageBox.Show("Логин или пароль неверный");
                return;
            }
            if (employee.RoleID != 6)
            {
                MessageBox.Show("У вас нету доступа к этому приложению. \n Приложение только для Шев повара!");
                return;
            }
            if (employee.Password != PasswordTb.Password)
            {
                MessageBox.Show("Логин или пароль неверный");
                return;
            }
            App.LoggedEmployee = employee;
            NavigationService.Navigate(new CartPage());
        }
    }
}
