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
    /// SatisYap.xaml etkileşim mantığı
    /// </summary>
    public partial class ucUrunler : UserControl
    {
        public ucUrunler()
        {
            InitializeComponent();
        }

        Anasayfa gk = (Anasayfa)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive); // Anasayfa sayfasını kontrol etmek için o sayfayı çağırıyoruz.
        private void btnUrunEkle_Click(object sender, RoutedEventArgs e)
        {
            UrunEkle urunEkle = new UrunEkle();
            urunEkle.Owner = gk;
            urunEkle.ShowDialog();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Baglanti.GridiDoldur(dtg_UrunlerListesi);
        }


        string urun_Adi,barkodNo;
        string id;
        private void dtg_UrunlerListesi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            barkodNo = ((TextBlock)dtg_UrunlerListesi.Columns[2].GetCellContent(dtg_UrunlerListesi.SelectedItem)).Text; // 0-1-2.hücredeki elemanı seçiyoruz.
            urun_Adi = ((TextBlock)dtg_UrunlerListesi.Columns[1].GetCellContent(dtg_UrunlerListesi.SelectedItem)).Text;
            id = ((TextBlock)dtg_UrunlerListesi.Columns[0].GetCellContent(dtg_UrunlerListesi.SelectedItem)).Text;
            MessageBox.Show(id+"    -   "+urun_Adi+"    -   "+barkodNo);
        }
    }
}
