using StokTakipUygulamasi.Class.Parametreler;
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
        string id, urun_adi;
       
        
        public ucIndirimdekiler()
        {
            InitializeComponent();
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Baglanti.Indirimde_Olanlar_IndirimdekilerGridiDoldur(dtg_IndirimdekilerListesi);
           
        }

       


        private void check_Indirimde_Olmayanlar_Checked(object sender, RoutedEventArgs e)
        {
            Baglanti.Indirimde_Olmayanlar_IndirimdekilerGridiDoldur(dtg_IndirimdekilerListesi);
            Prm.checkbox_indirimde_olmayanlar = true;
        }

        private void check_Indirimde_Olmayanlar_Unchecked(object sender, RoutedEventArgs e)
        {
            Baglanti.Indirimde_Olanlar_IndirimdekilerGridiDoldur(dtg_IndirimdekilerListesi);
            Prm.checkbox_indirimde_olmayanlar = false;
        }

        private void btnCikar_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_IndirimdekilerListesi.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Bir ürün seçiniz");
            }
            else
            {
                id = ((TextBlock)dtg_IndirimdekilerListesi.Columns[0].GetCellContent(dtg_IndirimdekilerListesi.SelectedItem)).Text;
                urun_adi = ((TextBlock)dtg_IndirimdekilerListesi.Columns[2].GetCellContent(dtg_IndirimdekilerListesi.SelectedItem)).Text;
                MessageBoxResult result = MessageBox.Show($"{urun_adi} isimli ürünü indirimden çıkarmak istediğinize emin misiniz?", "EVET/HAYIR", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (Baglanti.indirimdekilerden_cikar(id))
                    {
                        if (Prm.checkbox_indirimde_olmayanlar)
                        {
                            Baglanti.Indirimde_Olmayanlar_IndirimdekilerGridiDoldur(dtg_IndirimdekilerListesi);
                        }
                        else
                        {
                            Baglanti.Indirimde_Olanlar_IndirimdekilerGridiDoldur(dtg_IndirimdekilerListesi);
                        }
                        Prm.Hata = 0;
                        Prm.BilgiMesajiAlani = "Ürün indirimdekilerden çıkarıldı";
                        BilgiEkrani be = new BilgiEkrani();
                        be.Show();
                    }
                    else
                    {
                        Prm.Hata = 1;
                        Prm.BilgiMesajiAlani = "Ürün indirimdekilerden çıkarılırken bir hata oldu!";
                        BilgiEkrani be = new BilgiEkrani();
                        be.Show();
                    }
                }
            }
            
            
        
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_IndirimdekilerListesi.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Bir ürün seçiniz");
            }
            else
            {
                id = ((TextBlock)dtg_IndirimdekilerListesi.Columns[0].GetCellContent(dtg_IndirimdekilerListesi.SelectedItem)).Text;
                IndirimdekilerGuncelle ig = new IndirimdekilerGuncelle(dtg_IndirimdekilerListesi,id);
                ig.Owner = gk;
                ig.ShowDialog();
            }
        }


       

    }
}
