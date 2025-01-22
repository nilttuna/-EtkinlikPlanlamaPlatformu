<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SifremiUnuttum.aspx.cs" Inherits="Yaz_Lab1_Proje2.SifremiUnuttum" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Şifre Yenileme</title>
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
            <h2>Şifre Yenileme</h2>
        </div>
        
        <div class="login-section">
            <h3>Giriş</h3>
            <p>Lütfen hesabınıza kayıtlı e-posta adresinizi ve yeni şifreyi girin.</p>
            <form id="resetForm" runat="server" method="post">
                <input type="email" name="email" placeholder="E-posta" required>
                <input type="password" name="password" placeholder="Yeni Şifre" required>
                <asp:Button ID="SifreYenile" runat="server" Text="Şifre Yenile" CssClass="custom-button" OnClick="SifreYenile_Click" />
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </form>
        </div>
    </div>
</body>
</html>
