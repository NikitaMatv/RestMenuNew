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
    /// Логика взаимодействия для CrateOrder_TablePage.xaml
    /// </summary>
    public partial class CrateOrder_TablePage : Page
    {
        public Tables contextTable = new Tables();
        public Customer contextCustomer;
        public CrateOrder_TablePage(Tables table)
        {
            InitializeComponent();
            contextTable = table;
            DataContext = contextTable;
            Update();
        }
        private void Update()
        {
            
            if(TbSearch.Text.Length > 0)
            {
                LbCustomer.ItemsSource = App.DB.Customer.Where(x=>x.FirstName.StartsWith(TbSearch.Text) || x.SurName.StartsWith(TbSearch.Text) || x.PhoneNumber.StartsWith(TbSearch.Text)).ToList();
            }
            else
            {
                LbCustomer.ItemsSource = App.DB.Customer.ToList();
            }

        }
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
           Customer newcustomer = new Customer();
            if(TbNames.Text.Trim().Length > 0 && TbSNames.Text.Trim().Length > 0 && TbPhone.Text.Trim().Length > 0)
            {
                newcustomer.FirstName = TbNames.Text;
                newcustomer.SurName = TbSNames.Text;
                newcustomer.PhoneNumber = TbPhone.Text;
            }
            else
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if (App.DB.Customer.FirstOrDefault(x=>x.PhoneNumber == newcustomer.PhoneNumber) == null)
            {
                App.DB.Customer.Add(newcustomer);
                App.DB.SaveChanges();
            }
            else
            {
                MessageBox.Show("Аккаунт с таким телефоном уже существует");
                return;
            }
            SpCustomer.DataContext = null;
            Update();
        }

        private void BtCleat_Click(object sender, RoutedEventArgs e)
        {
            SpCustomer.DataContext = null;
            Update();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void BtCreate_Click(object sender, RoutedEventArgs e)
        {
            Order ordern = new Order();
            ordern.CustomerId = contextCustomer.ID;
            ordern.DateTimesSt = DateTime.Now;
            ordern.StatusID = 1;
            ordern.OptionsID = 1;
            ordern.TableId = contextTable.Id;
            App.DB.Order.Add(ordern);
            App.DB.SaveChanges();
            NavigationService.Navigate(new ListTablesPage());
        }

        private void BtCansle_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListTablesPage());
        }

        private void BtAddInOrder_Click(object sender, RoutedEventArgs e)
        {
            var sleect = (sender as MenuItem).DataContext as Customer;
            if(sleect != null)
            {
                contextCustomer = sleect;
                TbName.Text = $"Имя гостя {sleect.FirstName}.";
            }
            else
            {
                return;
            }
            
        }
    }
}
