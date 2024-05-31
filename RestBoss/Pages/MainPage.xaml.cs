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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            CbTimes.SelectedIndex = 0;

            Update();
        }

        private void BtChief_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChiefRequestPage());
        }

        private void BtEmployees_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesPage());
        }

        private void BtDismissed_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeDismissedPage());
        }

        public void Update()
        {
            int d = 0;
            if(CbTimes.SelectedIndex == 0)
            {
                d = 7;
            }
            if (CbTimes.SelectedIndex == 1)
            {
                d = 30;
            }
            if (CbTimes.SelectedIndex == 2)
            {
                d = 90;
            }
            var dates = DateTime.Now - TimeSpan.FromDays(d);
            ScottPlot.Plot myPlot1 = new ScottPlot.Plot();
            var result3 = (
            from s in App.DB.Order.Where(x => x.DataTimeEnd >= dates && x.DataTimeEnd < DateTime.Now).ToList()
            group s by new { date = new DateTime(s.DataTimeEnd.Value.Date.Year, s.DataTimeEnd.Value.Date.Month, s.DataTimeEnd.Value.Date.Day).Date } into g
            select new
            {
                DateKey = g.Key.date,
                Counts = g.Count()
            }
            ).ToList();
            double?[] counts3 = new double?[result3.ToList().Count];
            DateTime?[] date3 = new DateTime?[result3.ToList().Count];
            for (int i = 0; i < result3.Count; i++)
            {
                counts3[i] = result3[i].Counts;
                date3[i] = result3[i].DateKey;
            }
            for (int i = 0; i < result3.Count; i++)
            {
                myPlot1.Add.Text($"{result3[i].DateKey.ToString("D")} \n {result3[i].Counts} шт.", result3[i].DateKey.Date.ToOADate(), (double)result3[i].Counts);
            }
            myPlot1.Add.Scatter(date3, counts3);
            myPlot1.Axes.DateTimeTicksBottom();
            myPlot1.SavePng("demo.png", 400, 300);
            wpfPlot1.Reset(myPlot1);
            wpfPlot1.Refresh();

            ScottPlot.Plot myPlot2 = new ScottPlot.Plot();
  
            var result2 = (
            from s in App.DB.Order.Where(x=>x.DataTimeEnd >= dates && x.DataTimeEnd < DateTime.Now).ToList()
            group s by new { date = new DateTime(s.DataTimeEnd.Value.Date.Year, s.DataTimeEnd.Value.Date.Month, s.DataTimeEnd.Value.Date.Day).Date } into g
            select new
            {
                DateKey = g.Key.date,
                Sum = g.Sum(x => x.Price)
            }
            ).ToList();
            double?[] sum2 = new double?[result2.ToList().Count];
            DateTime?[] date2 = new DateTime?[result2.ToList().Count];
            for (int i = 0; i < result2.Count; i++)
            {
                sum2[i] = result2[i].Sum;
                date2[i] = result2[i].DateKey;
            }
            for (int i = 0; i < result2.Count; i++)
            {
                myPlot2.Add.Text($"{result2[i].DateKey.ToString("D")} \n {result2[i].Sum} руб.", result2[i].DateKey.Date.ToOADate(), (double)result2[i].Sum);
            } 
            myPlot2.Axes.DateTimeTicksBottom();
            myPlot2.Add.Scatter(date2, sum2);
            myPlot2.SavePng("demo.png", 400, 300);
           
            wpfPlot2.Reset(myPlot2);
            wpfPlot2.Refresh();

            ScottPlot.Plot myPlot3 = new ScottPlot.Plot();
            int?[] sum1 = App.DB.Order.Where(o => o.DataTimeEnd < DateTime.Now).OrderBy(x => x.DataTimeEnd).Select(x => x.Price).ToArray();
            DateTime?[] dataX2 = App.DB.Order.Where(o => o.DataTimeEnd < DateTime.Now).OrderBy(x => x.DataTimeEnd).Select(x => x.DataTimeEnd).ToArray();
            var result = (
           from s in App.DB.Order.Where(x => x.DataTimeEnd != null).ToList()
           group s by new { date = new DateTime(s.DataTimeEnd.Value.Date.Year, s.DataTimeEnd.Value.Date.Month, s.DataTimeEnd.Value.Date.Day).Date } into g
           select new
           {
               DateKey = g.Key.date,
               Sum = g.Sum(x => x.Price),
               Counts = g.Count()
           }
           ).ToList();
            double?[] sum = new double?[result.ToList().Count];
            double?[] counts = new double?[result.ToList().Count];
            DateTime?[] date = new DateTime?[result.ToList().Count];
            for (int i = 0; i < result.Count; i++)
            {
                sum[i] = result[i].Sum;
                date[i] = result[i].DateKey;
                counts[i] = result[i].Counts;
            }        
            for (int i = 0; i < result.Count; i++)
            {
                myPlot3.Add.Text($"{result[i].DateKey.ToString("D")} \n {result[i].Sum} руб.", result[i].DateKey.Date.ToOADate(), (double)result[i].Sum);
            }
            myPlot3.Add.Scatter(date, sum);
            for (int i = 0; i < result.Count; i++)
            {
                myPlot3.Add.Text($"{result[i].DateKey.ToString("D")} \n {result[i].Counts} шт.", result[i].DateKey.Date.ToOADate(), (double)result[i].Counts);
            }
            myPlot3.Add.Scatter(date, counts);
            myPlot3.Axes.DateTimeTicksBottom();
            myPlot3.SavePng("demo.png", 400, 300);
            wpfPlot3.Reset(myPlot3);
            wpfPlot3.Refresh();
        }
        private void CbTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
