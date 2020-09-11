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
    /// ucSiparisler.xaml etkileşim mantığı
    /// </summary>
    public partial class ucSiparisler : UserControl
    {
        public ucSiparisler()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            String sorgu = $@"Select u.Urun_Adi,o.Olcu_Birimi, u.Olcu_Miktar,s.Adet, s.Siparis_Tarihi, t.Toptanci_Adi, c.Ad,c.Soyad 
                            from urun_siparis s 
                            join olcu_birimi o on s.Urun_Olcu_Birimi_ID = o.ID 
                            join urunler u on u.ID= s.Urun_ID
                            join toptancilar t on t.ID = s.Toptanci_ID 
                            join calisanlar c on c.ID = s.Calisan_ID;";
            Baglanti.GridiDoldurGenel(dtg_SiparisListesi, sorgu);
        }
        Anasayfa gk = (Anasayfa)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

        private void btnUrunEkleSiparis(object sender, RoutedEventArgs e)
        {
            SiparisUrunEkle siparisÜrünEkle = new SiparisUrunEkle(dtg_SiparisListesi);
            siparisÜrünEkle.Owner = gk;
            siparisÜrünEkle.ShowDialog();
            
        }


        private void btnGuncelleClick(object sender, RoutedEventArgs e)
        {
            SiparisGuncelle siparisGünceller = new SiparisGuncelle();
            siparisGünceller.Owner = gk;
            siparisGünceller.ShowDialog();
        }
    }
}
