using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class KullanicilarProfil : System.Web.UI.Page
    {
        sqlConnection connection = new sqlConnection();
        Kullanicilar puan = new Kullanicilar();
        protected void Page_Load(object sender, EventArgs e)
        {
            int kullaniciID =Convert.ToInt32( Request.QueryString["kullaniciid"]);
            LoadIlgiAlanlari(kullaniciID);

            if (!IsPostBack)
            {
                LoadProfile(kullaniciID);
                int toplampuan = puan.PuanHesapla(kullaniciID);
                lblMessage.Text = "Puanınız: " + toplampuan;
                {
                    profil.InnerText = "Profiller";
                    profil.HRef = "\\Profiller.aspx";
                    profil.Visible = true; // Admin için görünür
                    rapor.InnerText = "Rapor";
                    rapor.HRef = "\\Rapor.aspx";
                    rapor.Visible = true; // Admin için görünür
                }

            }
            LoadEtkinlikler(kullaniciID);
        }

        private void LoadProfile(int kullaniciid)
        {
            string query = "SELECT * FROM Tbl_Kullanicilar WHERE KullaniciID = @KullaniciID";
            SqlCommand command = new SqlCommand(query, connection.baglanti());
            command.Parameters.AddWithValue("@KullaniciID", kullaniciid);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                // Bilgileri ASP.NET kontrollerine atama
                txtKullaniciAdi.Text = reader["KullaniciAdi"].ToString();
                txtAd.Text = reader["Ad"].ToString();
                txtSoyad.Text = reader["Soyad"].ToString();
                txtDogumTarihi.Text = Convert.ToDateTime(reader["DogumTarihi"]).ToString("yyyy-MM-dd");
                ddlCinsiyet.SelectedValue = reader["Cinsiyet"].ToString();
                txtTelefon.Text = reader["TelefonNumarasi"].ToString();
                txtEmail.Text = reader["Eposta"].ToString();
                txtSifre.Text = reader["Sifre"].ToString();
                txtKonum.Text = reader["Konum"].ToString();

                // Profil fotoğrafı yolu
                string resimYolu = "";
                if (reader["ProfilFotografi"] != DBNull.Value)
                {
                    resimYolu = reader["ProfilFotografi"].ToString();
                }

                imgProfilFoto.ImageUrl = string.IsNullOrEmpty(resimYolu) ? "images/kullanıcı.png" : resimYolu;
            }
            reader.Close();
            connection.baglanti().Close();

        }



        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            Kullanicilar kullanicilar = new Kullanicilar();
            int kullaniciid= Convert.ToInt32(Request.QueryString["kullaniciid"]);
            if (kullanicilar.ProfilGuncelle(kullaniciid, txtKullaniciAdi.Text, txtAd.Text, txtSoyad.Text, txtDogumTarihi.Text, ddlCinsiyet.SelectedValue, txtTelefon.Text, txtEmail.Text, txtSifre.Text, txtKonum.Text, pnlIlgiAlanlari.Controls))
            {
                lblMesaj.Text = "Güncelleme Başarılı";
            }
            else
            {
                lblMesaj.Text = "Güncelleme Başarısız";
            }
        }

        protected void btnFotoGuncelle_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Request.QueryString["kullaniciid"]);

            if (fileUploadProfilFoto.HasFile)
            {
                try
                {

                    string fileName = fileUploadProfilFoto.FileName;
                    string filePath = "~/Uploads/" + fileName;
                    string serverPath = Server.MapPath(filePath);

                    if (!System.IO.Directory.Exists(Server.MapPath("~/Uploads")))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads"));
                    }

                    fileUploadProfilFoto.SaveAs(serverPath);

                    SqlCommand cmd = new SqlCommand("UPDATE Tbl_Kullanicilar SET ProfilFotografi = @ProfilFotografi WHERE KullaniciID = @KullaniciID", connection.baglanti());
                    cmd.Parameters.AddWithValue("@ProfilFotografi", filePath); // Tam dosya yolunu kaydediyoruz
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    cmd.ExecuteNonQuery();
                    connection.baglanti();
                    imgProfilFoto.ImageUrl = filePath + "?v=" + DateTime.Now.Ticks; // Önbellek yenileme için sorgu dizesi ekleyin
                }
                catch (Exception ex)
                {
                    connection.baglanti();
                    Console.WriteLine(ex.Message);

                }
            }
        }

        private void LoadEtkinlikler(int kullaniciID)
        {
            // Veritabanı bağlantısını aç
            SqlCommand cmd = new SqlCommand(
                "SELECT e.EtkinlikID, e.EtkinlikAdi, e.EtkinlikTarihi, e.EtkinlikSaati " +
                "FROM Tbl_Etkinlikler e " +
                "INNER JOIN Tbl_Katilimcilar k ON e.EtkinlikID = k.EtkinlikID " +
                "WHERE k.KullaniciID = @KullaniciID",
                connection.baglanti()
            );

            cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["EtkinlikID"]);
                    string ad = reader["EtkinlikAdi"].ToString();
                    string tarih = Convert.ToDateTime(reader["EtkinlikTarihi"]).ToString("dd.MM.yyyy");
                    string saat = reader["EtkinlikSaati"].ToString();

                    Panel eventPanel = new Panel { CssClass = "event-card" };

                    Label eventTitle = new Label { Text = $"<h3>{ad}</h3>", CssClass = "event-title" };
                    eventPanel.Controls.Add(eventTitle);

                    Label eventDetails = new Label { Text = $"<p>Tarih: {tarih}</p><p>Saat: {saat}</p>", CssClass = "event-details" };
                    eventPanel.Controls.Add(eventDetails);
                    eventListContainer.Controls.Add(eventPanel);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                connection.baglanti().Close();
            }
        }
        public void LoadIlgiAlanlari(int kullaniciID)
        {
            try
            {
                // Tüm kategorileri getir
                SqlCommand cmd = new SqlCommand("SELECT KategoriID, KategoriAdi FROM Tbl_Kategoriler", connection.baglanti());
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Checkbox oluştur
                    CheckBox chk = new CheckBox
                    {
                        Text = reader["KategoriAdi"].ToString(),
                        ID = "chk_" + reader["KategoriID"].ToString(),
                        CssClass = "ilgi-checkbox"
                    };

                    // Kullanıcının bu kategoriyi seçip seçmediğini kontrol et
                    int kategoriID = Convert.ToInt32(reader["KategoriID"]);
                    if (KullaniciIlgiAlaniVarMi(kullaniciID, kategoriID))
                    {
                        chk.Checked = true; // Seçili hale getir
                    }

                    pnlIlgiAlanlari.Controls.Add(chk);
                }

                reader.Close();
                connection.baglanti().Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                connection.baglanti().Close();
            }
        }
        private bool KullaniciIlgiAlaniVarMi(int kullaniciID, int kategoriID)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Tbl_İlgiAlaniIliski WHERE KullaniciID = @KullaniciID AND KategoriID = @KategoriID", connection.baglanti());
            cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
            cmd.Parameters.AddWithValue("@KategoriID", kategoriID);

            int count = (int)cmd.ExecuteScalar();
            connection.baglanti().Close();

            return count > 0;
        }
    }
}