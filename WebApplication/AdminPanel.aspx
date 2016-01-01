<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="WebApplication.AdminPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">

    <title>Admin Paneli</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="AdminPanel.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.4.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid center-block p0">
        <div class="col-md-6 center-block">
            <p></p>
            <div class="row kutu">
                <h1>Kullanıcı Listesi</h1>

                <asp:Gridview id="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="300px" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate> <%#Eval("Id") %> </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtId" Text='<%#Eval("Id") %>' runat="server"/>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Adi">
                            <ItemTemplate> <%#Eval("Adi") %> </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAdi" Text='<%#Eval("Adi") %>' runat="server"/>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Soyadi">
                            <ItemTemplate> <%#Eval("Soyadi") %> </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtYkordinat" Text='<%#Eval("Soyadi") %>' runat="server"/>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipi">
                            <ItemTemplate> <%#Eval("Name") %> </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtYkordinat" Text='<%#Eval("Name") %>' runat="server"/>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Değiştir">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" Text="Tıkla" Width="50px" OnClick="MyButtonClick" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:Gridview>

            </div>
        </div>
    </div>

</asp:Content>










