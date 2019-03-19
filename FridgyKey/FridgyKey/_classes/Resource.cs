using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FridgyKey 
{
    public static class Resource
    {
        public static BG bg; 
        public static void Get_BG(Canvas canv)
        {
            if (bg.r == 1)
            {
                canv.Background = bg.brush;
            }
            else if (bg.r == 0)
            {
                canv.Background = bg.Mcolor3;
            }
            else
            {
                canv.Background = bg.Mcolor;
            }
        }
    }
}
