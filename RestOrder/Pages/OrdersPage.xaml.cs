using RestOrder.Components;
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

namespace RestOrder.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
            LbCart.ItemsSource = App.DB.Order_Meal.Where(x => x.StatusId == 2).ToList();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LbCart.ItemsSource = App.DB.Order_Meal.Where(x => x.StatusId == 2).ToList();
            CommandManager.InvalidateRequerySuggested();
        }

        private void BtDetails_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as MenuItem).DataContext as Order_Meal;
            if (select == null)
            {
                return;
            }
            MessageBox.Show($"{select.Meal.Recipe}");
            return;
        }     
    }
}
