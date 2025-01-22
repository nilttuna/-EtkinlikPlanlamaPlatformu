<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikOner.aspx.cs" Inherits="Yaz_Lab1_Proje2.EtkinlikOner" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Etkinlik Platformu - Öneri</title>
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
            max-width: 1300px; /* Konteyner genişliği */
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
            grid-template-columns: repeat(3, 1fr); /* Her satırda üç kart */
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

        .event-card button {
            padding: 8px 12px;
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            transition: background-color 0.3s;
            font-size: 14px;
        }

        .event-card button:hover {
            background-color: #5a73d8;
        }

        /* Etkinlik Ekleme Butonu */
        .add-event-btn {
            position: fixed;
            bottom: 30px;
            right: 30px;
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
            width: 60px;
            height: 60px;
            border: none;
            border-radius: 50%;
            font-size: 30px;
            cursor: pointer;
            display: flex;
            align-items: center;
            justify-content: center;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
            transition: background-color 0.3s;
        }

        .add-event-btn:hover {
            background-color: #5a73d8;
        }

         .event-card-button {
            padding: 8px 12px;
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            transition: background-color 0.3s;
            font-size: 14px;
            display: inline-block;
            text-align: center;
            outline: none; /* Tarayıcı varsayılan buton stilini kaldırmak için */
        }

        .event-card-button:hover {
            background-color: #5a73d8;
        }
    </style>
</head>
<body>
     <form runat="server"> <!-- Sunucu taraflı form etiketi eklendi -->
    <!-- Başlık Bölümü -->
    <header>
        <div class="logo">Etkinlik Platformu</div>
        <div class="search-bar">
        </div>
        <div class="user-menu">
            <span>Hoş Geldiniz, <%=Session["KullaniciAdi"]%></span>

        </div>
    </header>

    <!-- Navigasyon Menüsü -->
    <nav>
        <a href="\AnaSayfa.aspx">Ana Sayfa</a>
        <a href="\EtkinlikSayfasi.aspx">Etkinlik Sayfası</a>
        <a href="\Sohbet.aspx">Sohbet</a>
        <a href="\Profil.aspx">Profil</a>
        <a id="profil" runat="server" href="#">Profiller</a>
        <a id="rapor" runat="server" href="#">Rapor</a>
        <a href="\Giris.aspx">Çıkış</a>
    </nav>

    <!-- Ana İçerik Alanı -->
    <div class="content">
        <!-- Önerilen Etkinlikler Konteyneri -->
                    <div class="container">
    <h2>Önerilen Etkinlikler</h2>
    <div class="event-list">
        <asp:PlaceHolder ID="eventListContainer2" runat="server"></asp:PlaceHolder>
    </div>
</div>
                    <div class="container">
    <h2>Mevcut Etkinlikler</h2>
    <div class="event-list">
        <asp:PlaceHolder ID="eventListContainer" runat="server"></asp:PlaceHolder>
    </div>
</div>
    </div>

          </form>
</body>
</html>
