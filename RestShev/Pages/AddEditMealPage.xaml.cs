using Microsoft.Win32;
using RestShev.Components;
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

namespace RestShev.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditMealPage.xaml
    /// </summary>
    public partial class AddEditMealPage : Page
    {
        Meal contextmeal;
        public AddEditMealPage(Meal meal)
        {
            InitializeComponent();
            contextmeal = meal;
            DataContext = contextmeal;
            CbCotegories.ItemsSource = App.DB.Cotegories.ToList();
            CbCotegories.SelectedIndex = 0;
        }

        private void BtImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextmeal.Images = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextmeal;
            }
        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[A-zА-я]") == false)
            {
                e.Handled = true;
            }
        }

        private void TbPhone_number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void ExitBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            if (contextmeal.Description == string.Empty || contextmeal.Description == null)
            {
                MessageBox.Show("Заполните описание.");
                return;
            }
            if (contextmeal.Name == string.Empty || contextmeal.Name== null)
            {
                MessageBox.Show("Заполните название.");
                return;
            }
            if (contextmeal.Images == null )
            {
                MessageBox.Show("Заполните фотогравию.");
                return;
            }
            if (contextmeal.Price == null || contextmeal.Price == null)
            {
                MessageBox.Show("Заполните цена.");
                return;
            }
            if (contextmeal.ID == 0)
            {
                contextmeal.RequestStatusID = 1;
                App.DB.Meal.Add(contextmeal);

            }
            else
            {
                contextmeal.RequestStatusID = 2;
            }
            App.DB.SaveChanges();
            NavigationService.Navigate(new MenuPage());
        }
    }
}
