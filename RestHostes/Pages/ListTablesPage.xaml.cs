using RestHostes.Components;
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

namespace RestHostes.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListTablesPage.xaml
    /// </summary>
    public partial class ListTablesPage : Page
    {
        int countSeats = 0;
        public ListTablesPage()
        {
            InitializeComponent();
            Update();
        }

        private void Update()
        {
            IEnumerable<Order> TableIdList = App.DB.Order.Where(x => x.DataTimeEnd == null && x.DateTimesSt < DateTime.Now).ToList();
            IEnumerable<Tables> TableIdListFree = App.DB.Tables.ToList();
            List<Tables> Free = App.DB.Tables.ToList();
            foreach (var items in TableIdList)
            {
                foreach (var items2 in TableIdListFree)
                {
                    if (items.TableId == items2.Id)
                    {
                        Free.Remove(items2);
                    }
                }
            }
            Free.OrderBy(x => x.Id);
            if(countSeats == 0)
            {
                LbTable.ItemsSource = Free.ToList();
            }
            else
            {
                LbTable.ItemsSource = Free.Where(x=>x.NumberOfSeats == countSeats).ToList();
            }
            

        }
        private void BtTable4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            countSeats = 4;
            Update();
        }

        private void BtTable6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            countSeats = 6;
            Update();
        }

        private void BtAll_MouseDown(object sender, MouseButtonEventArgs e)
        {
            countSeats = 0;
            Update();
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as MenuItem).DataContext as Tables;
            NavigationService.Navigate(new CrateOrder_TablePage(select));
        }
    }
}
