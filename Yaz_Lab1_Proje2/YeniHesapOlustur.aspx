<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YeniHesapOlustur.aspx.cs" Inherits="Yaz_Lab1_Proje2.YeniHesapOlustur" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Yeni Hesap Oluştur</title>
    <link href="style/yenikayit.css" rel="stylesheet" />
    <style>
        .custom-button {
    grid-column: 1 / -1; /* Butonu iki sütun boyunca yay */
    width: 100%;
    padding: 12px;
    background-color: #9013fe;
    color: white;
    border: none;
    border-radius: 8px;
    font-size: 18px;
    font-weight: bold;
    cursor: pointer;
    transition: background 0.3s, transform 0.1s, box-shadow 0.3s;
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
    <script>
        function previewProfilePicture(event) {
            const reader = new FileReader();
            reader.onload = function () {
                const output = document.getElementById('profilePicturePreview');
                output.src = reader.result;
            };
            reader.readAsDataURL(event.target.files[0]);
        }
    </script>
</head>
<body>
    <div class="register-wrapper">
        <div class="register-left">
            <h2>Yeni Hesap Oluştur</h2>
                
        </div>
        
        <div class="register-right">
                        <form id="registerForm" runat="server" method="post" enctype="multipart/form-data">

            <div class="profile-picture-container">
    <img id="profilePicturePreview" src="images/kullanıcı.png" alt="Profil Fotoğrafı">
    <asp:FileUpload ID="profilePicture" runat="server" accept="image/*" onchange="previewProfilePicture(event)" />
    <label for="profilePicture">Profil Fotoğrafı Seç</label>
</div>
                <input type="text" name="username" placeholder="Kullanıcı Adı" required>
                <input type="password" name="password" placeholder="Şifre" required>
                <input type="email" name="email" placeholder="E-posta" required>
                <input type="text" name="first_name" placeholder="Ad" required>
                <input type="text" name="last_name" placeholder="Soyad" required>
                <input type="text" name="location" placeholder="Konum (Şehir)">

                <asp:DropDownList ID="ddlInterests" runat="server" CssClass="custom-input">
                <asp:ListItem Text="İlgi Alanları Seç" Value="" Enabled="false" Selected="true"></asp:ListItem>
                </asp:DropDownList>

                <input type="date" name="birthdate" placeholder="Doğum Tarihi" required>
                <select name="gender" required>
                    <option value="" disabled selected>Cinsiyet</option>
                    <option value="Erkek">Erkek</option>
                    <option value="Kadın">Kadın</option>
                </select>
                <input type="tel" name="phone" placeholder="Telefon Numarası">
                <asp:Button ID="HesapOlustur" runat="server" Text="Hesap Oluştur" CssClass="custom-button" OnClick="HesapOlustur_Click1" />
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            </form>

            <div class="links">
                <a href="/Giris.aspx">Zaten hesabınız var mı? Giriş Yapın</a>
            </div>
        </div>
    </div>
</body>
</html>
