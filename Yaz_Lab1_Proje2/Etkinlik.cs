using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Yaz_Lab1_Proje2
{
    public class Etkinlik
    {
        sqlConnection connection = new sqlConnection();

        public static int Etkinlikid;
        public string EtkinlikAdi {  get; set; }
        public string Aciklama { get; set; }    
        public string EtkinlikTarihi { get; set; }
        public string EtkinlikSaati { get; set; }
        public string EtkinlikKonum { get; set; }
        public string EtkinlikKategorisi { get; set; }
        public string EtkinlikSuresi { get; set; }

        public int EkleyenID { get; set; }


        public Etkinlik EtkinlikDetayGetir(int etkinlikid)
        {
            Etkinlik detay=new Etkinlik();
            string query = "SELECT e.EkleyenID, e.EtkinlikAdi, e.Aciklama, e.EtkinlikTarihi, e.EtkinlikSaati, e.EtkinlikSuresi, e.EtkinlikKonum, k.KategoriAdi FROM Tbl_Etkinlikler e"+
                " INNER JOIN Tbl_Kategoriler k ON e.EtkinlikKategorisi = k.KategoriID WHERE e.EtkinlikID = @EtkinlikID";
            SqlCommand command = new SqlCommand(query, connection.baglanti());
            command.Parameters.AddWithValue("@EtkinlikID", etkinlikid);
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    detay.EtkinlikAdi = reader["EtkinlikAdi"].ToString();
                    detay.Aciklama = reader["Aciklama"].ToString();
                    detay.EtkinlikTarihi = Convert.ToDateTime(reader["EtkinlikTarihi"]).ToString("yyyy-MM-dd");
                    detay.EtkinlikSaati = reader["EtkinlikSaati"].ToString();
                    detay.EtkinlikSuresi = reader["EtkinlikSuresi"].ToString();
                    detay.EtkinlikKonum = reader["EtkinlikKonum"].ToString();
                    detay.EtkinlikKategorisi = reader["KategoriAdi"].ToString();
                    detay.EkleyenID = Convert.ToInt32(reader["EkleyenID"]); 
                }
                reader.Close();
                connection.baglanti().Close();
                return detay;

            }
            catch
            {
                reader.Close();
                connection.baglanti().Close();
                return detay;
            }
            
            
        }

        public Boolean YeniEtkinlikEkle(int ekleyenid, string etkinlikadi, string aciklama, string tarih, string saat, string sure, string konum, int kategori)
        {
            try
            {
                SqlCommand yenicommand = new SqlCommand("Insert into Tbl_Etkinlikler (EkleyenID, EtkinlikAdi, Aciklama, EtkinlikTarihi, EtkinlikSaati, EtkinlikSuresi, EtkinlikKonum, EtkinlikKategorisi)" +
                    " Values ( @EkleyenID, @EtkinlikAdi, @Aciklama, @EtkinlikTarihi, @EtkinlikSaati, @EtkinlikSuresi, @EtkinlikKonum, @EtkinlikKategorisi)", connection.baglanti());
                yenicommand.Parameters.AddWithValue("@EkleyenID", ekleyenid);
                yenicommand.Parameters.AddWithValue("@EtkinlikAdi", etkinlikadi);
                yenicommand.Parameters.AddWithValue("@Aciklama", aciklama);
                yenicommand.Parameters.AddWithValue("@EtkinlikTarihi", tarih);
                yenicommand.Parameters.AddWithValue("@EtkinlikSaati", saat);
                yenicommand.Parameters.AddWithValue("@EtkinlikSuresi", sure);
                yenicommand.Parameters.AddWithValue("@EtkinlikKonum", konum);
                yenicommand.Parameters.AddWithValue("@EtkinlikKategorisi", kategori);
                yenicommand.ExecuteNonQuery();
                connection.baglanti().Close();

                return true;

            }
            catch
            {
                connection.baglanti().Close();
                return false;
            }
        }

        public Boolean EtkinlikGuncelle(int ekleyenid, string etkinlikadi, string aciklama, string tarih, string saat, string sure, string konum, int kategori , int etkinlikid)
        {
            try
            {
                SqlCommand guncellecommand = new SqlCommand("Update Tbl_Etkinlikler SET EtkinlikAdi=@EtkinlikAdi, Aciklama=@Aciklama, EtkinlikTarihi=@EtkinlikTarihi, EtkinlikSaati=@EtkinlikSaati"+
                    ", EtkinlikSuresi=@EtkinlikSuresi, EtkinlikKonum=@EtkinlikKonum, EtkinlikKategorisi=@EtkinlikKategorisi where EtkinlikID=@EtkinlikID", connection.baglanti());
                guncellecommand.Parameters.AddWithValue("@EtkinlikAdi", etkinlikadi);
                guncellecommand.Parameters.AddWithValue("@Aciklama", aciklama);
                guncellecommand.Parameters.AddWithValue("@EtkinlikTarihi", tarih);
                guncellecommand.Parameters.AddWithValue("@EtkinlikSaati", saat);
                guncellecommand.Parameters.AddWithValue("@EtkinlikSuresi", sure);
                guncellecommand.Parameters.AddWithValue("@EtkinlikKonum", konum);
                guncellecommand.Parameters.AddWithValue("@EtkinlikKategorisi", kategori);
                guncellecommand.Parameters.AddWithValue("@EtkinlikID", etkinlikid);
                int etkilenensatir = guncellecommand.ExecuteNonQuery();
                if (etkilenensatir < 0)
                {
                    connection.baglanti().Close();

                    return false;
                }
                connection.baglanti().Close();
                return true;
            }
            catch
            {
                connection.baglanti().Close();
                return false;
            }
        }
        public Boolean EtkinlikSil(int etkinlikid)
        {
            try
            {
                SqlCommand iliskisilcommand=new SqlCommand("Delete From Tbl_Katilimcilar where EtkinlikID=@EtkinlikID",connection.baglanti());
                iliskisilcommand.Parameters.AddWithValue("@EtkinlikID", etkinlikid);
                int etkilenensatir=iliskisilcommand.ExecuteNonQuery();
                connection.baglanti().Close();
                if(etkilenensatir < 0)
                {
                    return false;
                }
                SqlCommand silcommand = new SqlCommand("Delete From Tbl_Etkinlikler where EtkinlikID=@EtkinlikID",connection.baglanti());
                silcommand.Parameters.AddWithValue("@EtkinlikID",etkinlikid);
                silcommand.ExecuteNonQuery();
                connection.baglanti().Close();
                return true;
            }
            catch
            {
                connection.baglanti().Close();
                return false;
            }

        }

        public bool CakismaKontrol(int kullaniciId, DateTime yeniEtkinlikTarihi, TimeSpan yeniEtkinlikBaslangic, int yeniEtkinlikSure)
        {
            List<(DateTime Tarih, TimeSpan Baslangic, TimeSpan Bitis)> kullaniciEtkinlikleri = new List<(DateTime, TimeSpan, TimeSpan)>();

            try
            {
                string sqlQuery = @" SELECT e.EtkinlikTarihi, e.EtkinlikSaati, e.EtkinlikSuresi FROM Tbl_Katilimcilar AS k INNER JOIN Tbl_Etkinlikler AS e ON k.EtkinlikID = e.EtkinlikID WHERE k.KullaniciID = @KullaniciID";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection.baglanti());
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime etkinlikTarihi = reader.GetDateTime(0);
                    TimeSpan etkinlikSaati = reader.GetTimeSpan(1);
                    int etkinlikSure = reader.GetInt32(2);

                    TimeSpan etkinlikBitis = etkinlikSaati.Add(TimeSpan.FromMinutes(etkinlikSure));

                    kullaniciEtkinlikleri.Add((etkinlikTarihi, etkinlikSaati, etkinlikBitis));
                }
                reader.Close();
                connection.baglanti().Close();
                TimeSpan yeniEtkinlikBitis = yeniEtkinlikBaslangic.Add(TimeSpan.FromMinutes(yeniEtkinlikSure));

                // Çakışma kontrolü
                foreach (var etkinlik in kullaniciEtkinlikleri)
                {
                    if (etkinlik.Tarih.Date == yeniEtkinlikTarihi.Date)
                    {
                        if (yeniEtkinlikBaslangic < etkinlik.Bitis && yeniEtkinlikBitis > etkinlik.Baslangic)
                        {
                            return true; // Çakışma var
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata oluştu: " + ex.Message);
            }

            return false; // Çakışma yok
        }

        public Boolean OnayKontrol(int etkinlikid)
        {
            string checkQuery = "SELECT Onay FROM Tbl_Etkinlikler WHERE EtkinlikID = @EtkinlikID";
            SqlCommand checkCommand = new SqlCommand(checkQuery, connection.baglanti());
            checkCommand.Parameters.AddWithValue("@EtkinlikID", etkinlikid);
            object result = checkCommand.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return Convert.ToBoolean(result);
            }
            return false;
        }
        public Boolean EtkinlikOnayla(int etkinlikid)
        {
            try
            {
                SqlCommand onaycommand = new SqlCommand("Update Tbl_Etkinlikler SET Onay=@Onay where EtkinlikID=@EtkinlikID", connection.baglanti());
                onaycommand.Parameters.AddWithValue("@EtkinlikID", etkinlikid);
                onaycommand.Parameters.AddWithValue("@Onay", 1);
                onaycommand.ExecuteNonQuery();
                connection.baglanti().Close();
                return true;
            }
            catch
            {
                connection.baglanti().Close();
                return false;
            } 
        }

    }
}