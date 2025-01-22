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
    public partial class EtkinlikDetay : System.Web.UI.Page
    {
        sqlConnection connection = new sqlConnection();

        string EtkinlikIsmi;
        DateTime EtkinlikTarihi;
        TimeSpan EtkinlikSaati;
        int EtkinlikSuresi;
        string EtkinlikAciklama;
        string EtkinlikKonumu;
        string EtkinlikKategorisi;
        int EtkinlikID = 1; // Örnek olarak 1. EtkinlikID'yi kullanıyoruz
        int KullaniciId = Kullanicilar.kullaniciid; 
        bool KatilimDurumu = false;
        int EtkinlikEkleyen;
        Kullanicilar kullanici = new Kullanicilar();
        Etkinlik etkinlik=new Etkinlik();

        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["etkinlikid"] != null)
                {
                    EtkinlikID = Convert.ToInt32(Request.QueryString["etkinlikid"]);
                    Etkinlik.Etkinlikid= Convert.ToInt32(Request.QueryString["etkinlikid"]);
                    LoadEventDetails();
                    CheckUserParticipation();

                    if (EtkinlikEkleyen == KullaniciId || KullaniciId==2)
                    {
                        if(KatilimDurumu)
                        {
                            lblStatus.Visible = true;
                        }
                        else
                        {
                            btnKatil.Visible = true;
                        }
                        btnGuncelle.Visible = true;
                        btnSil.Visible = true;

                        if(KullaniciId==2 && !etkinlik.OnayKontrol(EtkinlikID))
                        {
                            btnOnay.Visible = true;
                        }
                    }
                    else if (KatilimDurumu)
                    {
                        lblStatus.Visible = true;
                    }
                    else
                    {
                        btnKatil.Visible = true; 
                    }
                }
                else
                {
                    Response.Redirect("Anasayfa.aspx");
                }
            }
        }

        private void LoadEventDetails()
        {
            Etkinlik etkinlik = new Etkinlik();
            Etkinlik detay = etkinlik.EtkinlikDetayGetir(EtkinlikID);

            int dakika =Convert.ToInt32( detay.EtkinlikSuresi);
            TimeSpan time = TimeSpan.FromMinutes(dakika); // Dakikayı saat ve dakikaya dönüştür

            if ((int)time.TotalHours > 0) 
            {
                lblEtkinlikSuresi.Text = $"{(int)time.TotalHours} saat {time.Minutes} dakika";
            }
            else 
            {
                lblEtkinlikSuresi.Text = $"{time.Minutes} dakika";
            }

            lblEtkinlikAdi.Text = detay.EtkinlikAdi;
            lblEtkinlikAciklama.Text = detay.Aciklama;
            lblEtkinlikTarihi.Text = detay.EtkinlikTarihi;
            lblEtkinlikSaati.Text = detay.EtkinlikSaati;
            lblEtkinlikKonumu.Text = detay.EtkinlikKonum;
            lblEtkinlikKategorisi.Text = detay.EtkinlikKategorisi;
            EtkinlikEkleyen = detay.EkleyenID;
        }

        private void CheckUserParticipation()
        {
                string checkQuery = "SELECT COUNT(*) FROM Tbl_Katilimcilar WHERE KullaniciID = @KullaniciID AND EtkinlikID = @EtkinlikID";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection.baglanti());
                checkCommand.Parameters.AddWithValue("@KullaniciID", KullaniciId);
                checkCommand.Parameters.AddWithValue("@EtkinlikID", EtkinlikID);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    KatilimDurumu = true; 
                }
                connection.baglanti().Close();
            
        }

        protected void Katil_Click(object sender, EventArgs e)
        {
            EtkinlikID = Convert.ToInt32(Request.QueryString["etkinlikid"]);

            Etkinlik etkinlik = new Etkinlik();
            Etkinlik detay = etkinlik.EtkinlikDetayGetir(EtkinlikID);
            EtkinlikSaati = TimeSpan.Parse(detay.EtkinlikSaati);
            DateTime EtkinlikTarihi = DateTime.Parse(detay.EtkinlikTarihi);
            EtkinlikSuresi = Convert.ToInt32(detay.EtkinlikSuresi);
            if (etkinlik.CakismaKontrol(KullaniciId,EtkinlikTarihi,EtkinlikSaati,EtkinlikSuresi))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Bu tarihte ve saat aralığında bir etkinliğiniz zaten mevcut. 3 saniye içinde yönlendirileceksiniz...";
                string script = $"setTimeout(function(){{ window.location.href = 'EtkinlikOner.aspx?EtkinlikID={EtkinlikID}'; }}, 3000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            else
            {
                try
                {
                    SqlCommand katilcommand = new SqlCommand("Insert into Tbl_Katilimcilar (KullaniciID, EtkinlikID) values (@KullaniciID, @EtkinlikID)", connection.baglanti());
                    katilcommand.Parameters.AddWithValue("@KullaniciID", Kullanicilar.kullaniciid);
                    //Debug.WriteLine(EtkinlikID);
                    katilcommand.Parameters.AddWithValue("@EtkinlikID", EtkinlikID);
                    katilcommand.ExecuteNonQuery();
                    connection.baglanti().Close();
                    KatilimDurumu = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Katılım başarılı! 3 saniye içinde yönlendirileceksiniz...";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    kullanici.PuanEkle(KullaniciId, 10, DateTime.Today);
                }
                catch (Exception ex)
                {
                    connection.baglanti().Close();
                    KatilimDurumu = false;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Katılım işlemi sırasında bir hata oluştu. 3 saniye içinde yönlendirileceksiniz...";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                string script = "setTimeout(function(){ window.location.href = 'EtkinlikSayfasi.aspx'; }, 3000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
        }

        protected void Guncelle_Click(object sender, EventArgs e)
        {
            EtkinlikID = Convert.ToInt32(Request.QueryString["etkinlikid"]);

            Response.Redirect("YeniEtkinlik.aspx?etkinlikid="+EtkinlikID);
        }
        protected void Sil_Click(object sender, EventArgs e)
        {
            EtkinlikID = Convert.ToInt32(Request.QueryString["etkinlikid"]);

            Etkinlik sil = new Etkinlik();
            if (sil.EtkinlikSil(EtkinlikID))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Silme İşlemi Başarılı! 3 saniye içinde yönlendirileceksiniz...";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Silme İşlemi Başarısız! 3 saniye içinde yönlendirileceksiniz...";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            string script = "setTimeout(function(){ window.location.href = 'AnaSayfa.aspx'; }, 3000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        protected void Onayla_Click(object sender, EventArgs e)
        {
            EtkinlikID = Convert.ToInt32(Request.QueryString["etkinlikid"]);
            if (etkinlik.EtkinlikOnayla(EtkinlikID))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Onaylanama başarılı! 3 saniye içinde yönlendirileceksiniz...";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Onaylanama başarısız! 3 saniye içinde yönlendirileceksiniz...";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            string script = "setTimeout(function(){ window.location.href = 'EtkinlikSayfasi.aspx'; }, 3000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

        }
    }

}