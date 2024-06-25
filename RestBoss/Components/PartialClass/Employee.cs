using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBoss.Components
{
    public partial class Employee
    {
       public string staj
        {
            get
            {
                //int years = DateTime.Now.Year - DateSt.Value.Month;

                //if (DateTime.Now.Month < DateSt.Value.Month || (DateTime.Now.Month == DateSt.Value.Month && DateTime.Now.Day < DateSt.Value.Day))
                //{
                //    years--;
                //}

                //return years;
                int years = DateTime.Now.Year - DateSt.Value.Year;
                int months = DateTime.Now.Month - DateSt.Value.Month;

                // Корректируем количество лет и месяцев, если текущий месяц и день меньше месяца и дня устройства
                if (DateTime.Now.Day < DateSt.Value.Day)
                {
                    months--;
                    if (months < 0)
                    {
                        years--;
                        months += 12;
                    }
                }
                else if (months < 0)
                {
                    years--;
                    months += 12;
                }
                return $"{years} лет, {months} мес.";
            }
        }
    }
}
