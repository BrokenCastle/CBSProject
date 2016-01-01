<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Profil.aspx.cs" Inherits="WebApplication.Profil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <title>Profil</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Profil.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.4.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid center-block p0">
        <div class="col-md-6 center-block">
            <p></p>
            <div class="row kutu">
                <b>İsim :</b>    
                <asp:TextBox CssClass="text1" ID="txtName" runat="server" /><br />
                <b>Soyadı :</b>    
                <asp:TextBox CssClass="text2" ID="txtSurname" runat="server" /><br />
                <b>Eski Şifre :</b>
                <asp:TextBox CssClass="text3" ID="txtPassword" TextMode="password" runat="server" /><br />
                <b>Yeni Şifre :</b>
                <asp:TextBox CssClass="text4" ID="txtPasswordNew" TextMode="password" runat="server" /><br />
                <b>E-posta :</b>
                <asp:TextBox CssClass="text5" ID="txtMail" runat="server" /><br />
                <asp:Button CssClass="button" ID="ButonSave" runat="server" Text="Güncelle" OnClick="ButonSaveClick" /><br />
                <asp:Label CssClass="message" ID="LabelMessage" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>









