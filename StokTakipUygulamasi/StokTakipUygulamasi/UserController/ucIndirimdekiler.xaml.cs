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

namespace StokTakipUygulamasi.UserController
{
    /// <summary>
    /// ucIndirimdekiler.xaml etkileşim mantığı
    /// </summary>
    public partial class ucIndirimdekiler : UserControl
    {
        Anasayfa gk = (Anasayfa)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive); // Anasayfa sayfasını kontrol etmek için o sayfayı çağırıyoruz.
        public ucIndirimdekiler()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Baglanti.Indirimde_Olanlar_IndirimdekilerGridiDoldur(dtg_IndirimdekilerListesi);
        }

        string id, ad;
        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_IndirimdekilerListesi.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Bir ürün seçiniz");
            }
            else
            {
                id = ((TextBlock)dtg_IndirimdekilerListesi.Columns[0].GetCellContent(dtg_IndirimdekilerListesi.SelectedItem)).Text;
                ad = ((TextBlock)dtg_IndirimdekilerListesi.Columns[1].GetCellContent(dtg_IndirimdekilerListesi.SelectedItem)).Text;
                UrunGuncelle ug = new UrunGuncelle(dtg_IndirimdekilerListesi, id);
                ug.Owner = gk;
                ug.ShowDialog();
            }
        }
    }
}
