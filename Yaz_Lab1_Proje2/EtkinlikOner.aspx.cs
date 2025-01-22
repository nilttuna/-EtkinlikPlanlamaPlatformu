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
    public partial class EtkinlikOner : System.Web.UI.Page
    {
        sqlConnection connection=new sqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];
            int etkinlikid =Convert.ToInt32(Request.QueryString["EtkinlikID"]);
            LoadOnerilenEtkinlikler(kullaniciID,etkinlikid);
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
                rapor.Visible = false;
            }
        }

        private void LoadOnerilenEtkinlikler(int kullaniciID, int etkinlikid)
        {
            // Mevcut etkinlikler için sorgu
            string mevcutEtkinliklerQuery = @"
    SELECT e.EtkinlikID, e.EtkinlikAdi, e.EtkinlikTarihi, e.EtkinlikSaati, e.EtkinlikKonum, k.KategoriAdi
    FROM Tbl_Etkinlikler e
    INNER JOIN Tbl_Kategoriler k ON e.EtkinlikKategorisi = k.KategoriID
    WHERE e.EtkinlikID IN (SELECT EtkinlikID FROM Tbl_Katilimcilar WHERE KullaniciID = @KullaniciID)
    ORDER BY e.EtkinlikTarihi ASC;
";

            // Alternatif etkinlikler için sorgu
            string alternatifEtkinliklerQuery = @"
    SELECT e.EtkinlikID, e.EtkinlikAdi, e.EtkinlikTarihi, e.EtkinlikSaati, e.EtkinlikKonum, k.KategoriAdi
    FROM Tbl_Etkinlikler e
    INNER JOIN Tbl_Kategoriler k ON e.EtkinlikKategorisi = k.KategoriID
    WHERE 
        e.EtkinlikKategorisi = (SELECT EtkinlikKategorisi FROM Tbl_Etkinlikler WHERE EtkinlikID = @EtkinlikID) -- Kategori aynı
        AND e.EtkinlikTarihi <> (SELECT EtkinlikTarihi FROM Tbl_Etkinlikler WHERE EtkinlikID = @EtkinlikID) -- Tarih farklı
        AND e.EtkinlikID NOT IN (SELECT EtkinlikID FROM Tbl_Katilimcilar WHERE KullaniciID = @KullaniciID) -- Kullanıcı daha önce katılmamış
        AND Onay=1
    ORDER BY e.EtkinlikTarihi ASC; -- Tarihe göre sıralama
";

            try
            {
                // Mevcut etkinlikler
                SqlCommand mevcutCmd = new SqlCommand(mevcutEtkinliklerQuery, connection.baglanti());
                mevcutCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                SqlDataReader reader = mevcutCmd.ExecuteReader();


                while (reader.Read())
                {
                    Panel eventPanel = new Panel { CssClass = "mevcut-event event-card" };

                    Label eventTitle = new Label { Text = $"<h3>{reader["EtkinlikAdi"]}</h3>", CssClass = "event-title" };
                    eventPanel.Controls.Add(eventTitle);

                    Label eventDetails = new Label
                    {
                        Text = $"<p>Tarih: {Convert.ToDateTime(reader["EtkinlikTarihi"]).ToString("dd.MM.yyyy")}</p><p>Saat: {reader["EtkinlikSaati"]}</p>",
                        CssClass = "event-details"
                    };
                    eventPanel.Controls.Add(eventDetails);

                    Button btnDetaylar = new Button
                    {
                        Text = "Detaylar",
                        CssClass = "event-card-button",
                        CommandArgument = reader["EtkinlikID"].ToString()
                    };
                    btnDetaylar.Click += new EventHandler(DetaylarButton_Click);
                    eventPanel.Controls.Add(btnDetaylar);

                    eventListContainer.Controls.Add(eventPanel);
                }
                reader.Close();
                connection.baglanti().Close();

                // Alternatif etkinlikler
                SqlCommand alternatifCmd = new SqlCommand(alternatifEtkinliklerQuery, connection.baglanti());
                alternatifCmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                alternatifCmd.Parameters.AddWithValue("@EtkinlikID", etkinlikid);

                reader = alternatifCmd.ExecuteReader();

                while (reader.Read())
                {
                    Panel eventPanel = new Panel { CssClass = "alternatif-event event-card" };

                    Label eventTitle = new Label { Text = $"<h3>{reader["EtkinlikAdi"]}</h3>", CssClass = "event-title" };
                    eventPanel.Controls.Add(eventTitle);

                    Label eventDetails = new Label
                    {
                        Text = $"<p>Tarih: {Convert.ToDateTime(reader["EtkinlikTarihi"]).ToString("dd.MM.yyyy")}</p><p>Saat: {reader["EtkinlikSaati"]}</p>",
                        CssClass = "event-details"
                    };
                    eventPanel.Controls.Add(eventDetails);

                    Button btnDetaylar = new Button
                    {
                        Text = "Detaylar",
                        CssClass = "event-card-button",
                        CommandArgument = reader["EtkinlikID"].ToString()
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