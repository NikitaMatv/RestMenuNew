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
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace RestBoss.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
      
        class ExlMeal
        {
            
             public Meal meal { get; set; }
             public int Sum { get; set; }
            
        }
        IEnumerable<ExlMeal> import;
        string DateSelect = " ";
        string foglio = @"C:\Users\...\DownLoads\Example.xls";
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
            from s in App.DB.Order.Where(x => x.DataTimeEnd >= dates && x.DataTimeEnd < DateTime.Now).OrderBy(x=>x.DataTimeEnd).ToList()
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
            from s in App.DB.Order.Where(x=>x.DataTimeEnd >= dates && x.DataTimeEnd < DateTime.Now).OrderBy(x => x.DataTimeEnd).ToList()
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
            IEnumerable<Order_Meal> result = App.DB.Order_Meal.Where(x => x.Order.DataTimeEnd >= dates.Date && x.Order.DataTimeEnd < DateTime.Now).ToList() ;
            var results = result.GroupBy(x => x.Meal, x => x.Count)
                  .Select(g => new ExlMeal { meal = g.Key, Sum = (int)g.Sum() });
            import = results.OrderByDescending(x=>x.Sum);
            DateSelect = $"{DateTime.Now.Date - dates.Date} - {DateTime.Now.Date.ToShortDateString()}";
            LbMeal.ItemsSource = results.ToList();
        }
        private void CbTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void BtImport_Click(object sender, RoutedEventArgs e)
        {

            Excel.Application exApp = new Excel.Application();
            Excel.Workbook mWorkbook = exApp.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet mWSheet1 = (Excel.Worksheet)mWorkbook.Worksheets.get_Item(1);

            mWSheet1.Cells[1, 1] = "Название";
            mWSheet1.Cells[1, 1].Font.Bold = true;
            mWSheet1.Cells[1, 2] = "Цена за шт";
            mWSheet1.Cells[1, 2].Font.Bold = true;
            mWSheet1.Cells[1, 3] = "Количество";
            mWSheet1.Cells[1, 3].Font.Bold = true;
            mWSheet1.Cells[1, 4] = "Цена за все";
            mWSheet1.Cells[1, 4].Font.Bold = true;
            (exApp.Sheets[1] as Excel.Worksheet).Name = "Отчет";
            int i = 2;
            foreach(var item in import)
            {
                mWSheet1.Cells[i, 1] = item.meal.Name;
                mWSheet1.Cells[i, 2] = item.meal.Price;
                mWSheet1.Cells[i, 3] = item.Sum;
                mWSheet1.Cells[i, 4] = item.Sum * item.meal.Price;
                i++; 
            }
            SaveFileDialog save = new SaveFileDialog();
            
            save.Filter = "Excel (*.xls)|*.xls";
            save.ShowDialog();
            if (save.FileName == string.Empty)
            {
                return;
            }
            foglio = save.FileName;
            mWorkbook.SaveAs(foglio, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);          
            MessageBoxResult result = MessageBox.Show("Открыть фаил?","Открыть",MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result.Equals(MessageBoxResult.Yes))
            {
                OpenFile();
            }
            mWorkbook.Close(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
            exApp.Quit();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();          
        }
        private void OpenFile()
        {
            try
            {
                var excelApp = new Excel.Application();
                excelApp.Visible = true;
                Excel.Workbooks books = excelApp.Workbooks;
                Excel.Workbook sheets = books.Open(foglio);              
            }
            catch
            {
                return;
            }
        }
    }
}
