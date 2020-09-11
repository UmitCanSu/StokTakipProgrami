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
    /// SiparisGuncelle.xaml etkileşim mantığı
    /// </summary>
    public partial class SiparisGuncelle : Window
    {
        DataGrid grid;
        int Siparis_ID;
        Prm veri;
        public SiparisGuncelle(DataGrid grid, int Siparis_ID )
        {
         
            this.grid = grid;
            this.Siparis_ID = Siparis_ID;
            InitializeComponent();
            String[] cek = Baglanti.SiparisCek(Siparis_ID);
           
        }

        private void UrunAdiComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OlcuMiktariComboox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TotanciAdiComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUrunGuncelleSiparis(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtSiparisAdeti_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnKapatSiparis(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtSiparisAdeti.Text = Siparis_ID.ToString();
        }
    }
}
