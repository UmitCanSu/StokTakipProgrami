using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakipUygulamasi.Class.Parametreler
{
    public class Prm
    {

        #region Static Parametreler
        public static sbyte Hata;
        public static string BilgiMesajiAlani;
        public static string BelgelerimYolu = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
        public static string ResimAdi;
        public static string BarkodNo;
        public static string grd_UrunlerListesi;
        public static bool checkbox_Satista_Olanlar;
        #endregion



        #region Ekleme / Güncelleme Parametreleri
        private string _ID;
        private string _urunAdi;
        private string _barkod_No;
        private string _aciklama;
        private string _resim;
        private Nullable<int> _kdv_Orani;
        private Nullable<int> _kar_Orani;
        private Nullable<int> _satis_Fiyati;
        private Nullable<int> _olcu_Birimi_ID;
        private string _olcu_Birimi;
        private bool _satista_Mi;
        private Nullable<int> _olcu_miktar;

        // Gelen değerlerin hepsinin baş harfini büyük yaptık. (CultureInfo ile)
        public string UrunAdi { get => _urunAdi; set => _urunAdi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value); }
        public string Barkod_No { get => _barkod_No; set => _barkod_No = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value); }
        public string Aciklama { get => _aciklama; set => _aciklama = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value); }
        public Nullable<int> KDV_Orani { get => _kdv_Orani; set => _kdv_Orani = value; }
        public Nullable<int> Kar_Orani { get => _kar_Orani; set => _kar_Orani = value; }
        public Nullable<int> Satis_Fiyati { get => _satis_Fiyati; set => _satis_Fiyati = value; }
        public Nullable<int> Olcu_Birimi_ID { get => _olcu_Birimi_ID; set => _olcu_Birimi_ID = value; }
        public bool Satista_Mi { get => _satista_Mi; set => _satista_Mi = value; }
        public string Resim { get => _resim; set => _resim = value; }
        public Nullable<int> Olcu_Miktar { get => _olcu_miktar; set => _olcu_miktar = value; }
        public string Olcu_Birimi { get => _olcu_Birimi; set => _olcu_Birimi = value; }
        public string ID { get => _ID; set => _ID = value; }

        #endregion


    }
}
