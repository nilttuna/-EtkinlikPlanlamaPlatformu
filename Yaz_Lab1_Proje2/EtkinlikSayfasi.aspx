<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikSayfasi.aspx.cs" Inherits="Yaz_Lab1_Proje2.EtkinlikSayfasi" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Etkinlik Platformu - Etkinlik Sayfası</title>
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

        header .search-bar input {
            padding: 10px;
            border: none;
            border-radius: 8px;
            width: 200px;
        }

        header .user-menu {
            display: flex;
            align-items: center;
        }

        header .user-menu img {
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
            flex-direction: column;
            gap: 20px;
        }

        .container {
            background-color: #fff;
            border-radius: 15px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
            padding: 30px;
            transition: transform 0.3s;
            max-height: 500px; /* Yüksekliği sınırla */
            overflow-y: auto;
            overflow-x: hidden;
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

        .event-list {
            display: grid;
            grid-template-columns: repeat(5, 1fr); /* Her satırda beş kart */
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

        .event-card h3 a {
            color: #333;
            text-decoration: none;
            font-size: 18px;
            transition: color 0.3s;
        }

        .event-card h3 a:hover {
            color: #8A2BE2;
        }

        .event-card p {
            color: #555;
            margin-bottom: 10px;
        }

        .event-card button {
            padding: 8px 12px;
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            transition: background-color 0.3s;
            font-size: 14px;
            display: inline-block; /* Butonlar düzgün hizalanır */
        }
        .event-card .onay-label {
            display: block; /* Alt alta olması için block düzeni */
            color: red;
            margin-bottom: 10px;
}
        .event-card button:hover {
            background-color: #5a73d8;
        }

    </style>
</head>
<body>

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
        <div class="container">
            <h2>Tüm Etkinlikler</h2>
            <div class="event-list">
                <asp:Literal ID="EventListLiteral" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

</body>
</html>
