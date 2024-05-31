using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBoss.Components
{
    public partial class Employee
    {
       public TimeSpan? staj
        {
            get
            {
               return   DateTime.Now - DateSt;
            }
        }
    }
}
