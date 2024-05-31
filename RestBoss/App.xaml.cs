using RestBoss.Components;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RestBoss
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RestarauntDeliveryEntities1 DB = new RestarauntDeliveryEntities1();
        public static Employee LoggedBoss;
    }
}
