using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class Sohbet : System.Web.UI.Page
    {
        sqlConnection connection = new sqlConnection();
        static string etkinlikAdi = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];
            LoadEvents(kullaniciID);
            if (!IsPostBack)
            {

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
        }

        private void LoadEvents(int kullaniciid)
        {
            eventListContainer.Visible = true;

            try
            {
                string query = "SELECT e.EtkinlikID, e.EtkinlikAdi, e.EtkinlikTarihi, e.EtkinlikSaati FROM Tbl_Etkinlikler e " +
                    "INNER JOIN Tbl_Katilimcilar k ON e.EtkinlikID = k.EtkinlikID WHERE k.KullaniciID = @KullaniciID";
                if(kullaniciid==2)
                {
                    query = "SELECT e.EtkinlikID, e.EtkinlikAdi, e.EtkinlikTarihi, e.EtkinlikSaati FROM Tbl_Etkinlikler e ";
                }
                SqlCommand cmd = new SqlCommand(query, connection.baglanti());
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciid);

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
                        Text = "Mesajlar",
                        CssClass = "event-button",
                        CommandArgument = id.ToString()
                    };
                    btnDetaylar.Attributes["data-etiklikadi"] = ad;
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

        protected void DetaylarButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string etkinlikID = clickedButton.CommandArgument;
            Etkinlik.Etkinlikid = Convert.ToInt32(clickedButton.CommandArgument);
            etkinlikAdi = clickedButton.Attributes["data-etiklikadi"];
            int etkinlikid = Etkinlik.Etkinlikid;
            LoadChatMessages(etkinlikid);
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            int etkinlikID = Etkinlik.Etkinlikid; 
            int kullaniciID = (int)Session["KullaniciID"]; 
            string mesajMetni = txtMessage.Value.Trim(); 

            if (!string.IsNullOrEmpty(mesajMetni)) 
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Tbl_Mesajlar (GondericiID, AliciID, MesajMetni, GonderimZamani) " +
                        "VALUES (@GondericiID, @AliciID, @MesajMetni, @GonderimZamani)", connection.baglanti());
                    cmd.Parameters.AddWithValue("@GondericiID", kullaniciID);
                    cmd.Parameters.AddWithValue("@AliciID", etkinlikID);
                    cmd.Parameters.AddWithValue("@MesajMetni", mesajMetni);
                    cmd.Parameters.AddWithValue("@GonderimZamani", DateTime.Now);

                    cmd.ExecuteNonQuery();
                    connection.baglanti().Close();

                    LoadChatMessages(etkinlikID);
                    txtMessage.Value = "";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Boş mesaj gönderilemez.");
            }
        }


        private void LoadChatMessages(int etkinlikID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT m.MesajMetni, m.GonderimZamani, k.KullaniciAdi " +
                    "FROM Tbl_Mesajlar m " +
                    "INNER JOIN Tbl_Kullanicilar k ON m.GondericiID = k.KullaniciID " +
                    "WHERE m.AliciID = @EtkinlikID " +
                    "ORDER BY m.GonderimZamani ASC", connection.baglanti());
                cmd.Parameters.AddWithValue("@EtkinlikID", etkinlikID);

                SqlDataReader reader = cmd.ExecuteReader();

                string chatHtml = "";
                while (reader.Read())
                {
                    string mesajMetni = reader["MesajMetni"].ToString();
                    string kullaniciAdi = reader["KullaniciAdi"].ToString();
                    string gonderimZamani = Convert.ToDateTime(reader["GonderimZamani"]).ToString("dd/MM/yyyy HH:mm");

                    chatHtml += $@"
                <div class='chat-message'>
                    <span class='username'>{kullaniciAdi}</span>
                    <span class='timestamp'>{gonderimZamani}</span>
                    <p>{mesajMetni}</p>
                </div>";
                }

                ChatMessagesLiteral.Text =etkinlikAdi+"\n"+ chatHtml;

                reader.Close();
                connection.baglanti().Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                connection.baglanti().Close();
            }
        }

    }
}