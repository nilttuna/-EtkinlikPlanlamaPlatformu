using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public class Kullanicilar
    {
        sqlConnection connection = new sqlConnection();

        public static int kullaniciid=0;
        string kullaniciAd {  get; set; }   
        string sifre {  get; set; } 

        public Boolean GirisKontrol(string ad,string sifre)
        {
           
            try
            {
                SqlCommand command = new SqlCommand("Select KullaniciAdi,Sifre, KullaniciID From Tbl_Kullanicilar where KullaniciAdi=@Kullaniciadi and Sifre=@Sifre", connection.baglanti());
                command.Parameters.AddWithValue("@KullaniciAdi", ad);
                command.Parameters.AddWithValue("@Sifre", sifre);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    kullaniciid =Convert.ToInt32(dr["KullaniciID"]);
                    connection.baglanti().Close();
                    return true;
                }
                else
                {
                    connection.baglanti().Close();
                    return false;
                }
            }
            catch 
            {

                connection.baglanti().Close();
                return false; 
            }

        }
        public Boolean YeniKayit(string kullaniciAdi, string sifre, string eposta, string ad, string soyad, string konum,int kategoriID, string dogumTarihi, string cinsiyet, string telefon,string dosyayolu)
        {
            try
            {

                SqlCommand kontrolCommand = new SqlCommand("SELECT COUNT(*) FROM Tbl_Kullanicilar WHERE KullaniciAdi = @KullaniciAdi OR Eposta = @Eposta", connection.baglanti());
                kontrolCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                kontrolCommand.Parameters.AddWithValue("@Eposta", eposta);

                int mevcutKayitSayisi = (int)kontrolCommand.ExecuteScalar();
                connection.baglanti().Close();

                if (mevcutKayitSayisi > 0)
                {
                    // Eğer kayıt varsa, yeni kayıt eklenmesin
                    return false;
                }
                SqlCommand yeniuyecommand = new SqlCommand("INSERT INTO Tbl_Kullanicilar (KullaniciAdi, Sifre, Eposta, Ad, Soyad, Konum, DogumTarihi, Cinsiyet, TelefonNumarasi, ProfilFotografi) VALUES (@KullaniciAdi, @Sifre, @Eposta, @Ad, @Soyad, @Konum, @DogumTarihi, @Cinsiyet, @Telefon, @ProfilFotografi)", connection.baglanti());
                yeniuyecommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                yeniuyecommand.Parameters.AddWithValue("@Sifre", sifre); 
                yeniuyecommand.Parameters.AddWithValue("@Eposta", eposta);
                yeniuyecommand.Parameters.AddWithValue("@Ad", ad);
                yeniuyecommand.Parameters.AddWithValue("@Soyad", soyad);
                yeniuyecommand.Parameters.AddWithValue("@Konum", konum);
                yeniuyecommand.Parameters.AddWithValue("@DogumTarihi", DateTime.Parse(dogumTarihi));
                yeniuyecommand.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                yeniuyecommand.Parameters.AddWithValue("@Telefon", telefon);
                yeniuyecommand.Parameters.AddWithValue("@ProfilFotografi", dosyayolu);

                int etkilenenSatir = yeniuyecommand.ExecuteNonQuery();
                if(etkilenenSatir > 0)
                {
                    connection.baglanti().Close();
                    SqlCommand getLastIdCommand = new SqlCommand("SELECT IDENT_CURRENT('Tbl_Kullanicilar')", connection.baglanti());
                    int kullaniciID = Convert.ToInt32(getLastIdCommand.ExecuteScalar());
                    // İlgi alanlarını ilişki tablosuna ekle
                    IlgiAlaniEkle(kullaniciID, kategoriID);
                    connection.baglanti().Close();
                    return true;
                }
                else
                {
                    connection.baglanti().Close();
                    return false;
                }

            }
            catch
            {
                connection.baglanti().Close();
                return false;
            }
            
        }

        public Boolean YeniSifre(string eposta,string yenisifre)
        {
            SqlCommand passwordcommand = new SqlCommand("Update Tbl_Kullanicilar Set Sifre=@Sifre where Eposta = @Eposta ", connection.baglanti());
            passwordcommand.Parameters.AddWithValue("@Eposta", eposta);
            passwordcommand.Parameters.AddWithValue("@Sifre", yenisifre);

            int etkilenensatir=passwordcommand.ExecuteNonQuery();

            if(etkilenensatir>0)
            {
                connection.baglanti().Close();  
                return true;
            }
            else
            {
                connection.baglanti().Close();  
                return false;
            }
        }

        public Boolean ProfilGuncelle(int id, string kullaniciad, string ad, string soyad, string dogumtarihi, string cinsiyet, string telefon, string eposta, string sifre, string konum, ControlCollection controls)
        {
            try
            {
                // Mevcut kullanıcının kullanıcı adı ve e-posta bilgilerini al
                SqlCommand mevcutBilgilerCommand = new SqlCommand("SELECT KullaniciAdi, Eposta FROM Tbl_Kullanicilar WHERE KullaniciID = @KullaniciID", connection.baglanti());
                mevcutBilgilerCommand.Parameters.AddWithValue("@KullaniciID", id);
                SqlDataReader reader = mevcutBilgilerCommand.ExecuteReader();

                string mevcutKullaniciAdi = string.Empty;
                string mevcutEposta = string.Empty;

                if (reader.Read())
                {
                    mevcutKullaniciAdi = reader["KullaniciAdi"].ToString();
                    mevcutEposta = reader["Eposta"].ToString();
                }
                connection.baglanti().Close();

                // Kullanıcı adı veya e-posta değiştiyse diğer kayıtları kontrol et
                if (kullaniciad != mevcutKullaniciAdi || eposta != mevcutEposta)
                {
                    SqlCommand kontrolCommand = new SqlCommand("SELECT COUNT(*) FROM Tbl_Kullanicilar WHERE (KullaniciAdi = @KullaniciAdi OR Eposta = @Eposta) AND KullaniciID != @KullaniciID", connection.baglanti());
                    kontrolCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciad);
                    kontrolCommand.Parameters.AddWithValue("@Eposta", eposta);
                    kontrolCommand.Parameters.AddWithValue("@KullaniciID", id);

                    int mevcutKayitSayisi = (int)kontrolCommand.ExecuteScalar();
                    connection.baglanti().Close();

                    if (mevcutKayitSayisi > 0)
                    {
                        return false;
                    }
                }
                using (SqlCommand passwordCommand = new SqlCommand("UPDATE Tbl_Kullanicilar SET KullaniciAdi=@KullaniciAdi, Ad=@Ad, Soyad=@Soyad, DogumTarihi=@DogumTarihi, Cinsiyet=@Cinsiyet, Eposta=@Eposta, TelefonNumarasi=@TelefonNumarasi, Sifre=@Sifre, Konum=@Konum WHERE KullaniciID=@KullaniciID", connection.baglanti()))
                {
                    passwordCommand.Parameters.AddWithValue("@KullaniciID", id);
                    passwordCommand.Parameters.AddWithValue("@KullaniciAdi", kullaniciad);
                    passwordCommand.Parameters.AddWithValue("@Ad", ad);
                    passwordCommand.Parameters.AddWithValue("@Soyad", soyad);
                    passwordCommand.Parameters.AddWithValue("@DogumTarihi", dogumtarihi);
                    passwordCommand.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                    passwordCommand.Parameters.AddWithValue("@TelefonNumarasi", telefon);
                    passwordCommand.Parameters.AddWithValue("@Eposta", eposta);
                    passwordCommand.Parameters.AddWithValue("@Sifre", sifre); 
                    passwordCommand.Parameters.AddWithValue("@Konum", konum);

                    int rowsAffected = passwordCommand.ExecuteNonQuery();
                    connection.baglanti().Close();
                    IlgiAlanlariniIsle(controls,id);

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Güncelleme hatası: " + ex.Message);
                return false;
            }
        }

        public void IlgiAlanlariniIsle(ControlCollection controls, int kullaniciID)
        {
            foreach (Control control in controls)
            {
                if (control is CheckBox chk)
                {
                    int kategoriID = Convert.ToInt32(chk.ID.Replace("chk_", ""));

                    if (chk.Checked) 
                    {
                        IlgiAlaniEkle(kullaniciID, kategoriID);
                    }
                    else 
                    {
                        IlgiAlaniSil(kullaniciID, kategoriID);
                    }
                }
            }
        }

        private void IlgiAlaniEkle(int kullaniciID, int kategoriID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_İlgiAlaniIliski (KullaniciID, KategoriID) VALUES (@KullaniciID, @KategoriID)", connection.baglanti());
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                cmd.Parameters.AddWithValue("@KategoriID", kategoriID);

                cmd.ExecuteNonQuery();
                connection.baglanti().Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                connection.baglanti().Close();
            }
        }
        private void IlgiAlaniSil(int kullaniciID, int kategoriID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Tbl_İlgiAlaniIliski WHERE KullaniciID = @KullaniciID AND KategoriID = @KategoriID", connection.baglanti());
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                cmd.Parameters.AddWithValue("@KategoriID", kategoriID);

                cmd.ExecuteNonQuery();
                connection.baglanti().Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                connection.baglanti().Close();
            }
        }

        public Boolean PuanEkle(int kullaniciid,int puan,DateTime tarih)
        {
            try
            {
                SqlCommand puaneklecommand = new SqlCommand("Insert into Tbl_Puanlar (KullaniciID, Puanlar, KazanilanTarih) Values (@KullaniciID, @Puanlar, @KazanilanTarih)", connection.baglanti());
                puaneklecommand.Parameters.AddWithValue("@KullaniciID", kullaniciid);
                puaneklecommand.Parameters.AddWithValue("@Puanlar", puan);
                puaneklecommand.Parameters.AddWithValue("@KazanilanTarih", tarih);

                puaneklecommand.ExecuteNonQuery();
                connection.baglanti().Close();
                return true;

            }
            catch (Exception ex)
            {
                connection.baglanti().Close();
                return false;
            }


        }

        public int PuanHesapla(int kullaniciid)
        {
            int toplamPuan = 0;

            try
            {
                string katilimQuery = "SELECT COUNT(*) FROM Tbl_Katilimcilar WHERE KullaniciID = @KullaniciID";
                using (SqlCommand cmd = new SqlCommand(katilimQuery, connection.baglanti()))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciid);
                    object katilimSonucu = cmd.ExecuteScalar();
                    toplamPuan += (katilimSonucu != DBNull.Value) ? Convert.ToInt32(katilimSonucu) * 10 : 0;
                }
                connection.baglanti().Close();

               
                string olusturmaQuery = "SELECT COUNT(*) FROM Tbl_Etkinlikler WHERE EkleyenID = @KullaniciID";
                using (SqlCommand cmd = new SqlCommand(olusturmaQuery, connection.baglanti()))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciid);
                    object olusturmaSonucu = cmd.ExecuteScalar();
                    toplamPuan += (olusturmaSonucu != DBNull.Value) ? Convert.ToInt32(olusturmaSonucu) * 15 : 0;
                }
                toplamPuan += 20;
                connection.baglanti().Close();

            }
            catch (Exception ex)
            {
                connection.baglanti().Close();
                toplamPuan = 0;
            }

            return toplamPuan;
        }

        public Boolean KullaniciSil(int kulaniciid)
        {
            SqlCommand etkinlikiliskisilcommand = new SqlCommand("Delete From Tbl_Etkinlikler where EkleyenID=@EkleyenID", connection.baglanti());
            etkinlikiliskisilcommand.Parameters.AddWithValue("@EkleyenID", kulaniciid);
            int etkilenensatir = etkinlikiliskisilcommand.ExecuteNonQuery();
            connection.baglanti().Close();
            if (etkilenensatir < 0)
            {
                return false;
            }
            SqlCommand kullaniciSil = new SqlCommand("Delete From Tbl_Kullanicilar where KullaniciID=@KullaniciID",connection.baglanti());
            kullaniciSil.Parameters.AddWithValue("@KullaniciID", kulaniciid);
            int etkilenensatir2 = kullaniciSil.ExecuteNonQuery();
            connection.baglanti().Close();
            if (etkilenensatir2 < 0)
            {
                return false;
            }
            return true;
        }



    }
}