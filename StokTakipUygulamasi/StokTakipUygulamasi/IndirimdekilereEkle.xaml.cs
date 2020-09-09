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
    /// IndirimdekilereEkle.xaml etkileşim mantığı
    /// </summary>
    public partial class IndirimdekilereEkle : Window
    {
        Prm veri = new Prm();
        DataGrid grd;
        public IndirimdekilereEkle(DataGrid gelen_grid)
        {
            InitializeComponent();

            DateTime zaman = DateTime.Now;
            int yil = zaman.Year;
            int ay = zaman.Month;
            int gun = zaman.Day;

            this.grd = gelen_grid;

            string[] gelen_urunler = Baglanti.tumUrunleriCek(cmb_Urunler);
            txtIndirimsizFiyat.Text = gelen_urunler[2];
            txtStokAdedi.Text = gelen_urunler[3];

        }

        private void btnKapat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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
            double sonuc, indirimsiz, yuzde;
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

        private void cmb_Urunler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string secilenUrunAdi = cmb_Urunler.SelectedItem.ToString();
            string[] secilenUrunBilgileri = Baglanti.isimleUrunBilgileriCek(secilenUrunAdi);
            veri.ID = secilenUrunBilgileri[0];
            veri.Olcu_Birimi_ID = Convert.ToInt32(secilenUrunBilgileri[5]);

            MessageBox.Show(veri.Olcu_Birimi_ID.ToString());
            if (string.IsNullOrEmpty(secilenUrunBilgileri[4]))
            {
                txtStokAdedi.Text = "0".ToString();
            }
            else
            {
                txtStokAdedi.Text = secilenUrunBilgileri[4].ToString();
            }
        }

        private void btnIndirimliUrunuEkle_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_Urunler.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ürün seçiniz","Hata",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                veri.IndirimBaslangicTarihi = dateBaslangic.SelectedDate.GetValueOrDefault();
                veri.IndirimBitisTarihi = dateBitis.SelectedDate.GetValueOrDefault();
                veri.Indirimde_mi = checkbox_Indirimde_mi.IsChecked.Value;
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

                if (txtIndirimliFiyat.Text == "")
                {
                    Prm.Hata = 1;
                    Prm.BilgiMesajiAlani = "Lütfen ya indirim tutarını ya da indirim yüzdesini giriniz!";
                    BilgiEkrani be = new BilgiEkrani();
                    be.Show();
                }
                else
                {
                    veri.IndirimliSatisFiyati = Convert.ToInt32(float.Parse(txtIndirimliFiyat.Text.ToString()));
                }
                if (dateBaslangic.SelectedDate > dateBitis.SelectedDate)
                {
                    Prm.Hata = 1;
                    Prm.BilgiMesajiAlani = "İndirimin başlangıç zamanı bitiş zamanından sonra olamaz!";
                    BilgiEkrani be = new BilgiEkrani();
                    be.Show();
                }
                else
                {
                    if (Baglanti.IndirimlereEkle(veri))
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
                        Prm.BilgiMesajiAlani = "Seçtiğiniz ürün indirimdekilere başarıyla eklendi...";
                        BilgiEkrani be = new BilgiEkrani();
                        be.Show();
                        this.Close();
                    }
                    else
                    {
                        Prm.Hata = 1;
                        Prm.BilgiMesajiAlani = "Seçilen ürün indirimdekilere eklenirken bir sorun oldu!";
                        BilgiEkrani be = new BilgiEkrani();
                        be.Show();
                    }
                }
            }

           


        }
    }
}
