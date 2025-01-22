<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Giris.aspx.cs" Inherits="Yaz_Lab1_Proje2.Giris" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Giriş Ekranı</title>
    <link href="style/giris.css" rel="stylesheet" />
    <style>
        .custom-button {
    width: 100%;
    padding: 12px;
    background-color: #9013fe;
    color: white;
    border: none;
    border-radius: 8px;
    font-size: 18px;
    font-weight: bold;
    cursor: pointer;
    transition: background 0.3s;
    box-shadow: 0 5px 15px rgba(144, 19, 254, 0.3);
}

.custom-button:hover {
     background-color: #7d10db;
    box-shadow: 0 7px 20px rgba(125, 16, 219, 0.4);
}

.custom-button:active {
    transform: scale(0.98);
}

    </style>
</head>
<body>
    <div class="container">
        <div class="welcome-section">
            <h1>Hoşgeldiniz!</h1>
        </div>
        <div class="login-section">
            <h2>Giriş</h2>
            <p>Hoşgeldiniz! Lütfen Hesabınıza Giriş Yapınız.</p>
            <form id="Giris" runat="server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TextBox ID="txtUsername" runat="server" Placeholder="Kullanıcı Adı" CssClass="input-class" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Şifre" CssClass="input-class" />
            <asp:Button ID="GirisYap" runat="server" Text="Giriş Yap" OnClick="GirisYap_Click" CssClass="custom-button" />
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <div class="options">
                <a href="/YeniHesapOlustur.aspx">Kayıt Ol</a>
                <a href="/SifremiUnuttum.aspx">Şifremi Unuttum</a>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</form>

        </div>
    </div>
</body>
</html>