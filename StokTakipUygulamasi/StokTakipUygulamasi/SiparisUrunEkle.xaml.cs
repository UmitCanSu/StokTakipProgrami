using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using StokTakipUygulamasi.Class.Parametreler;

namespace StokTakipUygulamasi
{
    /// <summary>
    /// SiparisUrunEkle.xaml etkileşim mantığı
    /// </summary>
    public partial class SiparisUrunEkle : Window
    {
        DataGrid grid;
        int UrunID = 0;
        int OlcuBirimiID = 0;
        int Toptanci_ID = 0;
        public SiparisUrunEkle(DataGrid grid)
        {
            this.grid = grid;
            InitializeComponent();
            UrunAdiComBox = Baglanti.ComboBoxVeriCekme(UrunAdiComBox, $@"select DISTINCT Urun_Adi from urunler", "Urun_Adi");
            ToptancıAdiComBox = Baglanti.ComboBoxVeriCekme(ToptancıAdiComBox, $@"select Toptanci_Adi from toptancilar", "Toptanci_Adi");
        }


        private void btnKapatSiparis(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UrunAdiComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Stoktaki Ürünü Bulmak İçin
            string UrunIdBulmaSorgu = $@"select ID from urunler where Urun_Adi= '{UrunAdiComBox.SelectedItem.ToString()}'";
            UrunID = Baglanti.tekilUrunCekmeInt(UrunIdBulmaSorgu, "ID");
            String StokAdetiSorgu = $@"select Eldeki_Miktar from stok where Urun_ID = '{UrunID}'";
            int StokAdeti = Baglanti.tekilUrunCekmeInt(StokAdetiSorgu, "Eldeki_Miktar");

            // Ölçü Birimini Bulmak İçin
            string OlcuBirimiIdBulmaSorgu = $@"select Olcu_Birimi_ID from urunler where Urun_Adi= '{UrunAdiComBox.SelectedItem.ToString()}'";
            OlcuBirimiID = Baglanti.tekilUrunCekmeInt(OlcuBirimiIdBulmaSorgu, "Olcu_Birimi_ID");
            string OlcuBirimiBulmaSorgu = $@"select Olcu_Birimi from olcu_birimi where ID= '{OlcuBirimiID}'";
            string olcuBirimi = Baglanti.tekilUrunCekmeString(OlcuBirimiBulmaSorgu, "Olcu_Birimi");

            String comboVeriCekme =$@"select Olcu_Miktar from urunler where Urun_Adi = '{ UrunAdiComBox.SelectedItem}'";
            
            OlcüMiktariComboox = Baglanti.ComboBoxVeriCekme(OlcüMiktariComboox,comboVeriCekme, "Olcu_Miktar");
          
            txtStokAdeti.Text = StokAdeti.ToString();
            txtOlcuBirimi.Text = olcuBirimi.ToString();

        }
        private void TotanciAdiComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string ToptanciIDBulmaSorgu = $@"select ID from toptancilar where Toptanci_Adi ='{ToptancıAdiComBox.SelectedItem}' ";
            Toptanci_ID = Baglanti.tekilUrunCekmeInt(ToptanciIDBulmaSorgu,"ID");

         
        }
        private void OlcuMiktariComboox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void btnUrunEkleSiparis(object sender, RoutedEventArgs e)
        {
            if (UrunAdiComBox.SelectedItem != null && txtSiparisAdeti.Text != "" && OlcüMiktariComboox.SelectedItem != null)
            {
                Prm veri = new Prm();
                veri.Olcu_Birimi_ID = OlcuBirimiID;
                veri.UrunID = Baglanti.tekilUrunCekmeInt($@"select ID from urunler where Urun_Adi = '{UrunAdiComBox.SelectedItem.ToString()}' and Olcu_Miktar = '{OlcüMiktariComboox.SelectedItem.ToString()}'", "ID");
                veri.CalisanID = 1;
                veri.ToptanciID = Toptanci_ID;
                veri.SiparisAdet = Convert.ToInt32(txtSiparisAdeti.Text.ToString());
                veri.SiparisTarihi = dateSiparisTarihi.SelectedDate.GetValueOrDefault();
                veri.Olcu_Miktar = Convert.ToInt32(OlcüMiktariComboox.SelectedItem);

                Baglanti.SiparislereEkle(veri);
                String sorgu = $@"Select u.Urun_Adi,o.Olcu_Birimi, u.Olcu_Miktar,s.Adet, s.Siparis_Tarihi, t.Toptanci_Adi, c.Ad,c.Soyad 
                            from urun_siparis s 
                            join olcu_birimi o on s.Urun_Olcu_Birimi_ID = o.ID 
                            join urunler u on u.ID= s.Urun_ID
                            join toptancilar t on t.ID = s.Toptanci_ID 
                            join calisanlar c on c.ID = s.Calisan_ID;";
                Baglanti.GridiDoldurGenel(grid, sorgu);
              
            }
            else
            {
                Prm.Hata = 1;
                Prm.BilgiMesajiAlani = "Ürün adı ve sipariş adeti boş olamaz!";
                BilgiEkrani be = new BilgiEkrani();
                be.Show();
            }
        }

        private void txtSiparisAdeti_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) // Harf girilmesini engelliyoruz.
            {
                e.Handled = true;
            }
        }

       
    }
}
