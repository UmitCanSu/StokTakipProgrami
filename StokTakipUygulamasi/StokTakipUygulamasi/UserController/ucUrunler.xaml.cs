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
using StokTakipUygulamasi.Class.Parametreler;

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
            UrunEkle urunEkle = new UrunEkle(dtg_UrunlerListesi);
            urunEkle.Owner = gk;
            urunEkle.ShowDialog();
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Baglanti.GridiDoldur(dtg_UrunlerListesi);
        }


        string id,ad;

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_UrunlerListesi.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Bir ürün seçiniz");
            }
            else
            {
                id = ((TextBlock)dtg_UrunlerListesi.Columns[0].GetCellContent(dtg_UrunlerListesi.SelectedItem)).Text;
                ad = ((TextBlock)dtg_UrunlerListesi.Columns[1].GetCellContent(dtg_UrunlerListesi.SelectedItem)).Text;
                MessageBoxResult result = MessageBox.Show($"{ad} isimli ürünü silmek istediğinize emin misiniz?", "EVET/HAYIR", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (Baglanti.UrunSil(id))
                    {
                        if (Prm.checkbox_Satista_Olanlar == false)
                        {
                            Baglanti.SatistaOlmayanlar_GridiDoldur(dtg_UrunlerListesi);
                        }
                        else
                        {
                            Baglanti.GridiDoldur(dtg_UrunlerListesi);
                        }
                        Prm.Hata = 0;
                        Prm.BilgiMesajiAlani = "Ürün Başarıyla Silindi...";
                        BilgiEkrani be = new BilgiEkrani();
                        be.Show();
                    }
                    else
                    {
                        Prm.Hata = 1;
                        Prm.BilgiMesajiAlani = "Ürün Silinirken Bir Hata Oldu!";
                        BilgiEkrani be = new BilgiEkrani();
                        be.Show();
                    }
                }
            }


        }

        bool satista_olanlar = false;
        public void check_Satista_Olmayanlar_Checked(object sender, RoutedEventArgs e)
        {
            Baglanti.SatistaOlmayanlar_GridiDoldur(dtg_UrunlerListesi);
            Prm.checkbox_Satista_Olanlar = false;
        }


        public void check_Satista_Olmayanlar_Unchecked(object sender, RoutedEventArgs e)
        {
            Baglanti.GridiDoldur(dtg_UrunlerListesi);
            Prm.checkbox_Satista_Olanlar = true;
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_UrunlerListesi.SelectedItem == null)
            {
                MessageBox.Show("Lütfen Bir ürün seçiniz");
            }
            else
            {
                id = ((TextBlock)dtg_UrunlerListesi.Columns[0].GetCellContent(dtg_UrunlerListesi.SelectedItem)).Text;
                ad = ((TextBlock)dtg_UrunlerListesi.Columns[1].GetCellContent(dtg_UrunlerListesi.SelectedItem)).Text;
                UrunGuncelle ug = new UrunGuncelle(dtg_UrunlerListesi, id);
                ug.Owner = gk;
                ug.ShowDialog();
            }
           
        }
    }
}
