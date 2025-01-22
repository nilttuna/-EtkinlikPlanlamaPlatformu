using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class SifremiUnuttum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SifreYenile_Click(object sender, EventArgs e)
        {
            Kullanicilar yenisifre = new Kullanicilar();
            string sifre = Request.Form["password"];
            string eposta = Request.Form["email"];
            if (yenisifre.YeniSifre(eposta,sifre))
            {
                Response.Redirect("Giris.aspx");
            }
            else
            {
                lblErrorMessage.Text = "Hatalı Eposta ";
                lblErrorMessage.Visible = true;
            }
        }
    }
}