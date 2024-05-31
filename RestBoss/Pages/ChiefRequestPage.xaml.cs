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
    /// Логика взаимодействия для ChiefRequestPage.xaml
    /// </summary>
    public partial class ChiefRequestPage : Page
    {
        public ChiefRequestPage()
        {
            InitializeComponent();
            LBMealAdd.ItemsSource = App.DB.Meal.Where(m => m.RequestStatusID == 1 || m.RequestStatusID == 2).ToList();
        }


        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            var SelectedContextItem = (sender as MenuItem).DataContext as Meal;
            SelectedContextItem.RequestStatusID = 3;
            App.DB.SaveChanges();
            LBMealAdd.ItemsSource = App.DB.Meal.Where(m => m.RequestStatusID == 1 || m.RequestStatusID == 2).ToList();
        }


        private void MenuRefuse_Click(object sender, RoutedEventArgs e)
        {
            var SelectedContextItem = (sender as MenuItem).DataContext as Meal;
            SelectedContextItem.RequestStatusID = 4;
            App.DB.SaveChanges();
            LBMealAdd.ItemsSource = App.DB.Meal.Where(m => m.RequestStatusID == 1 || m.RequestStatusID == 2).ToList();
        }

        private void BtChief_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void BtEmployees_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeDismissedPage());
        }

        private void BtDismissed_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesPage());
        }
    }
}
