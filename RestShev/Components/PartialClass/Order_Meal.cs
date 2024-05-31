using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RestShev.Components
{
   public partial class Order_Meal
    {
        public Visibility visabilityTable
        {
            get
            {
                if (Order.TableId != null)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }
    }
}
