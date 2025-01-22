<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtkinlikDetay.aspx.cs" Inherits="Yaz_Lab1_Proje2.EtkinlikDetay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Etkinlik Detayları</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #eae6f8; /* Soluk lavanta gri */
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .container {
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            padding: 40px;
            border-radius: 15px;
            max-width: 1200px;
            max-height: 650px;
            color: white;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
            overflow-y: auto;
        }
        .container h2 {
            margin-top: 0;
            margin-bottom: 15px;
        }
        .container p {
            margin: 10px 0;
           margin: 10px 0; /* Paragraflar arasındaki mesafe */

        }
        .button-container {
            margin-top: 30px;
        }
        .button {
            background-color: #ffffff;
            color: #8A2BE2;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            font-weight: bold;
            text-transform: uppercase;
        }
        .button:hover {
            background-color: #f1f1f1;
        }
        .button:disabled {
            background-color: #d3d3d3;
            cursor: not-allowed;
        }
        .status {
            font-weight: bold;
        }

    </style>
</head>
<body onload="initMap()">
    <form id="form1" runat="server">
        <div class="container">
            <h2><asp:Label ID="lblEtkinlikAdi" runat="server" Text="Etkinlik Adı"></asp:Label></h2>
            <p><strong>Tarih:</strong> <asp:Label ID="lblEtkinlikTarihi" runat="server" Text="Tarih"></asp:Label></p>
            <p><strong>Saat:</strong> <asp:Label ID="lblEtkinlikSaati" runat="server" Text="Saat"></asp:Label></p>
            <p><strong>Sure:</strong> <asp:Label ID="lblEtkinlikSuresi" runat="server" Text="Süre"></asp:Label></p>
            <p><strong>Açıklama:</strong> <asp:Label ID="lblEtkinlikAciklama" runat="server" Text="Açıklama"></asp:Label></p>
            <p><strong>Konum:</strong> <asp:Label ID="lblEtkinlikKonumu" runat="server" Text="Konum"></asp:Label></p>

            <p><strong>Kategori:</strong> <asp:Label ID="lblEtkinlikKategorisi" runat="server" Text="Kategori"></asp:Label></p>

            <div class="button-container">
                <asp:Button ID="btnKatil" runat="server" CssClass="button" Text="Etkinliğe Katıl" OnClick="Katil_Click" Visible="false" />
                <asp:Button ID="btnGuncelle" runat="server" CssClass="button" Text="Güncelle" OnClick="Guncelle_Click" Visible="false" />
                <asp:Button ID="btnSil" runat="server" CssClass="button" Text="Sil" OnClick="Sil_Click" Visible="false" />
                <asp:Button ID="btnOnay" runat="server" CssClass="button" Text="Onayla" OnClick="Onayla_Click" Visible="false" />
                <asp:Label ID="lblStatus" runat="server" CssClass="status" Text="Katıldınız" Visible="false"></asp:Label>
                <asp:Label ID="lblMessage" runat="server" CssClass="status-message" Visible="false"></asp:Label>

            </div>
        </div>
    </form>
</body>
</html>
