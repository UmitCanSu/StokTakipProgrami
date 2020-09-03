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
    /// UrunEkle.xaml etkileşim mantığı
    /// </summary>
    public partial class UrunEkle : Window
    {
        public UrunEkle()
        {
            InitializeComponent();
        }
       
        private void btnKapat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void txtKDVOrani_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text,e.Text.Length-1)) // Harf girilmesini engelliyoruz.
            {
                e.Handled = true;
            }
        }

        private void txtKarOrani_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) // Harf girilmesini engelliyoruz.
            {
                e.Handled = true;
            }
        }

        private void txtSatisFiyati_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) // Harf girilmesini engelliyoruz.
            {
                e.Handled = true;
            }
        }

        // bool popupAcikMi = true; // Kişi elle kapatmak isterse diye
        private void btn_Bilgi_Click(object sender, RoutedEventArgs e)
        {

            Bonus.PopupShow(popup_bilgi);
            /*
             * Elle açıp kapatmak için
            if (popupAcikMi)
            {
                popup_bilgi.IsOpen = true;
                popupAcikMi = false;
            }
            else
            {
                popup_bilgi.IsOpen = false;
                popupAcikMi = true;
            }
            */
            
        }
        
        private void btn_Urunu_Ekle_Click(object sender, RoutedEventArgs e)
        {
            if (txtUrunAdi.Text != "" && cmb_UrunOlcuBirimi.Text != "")
            {
                Prm veri = new Prm();
                Prm.BarkodNo = txtBarkodNo.Text;
                veri.Olcu_Birimi_ID = Convert.ToInt32(cmb_UrunOlcuBirimi.Text);
                veri.UrunAdi = txtUrunAdi.Text;
                veri.Satista_Mi = checkbox_satistami.IsEnabled;
                veri.Barkod_No = txtBarkodNo.Text;
                veri.Aciklama = txtAciklama.Text;
                veri.Resim = Prm.ResimAdi;

                if (checkbox_satistami.IsChecked.Value)
                {
                    veri.Satista_Mi = true;
                }
                else
                {
                    veri.Satista_Mi = false;
                }

                if (txtKDVOrani.Text == "")
                {
                    veri.KDV_Orani = null;
                }
                else
                {
                    veri.KDV_Orani = Convert.ToInt32(txtKDVOrani.Text);
                }

                if (txtKarOrani.Text == "")
                {
                    veri.Kar_Orani = null;
                }
                else
                {
                    veri.Kar_Orani = Convert.ToInt32(txtKarOrani.Text);
                }

                if (txtSatisFiyati.Text == "")
                {
                    veri.Satis_Fiyati = null;
                }
                else
                {
                    veri.Satis_Fiyati = Convert.ToInt32(txtSatisFiyati.Text);
                }


                //ucUrunler uc = new ucUrunler();
                if (Baglanti.EklemeIslemi(veri))
                {
                    Prm.Hata = 0;
                    Prm.BilgiMesajiAlani = "Ürün başarıyla eklendi...";
                    BilgiEkrani be = new BilgiEkrani();
                    be.Show();
                }
                else
                {
                    Prm.Hata = 1;
                    Prm.BilgiMesajiAlani = "Veritabanına eklenirken bir sorun oldu!";
                    BilgiEkrani be = new BilgiEkrani();
                    be.Show();
                }

                
            }
            else
            {
                Prm.Hata = 1;
                Prm.BilgiMesajiAlani = "Ürün adı ve ölçü birimi boş olamaz!";
                BilgiEkrani be = new BilgiEkrani();
                be.Show();
            }
            

        }


        string SecilenResimAdi;
        private void btnResimEkle_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Belgelerim klasöründe StokTakipProgrami ve içinde Resimler klasörü yoksa oluştur diyoruz. Varsa zaten aşağıdaki işlemleri yapacak.
                if (!Directory.Exists(Prm.BelgelerimYolu+"\\StokTakipProgrami\\Resimler"))
                {
                    Directory.CreateDirectory(Prm.BelgelerimYolu+"\\StokTakipProgrami\\Resimler");  // Belgelerimin içine Resimler adlı klasör oluşturuyoruz.
                }

                // OpenFileDialog ile resim seçme işlemi yapıyoruz.
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Resim seç";
                dialog.InitialDirectory = "";  // Pencere açıldığında ilk olarak karşımıza hangi pencere gelsin onu seçiyoruz. Direkt Belgelerimi seçebiliriz yani.
                dialog.Filter = "Image Files (*.jpg;*.jpeg;)|*.jpg;*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";  // Seçilecek dosyayı filtreliyoruz. Resim seçmek istediğimiz için.
                dialog.FilterIndex = 1;
                if ((bool)dialog.ShowDialog())  // Seçim işlemi başarılı ise buraya girecek.
                {
                    // Openfile dialog ile seçilen resmi oluşturduğumuz klasör içerisine kopyalama işlemi.
                    SecilenResimAdi = dialog.FileName;
                    DateTime zaman = DateTime.Now;
                    string format = "dd-MM-yyyy-hh-mm-ss";
                    Prm.ResimAdi = Prm.BelgelerimYolu + "\\StokTakipProgrami\\Resimler\\"+Prm.BarkodNo+zaman.ToString(format)+".png"; // BarkodNo + Zaman ismini ver. (Şöyle bir dosya oluşturacağım diyoruz)
                    
                    File.Copy(SecilenResimAdi,Prm.ResimAdi,true);  // Aynı dosyayı iki kere oluşturuyoruz. Sadece ismini farklı yapıyoruz. Önceki resmi silmiyoruz.
                    

                    // Belgelerim içindeki resmimizin yolunu uriye çevirip img_UrunResmi yoluna verme.
                    BitmapImage img = new BitmapImage();  // Resmi anlık olarak değiştirmek için kullanıyoruz.
                    img.BeginInit();
                    img.UriSource = new Uri(@""+Prm.ResimAdi);
                    img.EndInit();
                    img_UrunResmi.Source = img;   // resmi burada değiştiriyoruz.


                    // Resim Başarıyla Eklendi Ekranı
                    Prm.Hata = 0;
                    BilgiEkrani be = new BilgiEkrani();
                    Prm.BilgiMesajiAlani = "Resim başarıyla eklendi...";
                    be.Show();
                }
                else
                {
                    // Resim Eklenemedi Ekranı
                    Prm.Hata = 1;
                    BilgiEkrani be = new BilgiEkrani();
                    Prm.BilgiMesajiAlani = "Resim eklenirken bir sorun oldu!";
                    be.Show();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
