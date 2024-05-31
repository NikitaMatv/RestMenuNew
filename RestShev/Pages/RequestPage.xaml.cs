using RestShev.Components;
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

namespace RestShev.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        public RequestPage()
        {
            InitializeComponent();
            LBMeal.ItemsSource = App.DB.Meal.OrderByDescending(x=>x.ID).ToList();
        }
        public IEnumerable<Meal> meal = App.DB.Meal.ToList();
        private void Update()
        {
            if (TbSearch.Text.Length > 0)
            {
                meal = meal.Where(x => x.Name.ToLower().Contains(TbSearch.Text.Trim().ToLower())).OrderByDescending(x => x.ID);
                LBMeal.ItemsSource = meal.ToList();
            }
            else
            {
                LBMeal.ItemsSource = meal.ToList();
            }
        }
        private void BtFirst_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 1).OrderByDescending(x => x.ID).ToList();
            Update();
        }
        private void BtSecond_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 2).OrderByDescending(x => x.ID).ToList();
            Update();
        }

        private void BtSalad_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 3).OrderByDescending(x => x.ID).ToList();
            Update();
        }

        private void BtDessert_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 4).OrderByDescending(x => x.ID).ToList();
            Update();
        }

        private void BtDrinks_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 5).OrderByDescending(x => x.ID).ToList();
            Update();
        }

        private void BtAll_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.OrderByDescending(x => x.ID).ToList();
            TbSearch.Text = string.Empty;
            Update();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }  
    }
}
