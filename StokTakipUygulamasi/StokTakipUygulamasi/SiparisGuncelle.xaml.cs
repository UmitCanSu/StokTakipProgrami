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

namespace StokTakipUygulamasi
{
    /// <summary>
    /// SiparisGuncelle.xaml etkileşim mantığı
    /// </summary>
    public partial class SiparisGuncelle : Window
    {
        public SiparisGuncelle()
        {
            InitializeComponent();
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
    }
}
