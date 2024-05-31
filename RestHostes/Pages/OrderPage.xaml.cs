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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public Order contsOrd = new Order();
        public int selectind = 0;
        public bool allcomplit = false;
        public OrderPage()
        {
            InitializeComponent();
            Update();
            Update2();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedclient = (sender as Button).DataContext as Order_Meal;
            if (selectedclient.StatusId == 1)
            {
                if (selectedclient.Count > 1)
                {
                    selectedclient.Count = selectedclient.Count - 1;
                }
                else
                {
                    App.DB.Order_Meal.Remove(selectedclient);
                }
                App.DB.SaveChanges();
                Update();
            }
            else
            {
                MessageBox.Show("Данный пункт заказа уже подтрежден его изменить нельзя");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedclient = (sender as Button).DataContext as Order_Meal;
            if (selectedclient.StatusId == 1)
            {
                 MessageBoxResult result = MessageBox.Show($"Уверены что хотите удалить {selectedclient.Meal.Name}?", "Подтверждение.",  MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    App.DB.Order_Meal.Remove(selectedclient);
                    App.DB.SaveChanges();
                }
               
                Update();
            }
            else
            {
                MessageBox.Show("Данный пункт заказа уже подтрежден его изменить нельзя");
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selectedclient = (sender as Button).DataContext as Order_Meal;
            if (selectedclient.StatusId == 1)
            {
                if (selectedclient.Count < 20)
                {
                    selectedclient.Count = selectedclient.Count + 1;
                }
                else
                {
                    MessageBox.Show("Можно заказать тока 20 шт");
                    return;
                }
                App.DB.SaveChanges();
                Update();
            }
            else
            {
                MessageBox.Show("Данный пункт заказа уже подтрежден его изменить нельзя");
            }

        }

        private void Update()
        {
            bool allcomplit = true;
            LbCart.ItemsSource = App.DB.Order_Meal.Where(x => x.OrderID == contsOrd.ID).ToList();
            int pri = 0;
            IEnumerable<Order_Meal> products = App.DB.Order_Meal.Where(x => x.OrderID == contsOrd.ID).ToList();
            foreach (var items in products)
            {
                pri += (int)items.Meal.Price * (int)items.Count;
                if (items.StatusId != 2)
                {
                    allcomplit = false;
                }
            }
            if (contsOrd.ID == 0 || contsOrd.Order_Meal.Count() == 0)
            {
                allcomplit = false;
            }
            contsOrd.Price = (int)pri;
            if (contsOrd.DiscountId != null)
            {
                contsOrd.DiscountPrice = (int)(pri * (1 - ((double)contsOrd.DiscountCode.Discount / 100)));
            }
            if (allcomplit == true)
            {
                contsOrd.StatusID = 3;
            }

            App.DB.SaveChanges();
            TbTotalPrice.Text = $"Цена: {contsOrd.Price} руб.";
            if (contsOrd.DiscountId != null)
            {
                TbEndePrice.Text = $"Цена со скидкой: {contsOrd.DiscountPrice} руб.";
            }
            if (contsOrd.StatusID == 3)
            {
                SpButtonStartOrder.Visibility = Visibility.Collapsed;
                SpButtonCmplOrder.Visibility = Visibility.Visible;
            }
            else
            {
                SpButtonStartOrder.Visibility = Visibility.Visible;
                SpButtonCmplOrder.Visibility = Visibility.Collapsed;
            }
            if (contsOrd.DiscountId != null)
            {
                SpDiscountCode.Visibility = Visibility.Hidden;
                SpDiscount.Visibility = Visibility.Visible;
            }
            else
            {
                SpDiscountCode.Visibility = Visibility.Visible;
                SpDiscount.Visibility = Visibility.Hidden;
            }

        }

        private void BtClear_Click(object sender, RoutedEventArgs e)
        {
            if (contsOrd.ID == 0)
            {
                MessageBox.Show("Заказ ещё не создан");
                return;
            }
            IEnumerable<Order_Meal> products = App.DB.Order_Meal.Where(x => x.OrderID == contsOrd.ID).ToList();
            foreach (var items in products)
            {
                if (items.StatusId == 1)
                {
                    App.DB.Order_Meal.Remove(items);
                }
            }
            App.DB.SaveChanges();
            Update();

        }

        private void BtCompl_Click(object sender, RoutedEventArgs e)
        {
            if (contsOrd.ID == 0)
            {
                MessageBox.Show("Заказ ещё не создан");
                return;
            }
            IEnumerable<Order_Meal> products = App.DB.Order_Meal.Where(x => x.OrderID == contsOrd.ID).ToList();
            foreach (var items in products)
            {
                items.StatusId = 2;

            }
            contsOrd.StatusID = 2;
            App.DB.SaveChanges();
            Update();
        }
        public IEnumerable<Meal> meal = App.DB.Meal.Where(x => x.RequestStatusID == 3).ToList();

        private void Update2()
        {
            if (TbSearch.Text.Length > 0)
            {
                meal = meal.Where(x => x.Name.ToLower().Contains(TbSearch.Text.Trim().ToLower()));
                LBMeal.ItemsSource = meal.ToList();
            }
            else
            {
                LBMeal.ItemsSource = meal.ToList();
            }
        }
        private void BtFirst_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 1).Where(x => x.RequestStatusID == 3).ToList();
            Update2();
        }
        private void BtSecond_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 2).Where(x => x.RequestStatusID == 3).ToList();
            Update2();
        }

        private void BtSalad_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 3).Where(x => x.RequestStatusID == 3).ToList();
            Update2();
        }

        private void BtDessert_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 4).Where(x => x.RequestStatusID == 3).ToList();
            Update2();
        }

        private void BtDrinks_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.CotegoriesID == 5).Where(x => x.RequestStatusID == 3).ToList();
            Update2();
        }

        private void BtAll_MouseDown(object sender, MouseButtonEventArgs e)
        {
            meal = App.DB.Meal.Where(x => x.RequestStatusID == 3).ToList();
            TbSearch.Text = string.Empty;
            Update2();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update2();
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            var selectedclient = (sender as MenuItem).DataContext as Meal;
            if (selectedclient == null)
            {
                MessageBox.Show("Выберете товар");
                return;
            }
            if (contsOrd.ID == 0)
            {
                MessageBox.Show("Заказ ещё не создан");
                return;
            }
            else
            {
                if (App.DB.Order_Meal.FirstOrDefault(x => x.Order.ID == contsOrd.ID && x.MealID == selectedclient.ID && x.StatusId == 1) == null)
                {
                    Order_Meal zakaz = new Order_Meal();
                    zakaz.MealID = selectedclient.ID;
                    zakaz.Count = 1;
                    zakaz.OrderID = contsOrd.ID;
                    zakaz.StatusId = 1;
                    App.DB.Order_Meal.Add(zakaz);
                }
                else
                {
                    var zakazz = App.DB.Order_Meal.FirstOrDefault(x => x.OrderID == contsOrd.ID && x.MealID == selectedclient.ID && x.StatusId == 1) as Order_Meal;
                    if (zakazz.Count < 20)
                    {
                        zakazz.Count = zakazz.Count + 1;
                    }
                    else
                    {
                        MessageBox.Show("Одну позицию из меню можно заказать не больше 20 раз в одно заказе!");
                        return;
                    }
                }
                contsOrd.StatusID = 1;
                App.DB.SaveChanges();
                Update();
            }

        }

        private void BtDiscount_Click(object sender, RoutedEventArgs e)
        {
            var code = App.DB.DiscountCode.FirstOrDefault(x => x.Code == TbCode.Text.Trim() && x.DataStart < DateTime.Now && x.DataEnd > DateTime.Now) as DiscountCode;
            if (contsOrd.ID == 0)
            {
                MessageBox.Show("Заказ ещё не создан");
                return;
            }
            if (code != null && contsOrd.DiscountId == null)
            {
                contsOrd.DiscountId = code.Id;
                SpDiscount.Visibility = Visibility.Visible;
                double disc = (double)(1 - ((double)code.Discount / 100));
                contsOrd.DiscountPrice = (int)(contsOrd.Price * disc);
                App.DB.SaveChanges();
                TbEndePrice.Text = $"Цена со скидкой: {contsOrd.DiscountPrice} руб.";
            }
            else
            {
                MessageBox.Show("Код неверный или истек срок действия");
                return;
            }
        }

        private void BtCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (contsOrd.ID == 0)
            {
                MessageBox.Show("Заказ ещё не создан");
                return;
            }
            if (contsOrd.Address == null || contsOrd.Address == string.Empty)
            {
                MessageBox.Show("Адрес ещё не заполнен");
                return;
            }
            contsOrd.DataTimeEnd = DateTime.Now;
            contsOrd.StatusID = 6;
            MessageBox.Show($"Заказ {contsOrd.ID} сформирован.");
            App.DB.SaveChanges();
            Update();
            Update2();
            BtCreate.Visibility = Visibility.Visible;
            SpAddress.Visibility = Visibility.Collapsed;
            Order order = new Order();
            contsOrd = order;
            Update();
        }

        private void BtCreate_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            order.DateTimesSt = DateTime.Now;
            order.OptionsID = 2;
            order.StatusID = 1;
            App.DB.Order.Add(order);
            App.DB.SaveChanges();
            contsOrd = order;
            BtCreate.Visibility = Visibility.Collapsed;
            SpAddress.Visibility = Visibility.Visible;
        }

        private void BtSaveAddress_Click(object sender, RoutedEventArgs e)
        {
            if (TbAddres.Text.Trim().Length > 0)
            {
                contsOrd.Address = TbAddres.Text.Trim();
            }
            else
            {
                MessageBox.Show($"Заполните адрес.");
                return;
            }

        }
    }
}
