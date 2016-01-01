<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="WebApplication.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kayıt Ol</title>
    <link href="SignUp.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="genel" /> 
    <div class="baslik" > Kayıt Ol</div>
    <div>
        <b>İsim :</b>    
        <asp:TextBox CssClass="text1" ID="txtname" runat="server" /><br />
        <b>Soyadı :</b>    
        <asp:TextBox CssClass="text2" ID="txtsurname" runat="server" /><br />
        <b>Şifre :</b>
        <asp:TextBox CssClass="text3" ID="txtpasword" TextMode="password" runat="server" /><br />
        <b>E-posta :</b>
        <asp:TextBox CssClass="text4" ID="txtmail" runat="server" /><br />
        <asp:Button CssClass="button" ID="ButonSave" runat="server" Text="Kayıt Ol" OnClick="ButonSaveClick"  /><br />
        <asp:Label CssClass="message" ID="LabelMessage" runat="server" />
    </div>
    </form>
</body>
</html>





