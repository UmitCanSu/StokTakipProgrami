﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using StokTakipUygulamasi.Class.Parametreler;

namespace StokTakipUygulamasi
{
    public class Baglanti
    {
        MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
        MySqlCommand cmd;
        MySqlDataReader reader;

        public void girisYap(string kadi, string sifre, Window w)
        {
            cmd = new MySqlCommand($@"Select * from calisanlar where Kadi='{kadi}' and Sifre='{sifre}'", baglan);
            baglan.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Giriş Başarılı!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Anasayfa ana = new Anasayfa();
                ana.Show();
                w.Close();
            }
            else
            {
                MessageBox.Show("Giriş Başarısız!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            baglan.Close();
        }


        public static bool SatistaOlmayanlar_GridiDoldur(DataGrid grd)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataAdapter adapter;
            sbyte i = 0;

            cmd = new MySqlCommand($@"select u.ID, u.Urun_Adi,u.Barkod_No,u.Aciklama,u.KDV_Orani,u.Kar_Orani,u.Satis_Fiyati,u.Satista_mi, ob.Olcu_Birimi 
                                    from urunler u  join olcu_birimi ob on u.Olcu_Birimi_ID = ob.ID  Where u.Satista_Mi=0", baglan);
            baglan.Open();
            try
            {
                adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grd.ItemsSource = null;
                grd.ItemsSource = dt.DefaultView;
                i = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                cmd.Dispose();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        // İndirimdekiler sayfasının verilerini çekiyoruz
        public static string[] indirimdekiler_guncelle_urun_Cek(string indirim_id)
        {
            string[] dizi = new string[12];
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand($@"Select u.Resim as 'Urun_Resmi', i.ID as 'Indirim_ID', u.ID as 'Urun_ID',u.Urun_Adi,u.Satis_Fiyati as 'Indirimsiz_Satis_Fiyati',  i.Baslangic_Tarihi, i.Bitis_Tarihi, i.Yuzde, i.Indirimde_mi, i.Indirim_Taban_Fiyati, i.Satis_Fiyati, s.Eldeki_Miktar as 'Stok_Adedi' from indirimdekiler i left join urunler u on i.Urunler_ID = u.ID left join stok s on s.Urun_ID = u.ID where i.ID={indirim_id}", baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    dizi[0] = reader["Indirim_ID"].ToString();
                    dizi[1] = reader["Urun_ID"].ToString();
                    dizi[2] = reader["Urun_Adi"].ToString();
                    dizi[3] = reader["Indirimsiz_Satis_Fiyati"].ToString();
                    dizi[4] = reader["Baslangic_Tarihi"].ToString();
                    dizi[5] = reader["Bitis_Tarihi"].ToString();
                    dizi[6] = reader["Yuzde"].ToString();
                    dizi[7] = reader["Indirimde_mi"].ToString();
                    dizi[8] = reader["Indirim_Taban_Fiyati"].ToString();
                    dizi[9] = reader["Satis_Fiyati"].ToString();
                    dizi[10] = reader["Stok_Adedi"].ToString();
                    dizi[11] = reader["Urun_Resmi"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return dizi;

        }

        // Ürünü indirimdekilerden Çıkar
        public static bool indirimdekilerden_cikar(string id)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            sbyte i = 0;
            cmd = new MySqlCommand($@"Update indirimdekiler set Indirimde_mi='0' where id='{id}'", baglan);
            try
            {
                baglan.Open();
                cmd.ExecuteNonQuery();
                i = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        // İndirimdekiler güncelleme işlemi

        public static bool IndirimdekilerGuncelle(Prm veri, string indirim_id)
        {
            sbyte donen = 0;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd = new MySqlCommand($@"update indirimdekiler set Baslangic_Tarihi=@Baslangic_Tarihi, Bitis_Tarihi=@Bitis_Tarihi, Yuzde=@Yuzde, Satis_Fiyati=@Satis_Fiyati, Indirim_Taban_Fiyati=@Indirim_Taban_Fiyati, Indirimde_mi=@Indirimde_mi where ID=@IndirimID", baglan);
            cmd.Parameters.AddWithValue("@Baslangic_Tarihi", veri.IndirimBaslangicTarihi);
            cmd.Parameters.AddWithValue("@Bitis_Tarihi", veri.IndirimBitisTarihi);
            cmd.Parameters.AddWithValue("@IndirimID", indirim_id);
            cmd.Parameters.AddWithValue("@Satis_fiyati", veri.IndirimliSatisFiyati);
            cmd.Parameters.AddWithValue("@Indirim_Taban_fiyati", veri.IndirimTabanFiyati);
            cmd.Parameters.AddWithValue("@Indirimde_mi", veri.Indirimde_mi);
            if (!string.IsNullOrEmpty(veri.IndirimYuzde.ToString()))  // KDV oranı null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Yuzde", veri.IndirimYuzde);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Yuzde", DBNull.Value);
            }

            try
            {
                baglan.Open();
                donen = 1;
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                baglan.Dispose();
            }

            if (donen > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Datagrid İndirimdekiler (İndiirmde olmayanlar) Doldurma Metodu
        public static bool Indirimde_Olmayanlar_IndirimdekilerGridiDoldur(DataGrid grd)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataAdapter adapter;
            sbyte i = 0;
            cmd = new MySqlCommand($@"Select i.ID as 'Indirim_ID', u.ID as 'Urun_ID',u.Urun_Adi,u.Satis_Fiyati as 'Indirimsiz_Satis_Fiyati',  i.Baslangic_Tarihi, i.Bitis_Tarihi, i.Yuzde, i.Indirimde_mi, i.Indirim_Taban_Fiyati, i.Satis_Fiyati, s.Eldeki_Miktar as 'Stok_Adedi' from indirimdekiler i left join urunler u on i.Urunler_ID = u.ID left join stok s on s.Urun_ID = u.ID where i.Indirimde_mi = 0", baglan);
            baglan.Open();
            try
            {
                adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grd.ItemsSource = null;
                grd.ItemsSource = dt.DefaultView;
                i = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }



        // Datagrid İndirimdekiler (İndiirmde olanlar) Doldurma Metodu
        public static bool Indirimde_Olanlar_IndirimdekilerGridiDoldur(DataGrid grd)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataAdapter adapter;
            sbyte i = 0;
            cmd = new MySqlCommand($@"Select i.ID as 'Indirim_ID', u.ID as 'Urun_ID',u.Urun_Adi,u.Satis_Fiyati as 'Indirimsiz_Satis_Fiyati',  i.Baslangic_Tarihi, i.Bitis_Tarihi, i.Yuzde, i.Indirimde_mi, i.Indirim_Taban_Fiyati, i.Satis_Fiyati, s.Eldeki_Miktar as 'Stok_Adedi' from indirimdekiler i left join urunler u on i.Urunler_ID = u.ID left join stok s on s.Urun_ID = u.ID where i.Indirimde_mi = 1", baglan);
            baglan.Open();
            try
            {
                adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grd.ItemsSource = null;
                grd.ItemsSource = dt.DefaultView;
                i = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        // Datagrid Doldurma metodu
        public static bool GridiDoldur(DataGrid grd)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataAdapter adapter;
            sbyte i = 0;

            cmd = new MySqlCommand($@"select u.ID, u.Urun_Adi,u.Barkod_No,u.Aciklama,u.KDV_Orani,u.Kar_Orani,u.Satis_Fiyati,u.Satista_mi, ob.Olcu_Birimi,u.Olcu_Miktar 
                                    from urunler u  join olcu_birimi ob on u.Olcu_Birimi_ID = ob.ID  Where u.Satista_Mi=1", baglan);
            baglan.Open();
            try
            {
                adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grd.ItemsSource = null;
                grd.ItemsSource = dt.DefaultView;
                i = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                cmd.Dispose();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool GridiDoldurGenel(DataGrid grd, String sorgu)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataAdapter adapter;
            sbyte i = 0;

            cmd = new MySqlCommand(sorgu, baglan);
            baglan.Open();
            try
            {
                adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grd.ItemsSource = null;
                grd.ItemsSource = dt.DefaultView;
                i = 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                cmd.Dispose();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // Ürünü silme fonksiyonu
        public static bool UrunSil(string gelen_id)
        {
            sbyte donen = 0;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd = new MySqlCommand($@"Delete from urunler where id={gelen_id}", baglan);
            try
            {
                baglan.Open();
                donen = 1;
                cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Hata: {e.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            if (donen > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        // Tüm ölçü birimlerini çekme fonksiyonu
        public static ComboBox OlcuBirimleri(ComboBox cmb)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand($@"Select * from olcu_birimi", baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmb.Items.Add(reader["Olcu_Birimi"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }
            return cmb;
        }

        public static ComboBox ComboBoxVeriCekme(ComboBox comboBox, String sorgu, String istenenDeger)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand(sorgu, baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader[istenenDeger]);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return comboBox;
        }

        public static string tekilUrunCekmeString(String sorgu, String istenenDeger)
        {
            String istenenSonuc = "";
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand(sorgu, baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    istenenSonuc = reader[istenenDeger].ToString();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return istenenSonuc;
        }


        public static int tekilUrunCekmeInt(String sorgu, String istenenDeger)
        {
            int istenenSonuc = 0;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand(sorgu, baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    istenenSonuc = Int32.Parse(reader[istenenDeger].ToString());

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return istenenSonuc;
        }


        // Combobox'a ürünleri doldurma ve ID ve Ürün Adı çekme fonksiyonu
        public static string[] tumUrunleriCek(ComboBox cmb)
        {
            string[] dizi = new string[11];
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand($@"select u.ID, u.Urun_Adi, u.Satis_Fiyati, s.Eldeki_Miktar as 'Stok_Adedi' from urunler u left join stok s on u.ID = s.Urun_ID;", baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmb.Items.Add(reader["Urun_Adi"]);
                    dizi[0] = reader["ID"].ToString();
                    dizi[1] = reader["Urun_Adi"].ToString();
                    dizi[2] = reader["Satis_Fiyati"].ToString();
                    dizi[3] = reader["Stok_Adedi"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return dizi;

        }






        // Ürün Adı ile ürünün bilgilerini çekme fonksiyonu (Stok adedi vs dahil)
        public static string[] isimleUrunBilgileriCek(string urun_Adi)
        {
            string[] dizi = new string[6];
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand($@"select u.ID, s.Eldeki_Miktar as 'Stok_Adedi', u.Urun_Adi,u.Satis_Fiyati, u.Resim, ob.ID as 'Olcu_Birimi_ID'
                                    from urunler u left join  stok s on s.Urun_ID = u.ID left join olcu_birimi ob on ob.ID = u.Olcu_Birimi_ID where u.Urun_Adi='{urun_Adi}'", baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    dizi[0] = reader["ID"].ToString();
                    dizi[1] = reader["Urun_Adi"].ToString();
                    dizi[2] = reader["Satis_Fiyati"].ToString();
                    dizi[3] = reader["Resim"].ToString();
                    dizi[4] = reader["Stok_Adedi"].ToString();
                    dizi[5] = reader["Olcu_Birimi_ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return dizi;

        }


        // ID ile Ürün bilgilerini çekme fonksiyonu
        public static string[] urunCek(string id)
        {
            string[] dizi = new string[11];
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand($@"select u.ID, u.Urun_Adi,u.Barkod_No,u.Aciklama,u.KDV_Orani,u.Kar_Orani,u.Satis_Fiyati,u.Satista_mi, u.Olcu_Miktar, u.Resim, ob.Olcu_Birimi 
                                    from urunler u  join olcu_birimi ob on u.Olcu_Birimi_ID = ob.ID where u.id={id}", baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    dizi[0] = reader["ID"].ToString();
                    dizi[1] = reader["Urun_Adi"].ToString();
                    dizi[2] = reader["Barkod_No"].ToString();
                    dizi[3] = reader["Aciklama"].ToString();
                    dizi[4] = reader["KDV_Orani"].ToString();
                    dizi[5] = reader["Kar_Orani"].ToString();
                    dizi[6] = reader["Satis_Fiyati"].ToString();
                    dizi[7] = reader["Olcu_Birimi"].ToString();
                    dizi[8] = reader["Satista_Mi"].ToString();
                    dizi[9] = reader["Olcu_Miktar"].ToString();
                    dizi[10] = reader["Resim"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return dizi;

        }


        // ID ile Ürün bilgilerini çekme fonksiyonu
        public static string[] SiparisCek(int id)
        {
            string[] dizi = new string[7];
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand($@"Select s.ID, u.Urun_Adi,o.Olcu_Birimi, u.Olcu_Miktar,s.Adet, s.Siparis_Tarihi, t.Toptanci_Adi, c.Ad,c.Soyad 
                            from urun_siparis s 
                            join olcu_birimi o on s.Urun_Olcu_Birimi_ID = o.ID 
                            join urunler u on u.ID= s.Urun_ID
                            join toptancilar t on t.ID = s.Toptanci_ID 
                            join calisanlar c on c.ID = s.Calisan_ID where s.ID =3", baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    
                    dizi[0] = reader["ID"].ToString();
                    dizi[1] = reader["Adet"].ToString();
                    dizi[2] = reader["Siparis_Tarihi"].ToString();
                    dizi[3] = reader["Urun_ID"].ToString();
                    dizi[4] = reader["Urun_Olcu_Birimi_ID"].ToString();
                    dizi[5] = reader["Toptanci_ID"].ToString();
                    dizi[6] = reader["Calisan_ID"].ToString();
               
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return dizi;

        }


        // Ölçü biriminin ID'sini bulma fonksiyonu
        public static int Olcu_Birimi_ID_Bul(string olcu_birimi_adi)
        {
            int donen = 0;
            MySqlDataReader reader;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd = new MySqlCommand($@"Select ID from olcu_birimi where Olcu_Birimi='{olcu_birimi_adi}'", baglan);
            try
            {
                baglan.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    donen = Convert.ToInt32(reader["ID"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Hata {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            return donen;
        }



        // Güncelleme İşlemi
        public static bool UrunuGuncelle(Prm veri, string id)
        {
            sbyte donen = 0;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd = new MySqlCommand($@"update urunler set Urun_Adi=@Urun_Adi, Barkod_No=@Barkod_No, Aciklama=@Aciklama, KDV_Orani=@KDV_Orani, Kar_Orani=@Kar_Orani, Satis_Fiyati=@Satis_Fiyati, Olcu_Birimi_ID=@Olcu_Birimi_ID, Satista_Mi=@Satista_Mi, Resim=@Resim, Olcu_Miktar=@Olcu_Miktar where ID=@ID", baglan);
            cmd.Parameters.AddWithValue("@Urun_Adi", veri.UrunAdi);
            cmd.Parameters.AddWithValue("@Satista_mi", veri.Satista_Mi);
            cmd.Parameters.AddWithValue("@ID", id);
            if (!string.IsNullOrEmpty(veri.KDV_Orani.ToString()))  // KDV oranı null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@KDV_Orani", veri.KDV_Orani);
            }
            else
            {
                cmd.Parameters.AddWithValue("@KDV_Orani", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Kar_Orani.ToString()))  // Kar oranı null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Kar_Orani", veri.Kar_Orani);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Kar_Orani", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Olcu_Birimi_ID.ToString()))  // Ölçü birimi null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Olcu_Birimi_ID", veri.Olcu_Birimi_ID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Olcu_Birimi_ID", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Satis_Fiyati.ToString()))  // Satış fiyatı null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Satis_Fiyati", veri.Satis_Fiyati);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Satis_Fiyati", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Barkod_No.ToString()))  // Barkod null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Barkod_No", veri.Barkod_No);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Barkod_No", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Aciklama.ToString())) // Açıklama null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Aciklama", veri.Aciklama);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Aciklama", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Resim))  // Resim null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Resim", veri.Resim);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Resim", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Olcu_Miktar.ToString()))  // Resim null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Olcu_Miktar", veri.Olcu_Miktar);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Olcu_Miktar", DBNull.Value);
            }


            try
            {
                baglan.Open();
                donen = 1;
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                baglan.Dispose();
            }

            if (donen > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // İndirimdekilere Ürün Ekleme Fonksiyonu
        public static bool IndirimlereEkle(Prm veri)
        {
            sbyte donen = 0;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd = new MySqlCommand("insert into indirimdekiler (Baslangic_Tarihi,Bitis_Tarihi,Yuzde,Satis_Fiyati,Indirimde_mi,Urunler_ID,Urunler_Olcu_Birimi_ID,Indirim_Taban_Fiyati) values (@BaslangicTarihi,@BitisTarihi,@Yuzde,@SatisFiyati,@IndirimdeMi,@UrunID,@OlcuBirimiID,@IndirimTabanFiyati)", baglan);
            cmd.Parameters.AddWithValue("@BaslangicTarihi", veri.IndirimBaslangicTarihi);
            cmd.Parameters.AddWithValue("@BitisTarihi", veri.IndirimBitisTarihi);
            cmd.Parameters.AddWithValue("@SatisFiyati", veri.IndirimliSatisFiyati);
            cmd.Parameters.AddWithValue("@IndirimdeMi", veri.Indirimde_mi);
            cmd.Parameters.AddWithValue("@OlcuBirimiID", veri.Olcu_Birimi_ID);
            cmd.Parameters.AddWithValue("@IndirimTabanFiyati", veri.IndirimTabanFiyati);
            cmd.Parameters.AddWithValue("@UrunID", veri.ID);
            if (!string.IsNullOrEmpty(veri.IndirimYuzde.ToString()))
            {
                cmd.Parameters.AddWithValue("@Yuzde", veri.IndirimYuzde);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Yuzde", DBNull.Value);
            }


            try
            {
                baglan.Open();
                donen = 1;
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                baglan.Dispose();
            }

            if (donen > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // Siparis Ürün Ekleme Fonksiyonu
        public static bool SiparislereEkle(Prm veri)
        {
            sbyte donen = 0;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd = new MySqlCommand("insert into urun_siparis(Adet, Siparis_Tarihi, Urun_ID, Urun_Olcu_Birimi_ID, Toptanci_ID, " +
                "Calisan_ID) values (@Adet, @Siparis_Tarihi, @Urun_ID, @Urun_Olcu_Birimi_ID, @Toptanci_ID, @Calisan_ID);", baglan);


            cmd.Parameters.AddWithValue("@Adet", veri.SiparisAdet);
            cmd.Parameters.AddWithValue("@Siparis_Tarihi", veri.SiparisTarihi);
            cmd.Parameters.AddWithValue("@Urun_ID", veri.UrunID);
            cmd.Parameters.AddWithValue("@Urun_Olcu_Birimi_ID", veri.Olcu_Birimi_ID);
            cmd.Parameters.AddWithValue("@Toptanci_ID", veri.ToptanciID);
            cmd.Parameters.AddWithValue("@Calisan_ID", veri.CalisanID);
      

            try
            {
                baglan.Open();
                donen = 1;
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                baglan.Dispose();
            }

            if (donen > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        // Ürün Ekleme işlemi
        public static bool EklemeIslemi(Prm veri)
        {
            sbyte donen = 0;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            //MySqlCommand cmd = new MySqlCommand($@"insert into urunler (Urun_Adi,Barkod_No,Aciklama,KDV_Orani,Kar_Orani,Satis_Fiyati,Olcu_Birimi_ID,Satista_mi) values ('{veri.UrunAdi}','{veri.Barkod_No}', '{veri.Aciklama}', '{veri.KDV_Orani}','{veri.Kar_Orani}','{veri.Satis_Fiyati}','{veri.Olcu_Birimi_ID}','{veri.Satista_Mi}') ",baglan);
            MySqlCommand cmd = new MySqlCommand("insert into urunler (Urun_Adi,Barkod_No,Aciklama,KDV_Orani,Kar_Orani,Satis_Fiyati,Olcu_Birimi_ID,Satista_mi,Olcu_Miktar,Resim) values (@Urun_Adi,@Barkod_No,@Aciklama,@KDV_Orani,@Kar_Orani,@Satis_Fiyati,@Olcu_Birimi_ID,@Satista_mi,@Olcu_Miktar,@Resim)", baglan);
            cmd.Parameters.AddWithValue("@Urun_Adi", veri.UrunAdi);
            cmd.Parameters.AddWithValue("@Satista_mi", veri.Satista_Mi);
            cmd.Parameters.AddWithValue("@Olcu_Miktar",veri.Olcu_Miktar);
            if (!string.IsNullOrEmpty(veri.KDV_Orani.ToString()))  // KDV oranı null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@KDV_Orani", veri.KDV_Orani);
            }
            else
            {
                cmd.Parameters.AddWithValue("@KDV_Orani", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Kar_Orani.ToString()))  // Kar oranı null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Kar_Orani", veri.Kar_Orani);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Kar_Orani", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Olcu_Birimi_ID.ToString()))  // Ölçü birimi null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Olcu_Birimi_ID", veri.Olcu_Birimi_ID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Olcu_Birimi_ID", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Satis_Fiyati.ToString()))  // Satış fiyatı null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Satis_Fiyati", veri.Satis_Fiyati);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Satis_Fiyati", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Barkod_No.ToString()))  // Barkod null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Barkod_No", veri.Barkod_No);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Barkod_No", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Aciklama.ToString())) // Açıklama null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Aciklama", veri.Aciklama);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Aciklama", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(veri.Resim))  // Resim null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@Resim", veri.Resim);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Resim", DBNull.Value);
            }




            try
            {
                baglan.Open();
                donen = 1;
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                baglan.Dispose();
            }

            if (donen > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


      

        // Çalışanları çekme fonksiyonu
        public static bool calisanlari_cek(DataGrid grd)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataAdapter adapter;
            sbyte i = 0;
            cmd = new MySqlCommand($@"Select * from calisanlar", baglan);
            baglan.Open();
            try
            {
                adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grd.ItemsSource = null;
                grd.ItemsSource = dt.DefaultView;
                i = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.ToString()}");
            }
            finally
            {
                baglan.Dispose();
            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
