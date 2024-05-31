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
using RestShev.Components;
namespace RestShev.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
            LbCart.ItemsSource = App.DB.Order_Meal.Where(x => x.StatusId == 2).OrderByDescending(x=> x.ID).ToList();
        }      
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LbCart.ItemsSource = App.DB.Order_Meal.Where(x => x.StatusId == 2).OrderByDescending(x => x.ID).ToList();
            CommandManager.InvalidateRequerySuggested();
        }
   
        private void BtCompl_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as MenuItem).DataContext as Order_Meal;
            if (select == null)
            {
                return;
            }
            select.StatusId = 4;
            App.DB.SaveChanges();
            bool allcomplit = true;
            Order order = App.DB.Order.FirstOrDefault(x=>x.ID == select.OrderID);
            IEnumerable<Order_Meal> products = App.DB.Order_Meal.Where(x => x.OrderID == order.ID).ToList();
            foreach (var items in products)
            {            
                if (items.StatusId != 4)
                {
                    allcomplit = false;
                }
            }         
            if (allcomplit == true)
            {
                order.StatusID = 3;
            }
            App.DB.SaveChanges();
            LbCart.ItemsSource = App.DB.Order_Meal.Where(x => x.StatusId == 2).OrderByDescending(x => x.ID).ToList();
        }

    }
}
