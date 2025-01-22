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
    public partial class AnaSayfa : System.Web.UI.Page
    {
        sqlConnection connection=new sqlConnection();
        Kullanicilar puan=new Kullanicilar();
        protected void Page_Load(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];
            LoadEtkinlikler(kullaniciID);
            LoadOnerilenEtkinlikler(kullaniciID);
            int toplampuan = puan.PuanHesapla(kullaniciID);
            lblMessage.Text="Puanınız: "+toplampuan;
            if (kullaniciID == 2)
            {
                profil.InnerText = "Profiller";
                profil.HRef = "\\Profiller.aspx";
                profil.Visible = true;
                rapor.InnerText = "Rapor";
                rapor.HRef = "\\Rapor.aspx";
                rapor.Visible = true; 
            }
            else
            {
                profil.Visible = false; 
                rapor.Visible=false;
            }
        }


        private void LoadEtkinlikler(int kullaniciID)
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT e.EtkinlikID, e.EtkinlikAdi, e.EtkinlikTarihi, e.EtkinlikSaati " +
                "FROM Tbl_Etkinlikler e "  +
                "WHERE e.EkleyenID = @KullaniciID",
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

                    Button btnDetaylar = new Button
                    {
                        Text = "Detaylar",
                        CssClass = "event-card-button",
                        CommandArgument = id.ToString()
                    };
                    btnDetaylar.Click += new EventHandler(DetaylarButton_Click);
                    eventPanel.Controls.Add(btnDetaylar);

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

        private void LoadOnerilenEtkinlikler(int kullaniciID)
        {
            // Veritabanı bağlantısını aç
            SqlCommand cmd = new SqlCommand(
    "SELECT DISTINCT e.EtkinlikID, e.EtkinlikAdi, e.EtkinlikTarihi, e.EtkinlikSaati, e.EtkinlikKonum, k.KategoriAdi, " +
    "CASE WHEN k.KategoriID IN ( SELECT KategoriID FROM Tbl_İlgiAlaniIliski WHERE KullaniciID = @KullaniciID ) AND e.EtkinlikKonum = ( SELECT Konum FROM Tbl_Kullanicilar WHERE KullaniciID = @KullaniciID ) THEN 1 " +
    "WHEN k.KategoriID IN (SELECT KategoriID FROM Tbl_İlgiAlaniIliski WHERE KullaniciID = @KullaniciID) THEN 2 " +
    "WHEN  k.KategoriID IN (SELECT e.EtkinlikKategorisi FROM Tbl_Etkinlikler e INNER JOIN Tbl_Katilimcilar kc ON e.EtkinlikID = kc.EtkinlikID WHERE kc.KullaniciID = @KullaniciID GROUP BY e.EtkinlikKategorisi ) THEN 3 " +
    "WHEN e.EtkinlikKonum = (SELECT Konum FROM Tbl_Kullanicilar WHERE KullaniciID = @KullaniciID) THEN 4 ELSE 5 END AS Priority " +
    "FROM Tbl_Etkinlikler e INNER JOIN Tbl_Kategoriler k ON e.EtkinlikKategorisi = k.KategoriID " +
    "WHERE (k.KategoriID IN (SELECT KategoriID FROM Tbl_İlgiAlaniIliski WHERE KullaniciID = @KullaniciID) " +
    "OR k.KategoriID IN (SELECT DISTINCT e.EtkinlikKategorisi FROM Tbl_Etkinlikler e INNER JOIN Tbl_Katilimcilar kc ON e.EtkinlikID = kc.EtkinlikID WHERE kc.KullaniciID = @KullaniciID) " +
    "OR e.EtkinlikKonum = (SELECT Konum FROM Tbl_Kullanicilar WHERE KullaniciID = @KullaniciID)) " +
    "AND e.EtkinlikID NOT IN (SELECT EtkinlikID FROM Tbl_Katilimcilar WHERE KullaniciID = @KullaniciID) AND Onay=1 " +
    "ORDER BY Priority ASC;",
    connection.baglanti()
);

            cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    Debug.WriteLine("Hiçbir etkinlik bulunamadı.");
                }
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

                    Button btnDetaylar = new Button
                    {
                        Text = "Detaylar",
                        CssClass = "event-card-button",
                        CommandArgument = id.ToString()
                    };
                    btnDetaylar.Click += new EventHandler(DetaylarButton_Click);
                    eventPanel.Controls.Add(btnDetaylar);

                    eventListContainer2.Controls.Add(eventPanel);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                connection.baglanti().Close();
            }
        }

        protected void DetaylarButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string etkinlikID = clickedButton.CommandArgument;
            Etkinlik.Etkinlikid = Convert.ToInt32(clickedButton.CommandArgument);


            Response.Redirect("EtkinlikDetay.aspx?etkinlikid=" + etkinlikID);
        }
    }
}