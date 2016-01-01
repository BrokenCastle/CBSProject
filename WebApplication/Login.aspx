<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Login.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="genel" /> 
    <div class="baslik" > Kullanıcı Girişi</div>
    <div>       
        <b>İsim :</b>    
        <asp:TextBox CssClass="text1" ID="txtisim" runat="server" /><br />
        <b>Şifre :</b>
        <asp:TextBox CssClass="text2" ID="txtsifre" TextMode="password" runat="server" /><br />
        <asp:Button CssClass="button" ID="buton" runat="server" Text="Giriş" OnClick="buton_Click" /><br />
        <asp:LinkButton CssClass="linkbutton" ID="LinkButton1" runat="server" href="SignUp.aspx" > Kayıt Ol </asp:LinkButton><br />
        <asp:Label CssClass="message" ID="labelmesaj" runat="server" />
    </div>
    </form>
</body>
</html>