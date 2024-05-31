using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RestWaiter.Components
{
    public partial class Order_Meal
    {
     public Visibility visabilitybt
        {
            get
            {
                if (StatusId == 1)
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
