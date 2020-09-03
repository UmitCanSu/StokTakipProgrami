using StokTakipUygulamasi.UserController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StokTakipUygulamasi.Class
{
   public class uc_cagir
    {
        public static void uc_Ekle(Grid grid, UserControl uc)
        {
            if (grid.Children.Count>0)
            {
                grid.Children.Clear();
                grid.Children.Add(uc);
            }
            else
            {
                grid.Children.Add(uc);
            }
        }


    }
}
