using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class YeniHesapOlustur : System.Web.UI.Page
    {
        sqlConnection connection = new sqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInterests();
            lblErrorMessage.Visible = false;
        }

        protected void HesapOlustur_Click1(object sender, EventArgs e)
        {
            Kullanicilar yenikullanici=new Kullanicilar();
            string kullaniciAdi = Request.Form["username"];
            string sifre = Request.Form["password"];
            string eposta = Request.Form["email"];
            string ad = Request.Form["first_name"];
            string soyad = Request.Form["last_name"];
            string konum = Request.Form["location"];
            int ilgiAlanlari =Convert.ToInt32(ddlInterests.SelectedItem.Value);
            string dogumTarihi = Request.Form["birthdate"];
            string cinsiyet = Request.Form["gender"];
            string telefon = Request.Form["phone"];
            string profilFoto = "";

            if (profilePicture.HasFile)
            {
                // Yüklenen dosyanın adı
                string fileName = Path.GetFileName(profilePicture.FileName);

                // Dosyanın kaydedileceği dizin
                string uploadDirectory = "/uploads/";

                // Dosya yolunu oluştur
                profilFoto = uploadDirectory + fileName;

                // Sunucu yolunu fiziksel yola dönüştür
                string physicalPath = Server.MapPath(uploadDirectory);

                // Klasörün var olup olmadığını kontrol et, yoksa oluştur
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                // Dosyayı sunucuya kaydet
                profilePicture.SaveAs(Server.MapPath(profilFoto));
            }



            if (yenikullanici.YeniKayit(kullaniciAdi, sifre, eposta, ad, soyad, konum, ilgiAlanlari, dogumTarihi, cinsiyet, telefon,profilFoto))
            {
                Session["KullaniciAdi"] = kullaniciAdi;
                SqlCommand command = new SqlCommand("Select KullaniciID from Tbl_Kullanicilar where KullaniciAdi=@KullaniciAdi",connection.baglanti());
                command.Parameters.AddWithValue("@KullaniciAdi",kullaniciAdi);
                int Kullaniciid =Convert.ToInt32( command.ExecuteScalar());
                yenikullanici.PuanEkle(Kullaniciid, 20, DateTime.Today);
                Response.Redirect("Giris.aspx");
            }
            else
            {
                lblErrorMessage.Text = "Hatalı bilgi girişi! Kullanici adi veya E-posta benzer olabilir!";
                lblErrorMessage.Visible = true;
            }
        }
        public void LoadInterests()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT KategoriID, KategoriAdi FROM Tbl_Kategoriler", connection.baglanti());
                SqlDataReader reader = cmd.ExecuteReader();

                // DropDownList'e verileri ekle
                while (reader.Read())
                {
                    ddlInterests.Items.Add(new ListItem(reader["KategoriAdi"].ToString(), reader["KategoriID"].ToString()));
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

    }
}