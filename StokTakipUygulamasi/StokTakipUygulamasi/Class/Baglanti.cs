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
        MySqlDataAdapter adapter;

        public void girisYap(string kadi,string sifre, Window w)
        {
            cmd = new MySqlCommand($@"Select * from calisanlar where Kadi='{kadi}' and Sifre='{sifre}'",baglan);
            baglan.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Giriş Başarılı!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                Anasayfa ana = new Anasayfa();
                ana.Show();
                w.Close();
            }
            else
            {
                MessageBox.Show("Giriş Başarısız!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            baglan.Close();
        }


        // Datagrid Doldurma metodu
        public static bool GridiDoldur(DataGrid grd)
        {
            MySqlConnection baglan = new MySqlConnection("Server=localhost;Database=stoktakipvt;Uid=root;Pwd=;Charset=utf8");
            MySqlCommand cmd;
            MySqlDataAdapter adapter;
            sbyte i = 0;

            cmd = new MySqlCommand($@"select u.ID, u.Urun_Adi,u.Barkod_No,u.Aciklama,u.KDV_Orani,u.Kar_Orani,u.Satis_Fiyati,u.Satista_mi, ob.Olcu_Birimi 
                                    from urunler u  join olcu_birimi ob on u.Olcu_Birimi_ID = ob.ID ",baglan);
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

            if (i>0)
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
