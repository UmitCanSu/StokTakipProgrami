using Microsoft.Win32;
using StokTakipUygulamasi.Class;
using StokTakipUygulamasi.Class.Parametreler;
using StokTakipUygulamasi.UserController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StokTakipUygulamasi
{
    /// <summary>
    /// IndirimdekilerGuncelle.xaml etkileşim mantığı
    /// </summary>
    public partial class IndirimdekilerGuncelle : Window
    {
        Prm veri = new Prm();
        DataGrid grd;
        public IndirimdekilerGuncelle(DataGrid gelen_grid, string indirim_id)
        {
            InitializeComponent();

            DateTime zaman = DateTime.Now;
            int yil = zaman.Year;
            int ay = zaman.Month;
            int gun = zaman.Day;


            veri.Indirim_ID = indirim_id;
            this.grd = gelen_grid;
            string[] cek = Baglanti.indirimdekiler_guncelle_urun_Cek(indirim_id);
            txtUrunAdi.Text = cek[2];
            txtIndirimsizFiyat.Text = cek[3];
            dateBaslangic.DisplayDateStart = new DateTime(yil,ay,gun);
            dateBaslangic.Text = cek[4];
            dateBitis.DisplayDateStart = new DateTime(yil,ay,gun);
            dateBitis.Text = cek[5];
            txtYuzde.Text = cek[6];
            if (Convert.ToInt32(cek[7]) == 1)
            {
                checkbox_Indirimde_mi.IsChecked = true;
            }
            else
            {
                checkbox_Indirimde_mi.IsChecked = false;
            }
            txtTaban.Text = cek[8];
            txtIndirimliFiyat.Text = cek[9];
            txtStokAdedi.Text = cek[10];

            if (!string.IsNullOrEmpty(cek[11]))
            {
                BitmapImage img = new BitmapImage();  // Resmi anlık olarak değiştirmek için kullanıyoruz.
                img.BeginInit();
                img.UriSource = new Uri(@"" + cek[11]);
                img.EndInit();
                img_UrunResmi.Source = img;   // resmi burada değiştiriyoruz.
            }

            if (txtIndirimliFiyat.Text.Length > 0)
            {
                txtYuzde.Text = "";
                txtYuzde.IsEnabled = false;
            }
        }

        private void btnKapat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // bool popupAcikMi = true; // Kişi elle kapatmak isterse diye
        private void btn_Bilgi_Click(object sender, RoutedEventArgs e)
        {

            Bonus.PopupShow(popup_bilgi);

        }
        
        private void txtIndirimliFiyat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) // Harf girilmesini engelliyoruz.
            {
                e.Handled = true;
            }
        }
        
        private void txtYuzde_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) // Harf girilmesini engelliyoruz.
            {
                e.Handled = true;
            }
        }

        private void txtTaban_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) // Harf girilmesini engelliyoruz.
            {
                e.Handled = true;
            }
        }

        private void txtYuzde_TextChanged(object sender, TextChangedEventArgs e)
        {
           double sonuc,indirimsiz,yuzde;
            if (txtIndirimsizFiyat.Text == "")
            {
                indirimsiz = 0;
            }
            else
            {
                indirimsiz = Convert.ToDouble(txtIndirimsizFiyat.Text);
            }
           
            if (txtYuzde.Text.Length > 0)
            {
                yuzde = Convert.ToDouble(txtYuzde.Text);
                sonuc = (indirimsiz * yuzde) / 100;
                txtIndirimliFiyat.Text = sonuc.ToString();
                txtIndirimliFiyat.IsEnabled = false;
            }
            else
            {
                txtIndirimliFiyat.IsEnabled = true;
            }
            

        }

        private void txtIndirimliFiyat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtIndirimliFiyat.Text.Length > 0 && txtIndirimliFiyat.Text != "0")
            {
                txtYuzde.IsEnabled = false;
               
            }
            else
            {
                txtYuzde.IsEnabled = true;
            }
        }

        private void btnIndirimiGuncelle_Click(object sender, RoutedEventArgs e)
        {
            if (txtIndirimliFiyat.Text == "")
            {
                Prm.Hata = 1;
                Prm.BilgiMesajiAlani = "Lütfen indirim yüzdesi ya da indirimli fiyatı girin!";
                BilgiEkrani be = new BilgiEkrani();
                be.Show();
            }
            else
            {
                veri.IndirimBaslangicTarihi = dateBaslangic.SelectedDate.GetValueOrDefault();
                veri.IndirimBitisTarihi = dateBitis.SelectedDate.GetValueOrDefault();
                if (txtYuzde.Text == "")
                {
                    veri.IndirimYuzde = null;
                }
                else
                {
                    veri.IndirimYuzde = Convert.ToInt32(txtYuzde.Text);
                }
                if (txtTaban.Text == "")
                {
                    veri.IndirimTabanFiyati = 0;
                }
                else
                {
                    veri.IndirimTabanFiyati = Convert.ToInt32(txtTaban.Text);
                }
                veri.IndirimliSatisFiyati = Convert.ToInt32(txtIndirimliFiyat.Text);
                veri.Indirimde_mi = checkbox_Indirimde_mi.IsChecked.Value;

                if (dateBaslangic.SelectedDate > dateBitis.SelectedDate)
                {
                    Prm.Hata = 1;
                    Prm.BilgiMesajiAlani = "İndirimin başlangıç zamanı bitiş zamanından sonra olamaz!";
                    BilgiEkrani be = new BilgiEkrani();
                    be.Show();
                }
                else
                {
                    if (Baglanti.IndirimdekilerGuncelle(veri, veri.Indirim_ID))
                    {
                        if (Prm.checkbox_indirimde_olmayanlar == true)
                        {
                            Baglanti.Indirimde_Olmayanlar_IndirimdekilerGridiDoldur(grd);
                        }
                        else
                        {
                            Baglanti.Indirimde_Olanlar_IndirimdekilerGridiDoldur(grd);
                        }
                        Prm.Hata = 0;
                        Prm.BilgiMesajiAlani = "İndirim başarıyla güncellendi...";
                        BilgiEkrani be = new BilgiEkrani();
                        be.Show();
                    }
                    else
                    {
                        Prm.Hata = 1;
                        Prm.BilgiMesajiAlani = "İndirim güncellenirken bir sorun oldu!";
                        BilgiEkrani be = new BilgiEkrani();
                        be.Show();
                    }
                }

               
            }

           
        }
    }
}
