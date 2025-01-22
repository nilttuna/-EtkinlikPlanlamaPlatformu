using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class Rapor : System.Web.UI.Page
    {
        sqlConnection connection = new sqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            int kullaniciID = (int)Session["KullaniciID"];
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

            if (!IsPostBack)
            {
                KullaniciSayisiYazdir(); 
                eventListContainer.Controls.Add(new Literal { Text = "<hr/>" }); 
                KategoriBilgileriYazdir(); 
            }
        }

        private void KullaniciSayisiYazdir()
        {
            string query = "SELECT COUNT(*) AS KullaniciSayisi FROM Tbl_Kullanicilar";

            SqlCommand cmd = new SqlCommand(query, connection.baglanti());

            try
            {
                int kullaniciSayisi = (int)cmd.ExecuteScalar();

                // Kullanıcı sayısını düzenli yazdır
                Literal kullaniciLiteral = new Literal
                {
                    Text = $"<h3>Kayıtlı Kullanıcı Sayısı:</h3><p><b>{kullaniciSayisi}</b></p>"
                };
                eventListContainer.Controls.Add(kullaniciLiteral);
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

        private void KategoriBilgileriYazdir()
        {
            string query = @"
                SELECT 
    k.KategoriAdi, 
    COUNT(DISTINCT e.EtkinlikID) AS EtkinlikSayisi,
    COUNT(kc.KullaniciID) AS KatilimciSayisi
FROM Tbl_Kategoriler k
LEFT JOIN Tbl_Etkinlikler e ON k.KategoriID = e.EtkinlikKategorisi
LEFT JOIN Tbl_Katilimcilar kc ON e.EtkinlikID = kc.EtkinlikID
GROUP BY k.KategoriAdi;
";

            SqlCommand cmd = new SqlCommand(query, connection.baglanti());

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();

                Literal baslikLiteral = new Literal
                {
                    Text = "<h3>Kategoriler:</h3><ul>"
                };
                eventListContainer.Controls.Add(baslikLiteral);

                while (reader.Read())
                {
                    string kategoriAdi = reader["KategoriAdi"].ToString();
                    int katilimciSayisi = Convert.ToInt32(reader["KatilimciSayisi"]);
                    int etkinlikSayisi = Convert.ToInt32(reader["EtkinlikSayisi"]);


                    Literal kategoriLiteral = new Literal
                    {
                        Text = $@"
                    <li>
                        <b>{kategoriAdi}</b>: {katilimciSayisi} katılımcı, {etkinlikSayisi} etkinlik
                    </li>"
                    };
                    eventListContainer.Controls.Add(kategoriLiteral);
                }

                Literal kapanisLiteral = new Literal
                {
                    Text = "</ul>"
                };
                eventListContainer.Controls.Add(kapanisLiteral);

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
    }
}
