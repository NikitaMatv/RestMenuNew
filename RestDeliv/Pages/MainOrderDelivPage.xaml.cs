using RestDeliv.Componens;
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

namespace RestDeliv.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainOrderDelivPage.xaml
    /// </summary>
    public partial class MainOrderDelivPage : Page
    {
        public MainOrderDelivPage()
        {
            InitializeComponent();
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
            LbCart.ItemsSource = App.DB.Order.Where(x => x.StatusID == 3 && x.OptionsID == 2).ToList();
            LbDeliv.ItemsSource = App.DB.Order.Where(x => x.StatusID == 4 && x.OptionsID == 2).ToList();
            LbCart.SelectedIndex = 0;
            SpInf.DataContext = LbCart.SelectedItem as Order;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LbCart.ItemsSource = App.DB.Order.Where(x => x.StatusID == 3 && x.OptionsID == 2).ToList();
            LbDeliv.ItemsSource = App.DB.Order.Where(x => x.StatusID == 4 && x.OptionsID == 2).ToList();
            CommandManager.InvalidateRequerySuggested();
        }

        private void BtDetails_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as MenuItem).DataContext as Order;
            if (select == null)
            {
                return;
            }
            string InOrder = "В заказе: \n";
            IEnumerable<Order_Meal> products = App.DB.Order_Meal.Where(x => x.OrderID == select.ID).ToList();
            foreach (var items in products)
            {
                InOrder += $"{items.Meal.Name} - {items.Count} шт. \n";
            }
            MessageBox.Show($"{InOrder}");
            return;
        }

        private void BtCompl_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as MenuItem).DataContext as Order;
            if (select == null)
            {
                return;
            }
            select.StatusID = 4;
            App.DB.SaveChanges();
            LbCart.ItemsSource = App.DB.Order.Where(x => x.StatusID == 3 && x.OptionsID == 2).ToList();
            LbDeliv.ItemsSource = App.DB.Order.Where(x => x.StatusID == 4 && x.OptionsID == 2).ToList();
        }

        private void BtComplits_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as MenuItem).DataContext as Order;
            if (select == null)
            {
                return;
            }
            select.StatusID = 5;
            select.DataTimeEnd = DateTime.Now;
            App.DB.SaveChanges();
            LbCart.ItemsSource = App.DB.Order.Where(x => x.StatusID == 3 && x.OptionsID == 2).ToList();
            LbDeliv.ItemsSource = App.DB.Order.Where(x => x.StatusID == 4 && x.OptionsID == 2).ToList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var select = (sender as Border).DataContext as Order;
            SpInf.DataContext = select;
        }
    }
}
