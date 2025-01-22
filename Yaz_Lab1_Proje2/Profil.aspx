<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profil.aspx.cs" Inherits="Yaz_Lab1_Proje2.Profil" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Etkinlik Platformu - Profil</title>
    <style>
        /* Temel Stil Ayarları */
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: Arial, sans-serif;
        }

        body {
            display: flex;
            flex-direction: column;
            min-height: 100vh;
            background-color: #f3f4f6;
            color: #333;
        }

        /* Header Bölümü */
        header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 20px;
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
        }

        header .logo {
            font-size: 24px;
            font-weight: bold;
        }

        .search-bar-input {
            padding: 10px;
            border: none;
            border-radius: 8px;
            width: 200px;
        }

        .user-menu {
            display: flex;
            align-items: center;
        }

        .profile-img {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            margin-left: 10px;
            cursor: pointer;
        }

        /* Navigasyon Menüsü */
        nav {
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            padding: 10px 0;
            display: flex;
            justify-content: center;
        }

        nav a {
            color: white;
            padding: 10px 20px;
            text-decoration: none;
            font-weight: bold;
            transition: background-color 0.3s;
        }

        nav a:hover {
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
        }

        /* Ana İçerik Alanı */
        .content {
            flex: 1;
            padding: 20px;
            max-width: 1300px;
            margin: 0 auto;
            display: flex;
            gap: 20px;
        }

        .container {
            flex: 1;
            background-color: #fff;
            border-radius: 15px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
            padding: 30px;
            width: 1000px;
            height: 500px;
            transition: transform 0.3s;
            overflow-y: auto;
            overflow-x: hidden;
            max-height: 500px; /* Yüksekliği sınırla */
        }

        .container:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
        }

        .container h2 {
            margin-bottom: 20px;
            text-align: center;
            color: #444;
            font-size: 22px;
            border-bottom: 2px solid #8A2BE2;
            padding-bottom: 10px;
        }

        .form-label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .submit-button {
            padding: 10px 15px;
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            font-size: 14px;
            width: 100%;
        }

        .submit-button:hover {
            background-color: #6A1BBE;
        }

        .event-list {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 20px;
        }

        .event-card {
            background-color: #f9f9f9;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
            text-align: center;
            transition: transform 0.2s;
        }

        .event-card:hover {
            transform: scale(1.05);
        }

        .event-card h3 {
            color: #333;
            margin-bottom: 10px;
            font-size: 18px;
        }

        .event-card p {
            color: #555;
            margin-bottom: 10px;
        }

        .event-button {
            padding: 8px 12px;
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            transition: background-color 0.3s;
            font-size: 14px;
        }

        .event-button:hover {
            background-color: #5a73d8;
        }
        .profile-picture-container {
            grid-column: 1 / -1; /* Butonu iki sütun boyunca yay */
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-bottom: 20px;

}

        .profile-picture-container img {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            object-fit: cover;
            border: 3px solid #6A1BBE;
            margin-bottom: 10px;

             }

    .profile-picture-container input[type="file"] {
        display: none;
    }

    .profile-picture-container label {
           background: linear-gradient(135deg, #8A2BE2, #FFA500);;
            color: white;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;

    }
     .btn-guncelle {
            background: linear-gradient(135deg, #8A2BE2, #FFA500);;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin-top: 10px;
            font-size: 16px;
        }

      /* Hata mesajı */
        .error-message {
            color: red;
            text-align: center;
        }


    </style>
</head>
<body>

    <form id="form1" runat="server">
        <!-- Başlık Bölümü -->
        <header>
            <div class="logo">Etkinlik Platformu</div>
            <div class="search-bar">
            </div>
            <div class="user-menu">
                <span>Hoş Geldiniz, <% =Session["KullaniciAdi"] %></span>
            </div>
        </header>

        <!-- Navigasyon Menüsü -->
        <nav>
            <a href="\AnaSayfa.aspx">Ana Sayfa</a>
            <a href="\EtkinlikSayfasi.aspx">Etkinlik Sayfası</a>
            <a href="\Sohbet.aspx">Sohbet</a>
            <a href="\Profil.aspx">Profil</a>
            <a id="profil" runat="server" href="#">\Profiller</a>
            <a id="rapor" runat="server" href="#">Rapor</a>
            <a href="\Giris.aspx">Çıkış</a>
        </nav>

        <!-- Ana İçerik Alanı -->
        <div class="content">
            <!-- Kullanıcı Profil Bilgileri Konteyneri -->
            <div class="container">
                <h2>Profil Bilgilerim</h2>
                <asp:Panel ID="pnlProfile" runat="server">
                   <div class="profile-picture-container">
                       <asp:Image ID="imgProfilFoto" runat="server" Width="150" Height="150" />
            <label for="fileUploadProfilFoto">Fotoğraf Seç</label>
            <asp:FileUpload ID="fileUploadProfilFoto" runat="server" />
            <asp:Button ID="btnFotoGuncelle" runat="server" Text="Fotoğrafı Güncelle" OnClick="btnFotoGuncelle_Click" CssClass="btn-guncelle" />
        </div>
                   <asp:Label ID="lblMessage" Text="Puanınız:" runat="server" CssClass="form-label"  Visible="true"></asp:Label>

                    <asp:Label ID="lblKullaniciAdiText" runat="server" Text="Kullanıcı Adı:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtKullaniciAdi" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                    <asp:Label ID="lblAd" runat="server" Text="Ad:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtAd" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblSoyad" runat="server" Text="Soyad:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtSoyad" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblDogumTarihi" runat="server" Text="Doğum Tarihi:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtDogumTarihi" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblCinsiyet" runat="server" Text="Cinsiyet:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="ddlCinsiyet" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Erkek" Value="Erkek"></asp:ListItem>
                        <asp:ListItem Text="Kadın" Value="Kadın"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:Label ID="lblTelefon" runat="server" Text="Telefon:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblEmail" runat="server" Text="E-Posta:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblSifre" runat="server" Text="Şifre:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtSifre" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblKonum" runat="server" Text="Konum:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtKonum" runat="server" CssClass="form-control"></asp:TextBox>

                   <asp:Label ID="lblIlgiAlani" runat="server" Text="İlgi Alanları:" CssClass="form-label"></asp:Label>
<asp:Panel ID="pnlIlgiAlanlari" runat="server" CssClass="form-control" Style="height: auto; overflow-y: auto; border: 1px solid #ccc; padding: 10px;">
    <!-- Dinamik olarak doldurulacak -->
</asp:Panel>

                    <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" CssClass="submit-button" OnClick="btnGuncelle_Click" />

                      <!-- Hata Mesajı -->
        <asp:Label ID="lblMesaj" runat="server" CssClass="error-message" />

                </asp:Panel>
            </div>

            <!-- Kullanıcının Katıldığı Etkinlikler Konteyneri -->
            <div class="container">
    <h2>Katıldığım Etkinlikler</h2>
    <div class="event-list">
        <asp:PlaceHolder ID="eventListContainer" runat="server"></asp:PlaceHolder>
    </div>
</div>

        </div>
    </form>
</body>
</html>
