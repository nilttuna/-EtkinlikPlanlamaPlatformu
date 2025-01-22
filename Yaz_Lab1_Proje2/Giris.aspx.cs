using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yaz_Lab1_Proje2
{
    public partial class Giris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;
        }

        protected void GirisYap_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Text;
            Kullanicilar kullanicilar = new Kullanicilar();

            if (kullanicilar.GirisKontrol(username, password))
            {
                Session["KullaniciID"] = Kullanicilar.kullaniciid;
                Session["KullaniciAdi"] = txtUsername.Text;
                Response.Redirect($"AnaSayfa.aspx?KullaniciID={Kullanicilar.kullaniciid}");

            }
            else
            {
                lblErrorMessage.Text = "Hatalı kullanıcı adı veya şifre!";
                lblErrorMessage.Visible = true; 
            }

        }
    }
}