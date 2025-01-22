using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class EtkinlikSayfasi : System.Web.UI.Page
    {
        sqlConnection connection = new sqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEvents();
                    if (Kullanicilar.kullaniciid == 2)
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

        private void LoadEvents()
        {
            EventListLiteral.Visible = true;
           
            try
            {
                string query = "SELECT EtkinlikID, EtkinlikAdi, EtkinlikTarihi, EtkinlikSaati FROM Tbl_Etkinlikler where Onay = 1";
                if(Kullanicilar.kullaniciid==2)
                {
                    query= "SELECT EtkinlikID, EtkinlikAdi, EtkinlikTarihi, EtkinlikSaati, Onay FROM Tbl_Etkinlikler";
                }
                SqlCommand cmd = new SqlCommand(query, connection.baglanti());
                SqlDataReader reader = cmd.ExecuteReader();

                string eventsHtml = "";
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["EtkinlikID"]);
                    string ad = reader["EtkinlikAdi"].ToString();
                    string tarih = Convert.ToDateTime(reader["EtkinlikTarihi"]).ToString("dd.MM.yyyy");
                    string saat = reader["EtkinlikSaati"].ToString();
                    //Etkinlik.Etkinlikid = id;
                    eventsHtml += $@"
                        <div class='event-card'>
                            <h3><a href='EtkinlikDetay.aspx?etkinlikid={id}'>{ad}</a></h3>
                            <p>Tarih: {tarih}</p>
                            <p>Saat: {saat}</p>
                            <a href='EtkinlikDetay.aspx?etkinlikid={id}'>
            <button>Detay Görüntüle</button>
        </a>";
                    if (Kullanicilar.kullaniciid == 2 && Convert.ToInt32(reader["Onay"]) != 1)
                    {
                        eventsHtml += "<label class='onay-label'>Onaylı Değil</label>";
                    }

                    eventsHtml +="</div > ";
                }

                EventListLiteral.Text = eventsHtml;
                reader.Close();
                connection.baglanti().Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Hata:"+ ex.Message);
                connection.baglanti().Close();
              
            }
           
        }
    }
}