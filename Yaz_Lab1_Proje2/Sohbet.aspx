<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sohbet.aspx.cs" Inherits="Yaz_Lab1_Proje2.Sohbet" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Etkinlik Platformu - Sohbet</title>
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
            display: flex;
            justify-content: space-between; /* Konteynerleri yan yana yerleştir */
            padding: 20px;
            max-width: 1300px;
            margin: 0 auto;
        }

        /* Katıldığım Etkinlikler Alanı ve Sohbet Alanı */
        .left-container,
        .right-container {
            background-color: #fff;
            border-radius: 15px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
            padding: 30px;
            width: 1000px; /* Genişlik: %48, toplam genişlik %96 yapar, arada boşluk için */
            transition: transform 0.3s;
            max-height: 500px; /* İçeriğin yüksekliğini sınırlandırmak için bir yükseklik belirleyin */
            overflow-y: auto;
            overflow-x: hidden;
        }

        .left-container {
            background-color: #fff;
            border-radius: 15px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
            padding: 30px;
            width: 1000px;
            transition: transform 0.3s;
            overflow-y: auto; /* Kaydırma çubuğu eklemek için */
            max-height: 500px; /* İçeriğin yüksekliğini sınırlandırmak için bir yükseklik belirleyin */
            margin-right: 20px; /* Aradaki boşluk */
        }

            .left-container h2 {
                margin-bottom: 50px;
            }

        /* Etkinlik Listesi */
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

        /* Sohbet Paneli Stilleri */
        .chat-panel {
            display: flex;
            flex-direction: column;
            height: 400px;
            border: 1px solid #ddd;
            border-radius: 10px;
            overflow-y: auto;
            overflow-x: hidden;
            width: 100%;
        }

        .chat-messages {
            flex: 1;
            padding: 15px;
            overflow-y: auto;
            background-color: #f9f9f9;
        }

        .chat-message {
            margin-bottom: 15px;
            border-bottom: 1px solid #eee;
            padding-bottom: 5px;
        }

            .chat-message .username {
                font-weight: bold;
                color: #8A2BE2;
            }

            .chat-message .timestamp {
                font-size: 12px;
                color: #777;
                margin-left: 10px;
            }

        .chat-input {
            display: flex;
            border-top: 1px solid #ddd;
        }

            .chat-input textarea {
                flex: 1;
                padding: 10px;
                border: none;
                border-radius: 0;
                resize: none;
                outline: none;
            }

        .custom-button {
            padding: 10px 20px;
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-weight: bold;
            transition: background 0.3s;
        }

            .custom-button:hover {
                background: linear-gradient(135deg, #FFA500, #8A2BE2);
            }
    </style>
    <script>
        // JavaScript fonksiyonu, Detay Görüntüle butonuna basıldığında çağrılır
        function updateChat(eventDetails) {
            const chatMessages = document.querySelector('.chat-messages');
            chatMessages.innerHTML = `
                <div class="chat-message">
                    <span class="username">Etkinlik Detayı</span>
                    <p>${eventDetails}</p>
                </div>
            `;
        }
    </script>
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
    <form runat="server">

        <!-- Ana İçerik -->
        <div class="content">
            <!-- Katıldığım Etkinlikler Alanı -->
            <div class="left-container">
                <h2>Katıldığım Etkinlikler</h2>
                <div class="event-list">
                    <asp:PlaceHolder ID="eventListContainer" runat="server"></asp:PlaceHolder>
                </div>
            </div>

            <!-- Sohbet Alanı -->
            <div class="right-container">
                <h2>Etkinlik Sohbeti</h2>
                <!-- ASP.NET UpdatePanel kullanımı -->
                <asp:ScriptManager runat="server"></asp:ScriptManager>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="chat-panel">
                            <div class="chat-messages">
                                <asp:Literal ID="ChatMessagesLiteral" runat="server"></asp:Literal>
                            </div>
                            <div class="chat-input">
                                <textarea id="txtMessage" placeholder="Mesajınızı yazın..." runat="server"></textarea>
                                <asp:Button ID="btnGonder" runat="server" Text="Gönder" CssClass="custom-button" OnClick="btnGonder_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>

</body>
</html>
