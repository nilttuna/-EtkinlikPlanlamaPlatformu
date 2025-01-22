using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class YeniEtkinlik : System.Web.UI.Page
    {
        sqlConnection connection=new sqlConnection();
        Kullanicilar puanekle=new Kullanicilar();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string referrerUrl = Request.UrlReferrer?.ToString();

                if (!string.IsNullOrEmpty(referrerUrl))
                {
                    if (referrerUrl.Contains("EtkinlikDetay.aspx"))
                    {
                        btnGuncelle.Visible = true;
                        btnEkle.Visible = false;
                        LoadEtkinlikBilgileri();
                    }
                    else if (referrerUrl.Contains("AnaSayfa.aspx"))
                    {

                        btnEkle.Visible= true;
                    }
                }
                LoadEtkinlikKategorileri();

            }
        }

        private void LoadEtkinlikKategorileri()
        {
                string query = "SELECT KategoriID, KategoriAdi FROM Tbl_Kategoriler"; 
                SqlCommand cmd = new SqlCommand(query, connection.baglanti());
                SqlDataReader reader = cmd.ExecuteReader();
                ddlEtkinlikKategori.DataSource = reader;
                ddlEtkinlikKategori.DataTextField = "KategoriAdi";
                ddlEtkinlikKategori.DataValueField = "KategoriID";
                ddlEtkinlikKategori.DataBind();
                reader.Close(); 
                connection.baglanti().Close();

          
        }

        private void LoadEtkinlikBilgileri()
        {
            LoadEtkinlikKategorileri();
            Etkinlik detay= new Etkinlik();
            if (Request.QueryString["etkinlikid"] != null)
            {
               Etkinlik.Etkinlikid =Convert.ToInt32( Request.QueryString["etkinlikid"]);
            }
            detay = detay.EtkinlikDetayGetir(Etkinlik.Etkinlikid);

            txtEtkinlikAdi.Text=detay.EtkinlikAdi;
            txtAciklama.Text = detay.Aciklama;
            txtEtkinlikTarihi.Text = detay.EtkinlikTarihi;
            txtEtkinlikSaati.Text = detay.EtkinlikSaati;
            txtEtkinlikSuresi.Text = detay.EtkinlikSuresi;
            txtEtkinlikKonum.Text = detay.EtkinlikKonum;
            /*if (ddlEtkinlikKategori.Items.Count > 0)
            {
                Response.Write("DropDownList verileri başarıyla yüklendi.<br>");
            }
            else
            {
                Response.Write("DropDownList boş, veri bağlanmamış olabilir.<br>");
            }*/



        }

        protected void btnEkle_Click(object sender, EventArgs e)
        {
            Etkinlik yenietkinlik = new Etkinlik();
            if (yenietkinlik.YeniEtkinlikEkle(Kullanicilar.kullaniciid, txtEtkinlikAdi.Text, txtAciklama.Text, txtEtkinlikTarihi.Text, txtEtkinlikSaati.Text, txtEtkinlikSuresi.Text, txtEtkinlikKonum.Text,Convert.ToInt32( ddlEtkinlikKategori.SelectedValue)))
            {
                puanekle.PuanEkle(Kullanicilar.kullaniciid, 15, DateTime.Today);
                lblMessage.Visible = true;
                lblMessage.Text = "Ekleme başarılı! 3 saniye içinde yönlendirileceksiniz...";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Ekleme işlemi sırasında bir hata oluştu. 3 saniye içinde yönlendirileceksiniz...";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            string script = "setTimeout(function(){ window.location.href = 'AnaSayfa.aspx'; }, 3000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
             Etkinlik gunceletkinlik = new Etkinlik();
             if(gunceletkinlik.EtkinlikGuncelle(Kullanicilar.kullaniciid, txtEtkinlikAdi.Text, txtAciklama.Text, txtEtkinlikTarihi.Text, txtEtkinlikSaati.Text, txtEtkinlikSuresi.Text, txtEtkinlikKonum.Text, Convert.ToInt32(ddlEtkinlikKategori.SelectedValue), Etkinlik.Etkinlikid))
             {
                 lblMessage.Visible = true;
                 lblMessage.Text = "Güncelleme Başarılı! 3 saniye içinde yönlendirileceksiniz...";
                 lblMessage.ForeColor = System.Drawing.Color.Green;
             }
             else
             {
                 lblMessage.Visible = true;
                 lblMessage.Text = "Güncelleme işlemi sırasında bir hata oluştu. 3 saniye içinde yönlendirileceksiniz...";
                 lblMessage.ForeColor = System.Drawing.Color.Red;
             }
            string script = "setTimeout(function(){ window.location.href = 'AnaSayfa.aspx'; }, 3000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
    }
}