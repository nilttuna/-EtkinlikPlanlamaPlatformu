<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YeniEtkinlik.aspx.cs" Inherits="Yaz_Lab1_Proje2.YeniEtkinlik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Yeni Etkinlik Ekle</title>
    <style>
        .content {
            flex: 1;
            padding: 20px;
            max-width: 800px;
            margin: 0 auto;
            display: flex;
            justify-content: center;
        }

        .container {
            flex: 1;
            background-color: #fff;
            border-radius: 15px;
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
            padding: 30px;
            width: 500px;
            transition: transform 0.3s;
            border: 2px solid #8A2BE2;
            overflow-y: auto; /* Yalnızca dikey kaydırma çubuğu */
            max-height: 600px; /* Maksimum yükseklik belirleyin */
        }

        .container:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
        }

        .container h2 {
            text-align: center;
            color: #444;
            font-size: 22px;
            border-bottom: 2px solid #8A2BE2;
            padding-bottom: 10px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #000; /* Siyah renk */
        }

        input[type="text"], input[type="date"], input[type="time"], input[type="number"], select {
            width: 100%;
            padding: 10px;
            box-sizing: border-box;
            border-radius: 5px;
            border: 1px solid #ccc;
            transition: border-color 0.3s, box-shadow 0.3s;
            font-size: 14px;
            appearance: none; /* Tarayıcı varsayılan stilini kaldır */
        }

        input[type="text"]:focus, input[type="date"]:focus, input[type="time"]:focus, input[type="number"]:focus, select:focus {
            border-color: #8A2BE2;
            box-shadow: 0 0 5px rgba(138, 43, 226, 0.5); /* Mor gölge */
            outline: none;
        }

        .submit-btn {
            background: linear-gradient(135deg, #8A2BE2, #FFA500);
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
            font-size: 16px;
            font-weight: bold;
            transition: background 0.3s;
        }

        .submit-btn:hover {
            background: linear-gradient(135deg, #6A1BB1, #FF8C00); /* Hover rengi */
        }
    </style>
</head>
<body>
<asp:Label ID="lblMessage" runat="server" CssClass="status-message" Visible="false"></asp:Label>

    <form id="form1" runat="server">
        <div class="content">
            <div class="container">
                <h2>Yeni Etkinlik Ekle</h2>
                <div class="form-group">
                    <label for="txtEtkinlikAdi">Etkinlik Adı:</label>
                    <asp:TextBox ID="txtEtkinlikAdi" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtAciklama">Açıklama:</label>
                    <asp:TextBox ID="txtAciklama" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEtkinlikTarihi">Etkinlik Tarihi:</label>
                    <asp:TextBox ID="txtEtkinlikTarihi" runat="server" TextMode="Date"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEtkinlikSaati">Etkinlik Saati:</label>
                    <asp:TextBox ID="txtEtkinlikSaati" runat="server" TextMode="Time"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEtkinlikSuresi">Etkinlik Süresi (dakika):</label>
                    <asp:TextBox ID="txtEtkinlikSuresi" runat="server" TextMode="Number"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEtkinlikKonum">Etkinlik Konumu:</label>
                    <asp:TextBox ID="txtEtkinlikKonum" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="ddlEtkinlikKategori">Etkinlik Kategorisi:</label>
                    <asp:DropDownList ID="ddlEtkinlikKategori" runat="server" CssClass="custom-dropdown"></asp:DropDownList>
                </div>
                <asp:Button ID="btnEkle" runat="server" Text="Ekle" CssClass="submit-btn" OnClick="btnEkle_Click" />
                <asp:Button ID="btnGuncelle" runat="server" Text="Guncelle" CssClass="submit-btn" OnClick="btnGuncelle_Click" Visible="false"  />

            </div>
        </div>
    </form>
</body>
</html>
