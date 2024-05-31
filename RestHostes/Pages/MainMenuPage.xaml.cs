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
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void BtTable_Click(object sender, RoutedEventArgs e)
        {
            MemuFame.NavigationService.Navigate(new ListTablesPage());
        }

        private void BtDeliv_Click(object sender, RoutedEventArgs e)
        {
            MemuFame.NavigationService.Navigate(new DelivePage());
        }

        private void BtOrder_Click(object sender, RoutedEventArgs e)
        {
            MemuFame.NavigationService.Navigate(new OrderPage());
        }
    }
}
