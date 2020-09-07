using System;
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


        // Datagrid İndirimdekiler Doldurma Metodu
        public static bool Indirimde_Olanlar_IndirimdekilerGridiDoldur(DataGrid grd)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataAdapter adapter;
            sbyte i = 0;
            cmd = new MySqlCommand($@"Select u.ID as 'Urun_ID',u.Urun_Adi,u.Satis_Fiyati as 'Indirimsiz_Satis_Fiyati', i.Baslangic_Tarihi, i.Bitis_Tarihi, i.Yuzde, i.Indirimde_mi, i.Indirim_Taban_Fiyati, i.Satis_Fiyati, s.Eldeki_Miktar as 'Stok_Adedi' from indirimdekiler i join urunler u on i.Urunler_ID = u.ID join stok s on s.Urun_ID = u.ID where i.Indirimde_mi = 1 ", baglan);
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

            cmd = new MySqlCommand($@"select u.ID, u.Urun_Adi,u.Barkod_No,u.Aciklama,u.KDV_Orani,u.Kar_Orani,u.Satis_Fiyati,u.Satista_mi, ob.Olcu_Birimi 
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

        public static ComboBox OlcuBirimleri(ComboBox cmb)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataReader reader;
            cmd = new MySqlCommand($@"Select * from olcu_birimi",baglan);
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


        public static int Olcu_Birimi_ID_Bul(string olcu_birimi_adi)
        {
            int donen = 0;
            MySqlDataReader reader;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd = new MySqlCommand($@"Select ID from olcu_birimi where Olcu_Birimi='{olcu_birimi_adi}'",baglan);
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
            cmd.Parameters.AddWithValue("@ID",id);
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


    // Ekleme işlemi
    public static bool EklemeIslemi(Prm veri)
        {
            sbyte donen = 0;
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            //MySqlCommand cmd = new MySqlCommand($@"insert into urunler (Urun_Adi,Barkod_No,Aciklama,KDV_Orani,Kar_Orani,Satis_Fiyati,Olcu_Birimi_ID,Satista_mi) values ('{veri.UrunAdi}','{veri.Barkod_No}', '{veri.Aciklama}', '{veri.KDV_Orani}','{veri.Kar_Orani}','{veri.Satis_Fiyati}','{veri.Olcu_Birimi_ID}','{veri.Satista_Mi}') ",baglan);
            MySqlCommand cmd = new MySqlCommand("insert into urunler (Urun_Adi,Barkod_No,Aciklama,KDV_Orani,Kar_Orani,Satis_Fiyati,Olcu_Birimi_ID,Satista_mi,Resim) values (@Urun_Adi,@Barkod_No,@Aciklama,@KDV_Orani,@Kar_Orani,@Satis_Fiyati,@Olcu_Birimi_ID,@Satista_mi,@Resim)", baglan);
            cmd.Parameters.AddWithValue("@Urun_Adi",veri.UrunAdi);
            cmd.Parameters.AddWithValue("@Satista_mi",veri.Satista_Mi);
            if (!string.IsNullOrEmpty(veri.KDV_Orani.ToString()))  // KDV oranı null mu yoksa bir değer gelmiş mi diye bakıyoruz.
            {
                cmd.Parameters.AddWithValue("@KDV_Orani",veri.KDV_Orani);
            }
            else
            {
                cmd.Parameters.AddWithValue("@KDV_Orani",DBNull.Value);
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


    }
}
