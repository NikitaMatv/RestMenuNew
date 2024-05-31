using RestBoss.Components;
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
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            LBEmployee.ItemsSource = App.DB.Employee.Where(x => x.IsDismissed != true).Where(x=>x.RoleID != 8).ToList();
        }

        private void Redact_Click(object sender, RoutedEventArgs e)
        {
            var SelectedEmployee = (sender as MenuItem).DataContext as Employee;
            if (SelectedEmployee == null)
            {
                return;
            }
            NavigationService.Navigate(new EmployeeEditPage(SelectedEmployee));
        }

        private void EmployeeFire_Click(object sender, RoutedEventArgs e)
        {
            var SelectedEmployee = (sender as MenuItem).DataContext as Employee;
            if (SelectedEmployee == null)
            {
                return;
            }
            SelectedEmployee.IsDismissed = true;
            App.DB.SaveChanges();
            LBEmployee.ItemsSource = App.DB.Employee.Where(x => x.IsDismissed != true).Where(x => x.RoleID != 8).ToList();
        }

        private void EmployeeAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeEditPage(new Employee()));
        }

        private void BtChief_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChiefRequestPage());
        }

        private void BtEmployees_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void BtDismissed_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeDismissedPage());
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbSearch.Text.Length > 0)
            {
               
                LBEmployee.ItemsSource = App.DB.Employee.Where(x => x.Name.ToLower().Contains(TbSearch.Text.Trim().ToLower()) || x.Surname.ToLower().Contains(TbSearch.Text.Trim().ToLower()) || x.PhoneNumber.ToLower().Contains(TbSearch.Text.Trim().ToLower())).Where(x => x.IsDismissed != true).Where(x => x.RoleID != 8).ToList();
            }
            else
            {
                LBEmployee.ItemsSource = App.DB.Employee.Where(x => x.IsDismissed != true).Where(x => x.RoleID != 8).ToList();
            }
        }
    }
}
