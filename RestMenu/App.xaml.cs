using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RestMenu.Components;
namespace RestMenu
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RestarauntDeliveryEntities1 DB = new RestarauntDeliveryEntities1();
    }
}
