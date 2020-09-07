using StokTakipUygulamasi.Class;
using StokTakipUygulamasi.UserController;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;

namespace StokTakipUygulamasi
{
    /// <summary>
    /// Anasayfa.xaml etkileşim mantığı
    /// </summary>
    public partial class Anasayfa : Window
    {
        public Anasayfa()
        {
            InitializeComponent();
            //this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            //this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight; Ekranın dışına kayma anlamına gelir
        }

        private void btnKapat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uc_cagir.uc_Ekle(Content_Icerik, new ucAnasayfa());
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            uc_cagir.uc_Ekle(Content_Icerik,new SatisYap());
        }

        private void btn_simgeDurumu_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnHamburgerMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (btnHamburgerMenu.Width != 50)
            {
                GridLength grd = new GridLength(80, GridUnitType.Pixel);
                grdClmn_menu.Width = grd;
                lblSolMenu1.Visibility = Visibility.Hidden;
                lblSolMenu2.Visibility = Visibility.Hidden;
                lblSolMenu3.Visibility = Visibility.Hidden;
                lblSolMenu4.Visibility = Visibility.Hidden;
                lblSolMenu5.Visibility = Visibility.Hidden;
                lblSolMenu6.Visibility = Visibility.Hidden;
                lblSolMenu7.Visibility = Visibility.Hidden;
                lblSolMenu8.Visibility = Visibility.Hidden;
                lblSolMenuReklam.Visibility = Visibility.Hidden;
                lblSolMenuResim.Visibility = Visibility.Hidden;
                lblSolMenuBaslik.Width = 0;
                btnHamburgerMenu.Width = 50;
                btnHamburgerMenu.Height = 50;
                lblSolMenuBaslikIcon.Width = 30;
            }
            else
            {
                GridLength grd = new GridLength(220, GridUnitType.Pixel);
                grdClmn_menu.Width = grd;
                lblSolMenu1.Visibility = Visibility.Visible;
                lblSolMenu2.Visibility = Visibility.Visible;
                lblSolMenu3.Visibility = Visibility.Visible;
                lblSolMenu4.Visibility = Visibility.Visible;
                lblSolMenu5.Visibility = Visibility.Visible;
                lblSolMenu6.Visibility = Visibility.Visible;
                lblSolMenu7.Visibility = Visibility.Visible;
                lblSolMenu8.Visibility = Visibility.Visible;
                lblSolMenuReklam.Visibility = Visibility.Visible;
                lblSolMenuResim.Visibility = Visibility.Visible;
                lblSolMenuBaslik.Width = 150;
                btnHamburgerMenu.Width = 80;
                btnHamburgerMenu.Height = 80;
                lblSolMenuBaslikIcon.Width = 24;
                lblSolMenuBaslikIcon.Height = 24;
            }
               
        }

        // region'u Gruplamak için kullanıyoruz.
        #region menubutonlar  
        int secimDurumu;

        private void menubuton_anasayfa_Click(object sender, RoutedEventArgs e)
        {
            secimDurumu = 1;
            secilenDurum();
            uc_cagir.uc_Ekle(Content_Icerik, new ucAnasayfa());
        }

        private void menubuton_indirimdekiler_Click(object sender, RoutedEventArgs e)
        {
            uc_cagir.uc_Ekle(Content_Icerik, new ucIndirimdekiler());
            secimDurumu = 2;
            secilenDurum();
        }

        private void btnUrunler_Click(object sender, RoutedEventArgs e)
        {
            uc_cagir.uc_Ekle(Content_Icerik, new ucUrunler());
            secimDurumu = 3;
            secilenDurum();
        }

        private void menubuton_siparisler_Click(object sender, RoutedEventArgs e)
        {
            secimDurumu = 4;
            secilenDurum();
        }

        private void menubuton_veresiye_Click(object sender, RoutedEventArgs e)
        {
            secimDurumu = 5;
            secilenDurum();
        }

        private void menubuton_kritikurunler_Click(object sender, RoutedEventArgs e)
        {
            secimDurumu = 6;
            secilenDurum();
        }

        private void menubuton_toptancilar_Click(object sender, RoutedEventArgs e)
        {
            secimDurumu = 7;
            secilenDurum();
        }

        private void menubuton_ayarlar_Click(object sender, RoutedEventArgs e)
        {
            secimDurumu = 8;
            secilenDurum();
        }

        void secilenDurum() // aktiflik durumu kontrol
        {
            if (secimDurumu == 1)
            {
                menubuton_anasayfa.IsChecked = true;
            }
            else
            {
                menubuton_anasayfa.IsChecked = false;
            }

            if (secimDurumu == 2)
            {
                menubuton_indirimdekiler.IsChecked = true;
            }
            else
            {
                menubuton_indirimdekiler.IsChecked = false;
            }

            if (secimDurumu == 3)
            {
                btnUrunler.IsChecked = true;
            }
            else
            {
                btnUrunler.IsChecked = false;
            }

            if (secimDurumu == 4)
            {
                menubuton_siparisler.IsChecked = true;
            }
            else
            {
                menubuton_siparisler.IsChecked = false;
            }

            if (secimDurumu == 5)
            {
                menubuton_veresiye.IsChecked = true;
            }
            else
            {
                menubuton_veresiye.IsChecked = false;
            }

            if (secimDurumu == 6)
            {
                menubuton_kritikurunler.IsChecked = true;
            }
            else
            {
                menubuton_kritikurunler.IsChecked = false;
            }

            if (secimDurumu == 7)
            {
                menubuton_toptancilar.IsChecked = true;
            }
            else
            {
                menubuton_toptancilar.IsChecked = false;
            }

            if (secimDurumu == 8)
            {
                menubuton_ayarlar.IsChecked = true;
            }
            else
            {
                menubuton_ayarlar.IsChecked = false;
            }

        }

  
        #endregion


        /*
        private void btn_tamEkran_Click(object sender, RoutedEventArgs e)  // Tam ekran butonu olsun istersek burayı kullanacağız
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        */
    }
}
