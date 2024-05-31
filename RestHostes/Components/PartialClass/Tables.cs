using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RestHostes.Components
{
    public partial class Tables
    {
        public BitmapImage Images
        {
            get
            {
                if(NumberOfSeats == 4)
                {
                    return new BitmapImage(new Uri($"pack://application:,,,/Resources/Tables4.png", UriKind.Absolute));
                }
                else
                {
                    return new BitmapImage(new Uri($"pack://application:,,,/Resources/Table6.png", UriKind.Absolute)); ;
                }
                
            }
        }
    }
}
