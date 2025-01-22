using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class Profiller : System.Web.UI.Page
    {
        sqlConnection connection=new sqlConnection();
        Kullanicilar kullanicilar = new Kullanicilar();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            LoadKullanicilar();
        }
        private void LoadKullanicilar()
        {
            SqlCommand cmd = new SqlCommand(
                "SELECT KullaniciID, Ad, Soyad, KullaniciAdi FROM Tbl_Kullanicilar where KullaniciID <> 2",
                connection.baglanti()
            );

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int kullaniciID = Convert.ToInt32(reader["KullaniciID"]);
                    string ad = reader["Ad"].ToString();
                    string soyad = reader["Soyad"].ToString();
                    string kullaniciAdi = reader["KullaniciAdi"].ToString();

                    // Kullanıcı kartını oluştur
                    Panel userPanel = new Panel { CssClass = "user-card" };

                    Label nameLabel = new Label
                    {
                        Text = $"<h3>{ad} {soyad}</h3>",
                        CssClass = "user-title"
                    };
                    userPanel.Controls.Add(nameLabel);

                    Label usernameLabel = new Label
                    {
                        Text = $"<p>Kullanıcı Adı: {kullaniciAdi}</p>",
                        CssClass = "user-details"
                    };
                    userPanel.Controls.Add(usernameLabel);

                    Button btnDetaylar = new Button
                    {
                        Text = "Detaylar",
                        CssClass = "user-button",
                        CommandArgument = kullaniciID.ToString()
                    };
                    btnDetaylar.Click += new EventHandler(DetaylarButton_Click);
                    userPanel.Controls.Add(btnDetaylar);
                    Button btnSil = new Button
                    {
                        Text = "Sil",
                        CssClass = "user-button",
                        CommandArgument = kullaniciID.ToString()
                    };
                    btnSil.Click+= new EventHandler(KullanciSil_Click);
                    userPanel.Controls.Add(btnSil);

                    eventListContainer.Controls.Add(userPanel);
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
            string kullaniciID = clickedButton.CommandArgument;

            Response.Redirect("KullanicilarProfil.aspx?kullaniciid=" + kullaniciID);
        }
        protected void KullanciSil_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string kullaniciID = clickedButton.CommandArgument;
            if (kullanicilar.KullaniciSil(Convert.ToInt32(kullaniciID)))
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = "Silme İşlemi Başarılı!";
                lblMesaj.ForeColor = System.Drawing.Color.Green;
                string script = "setTimeout(function(){ window.location.href = 'Profiller.aspx'; }, 1000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            else 
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = "Silme İşlemi Başarısız!";
                lblMesaj.ForeColor = System.Drawing.Color.Red;
            } 
        }
    }
}