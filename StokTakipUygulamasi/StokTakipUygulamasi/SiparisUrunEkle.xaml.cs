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
        public SiparisUrunEkle(DataGrid grid)
        {
            this.grid = grid;
            InitializeComponent();
            UrunAdiComBox = Baglanti.ComboBoxVeriCekme(UrunAdiComBox, $@"select Urun_Adi from urunler", "Urun_Adi");
        }


        private void btnKapatSiparis(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UrunAdiComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Stoktaki Ürünü Bulmak İçin
            string UrunIdBulmaSorgu = $@"select ID from urunler where Urun_Adi= '{UrunAdiComBox.SelectedItem.ToString()}'";
            int UrunId = Baglanti.tekilUrunCekmeInt(UrunIdBulmaSorgu, "ID");
            String StokAdetiSorgu = $@"select Eldeki_Miktar from stok where Urun_ID = '{UrunId}'";
            int StokAdeti = Baglanti.tekilUrunCekmeInt(StokAdetiSorgu, "Eldeki_Miktar");

            // Ölçü Birimini Bulmak İçin
            string OlcuBirimiIdBulmaSorgu = $@"select Olcu_Birimi_ID from urunler where Urun_Adi= '{UrunAdiComBox.SelectedItem.ToString()}'";
            int olcuBirimiID = Baglanti.tekilUrunCekmeInt(OlcuBirimiIdBulmaSorgu, "Olcu_Birimi_ID");
            string OlcuBirimiBulmaSorgu = $@"select Olcu_Birimi from olcu_birimi where ID= '{olcuBirimiID}'";
            string olcuBirimi = Baglanti.tekilUrunCekmeString(OlcuBirimiBulmaSorgu, "Olcu_Birimi");

            txtStokAdeti.Text = StokAdeti.ToString();
            txtOlcuBirimi.Text = olcuBirimi.ToString();

        }

        private void btnUrunEkleSiparis(object sender, RoutedEventArgs e)
        {
            if (UrunAdiComBox.SelectedItem != null && txtSiparisAdeti.Text != "")
            {

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
