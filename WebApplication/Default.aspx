<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">

    <title>Default </title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Default.css" type="text/css" rel="stylesheet" />
    <script src="http://maps.googleapis.com/maps/api/js?sensor=false"> </script>
    <script src="Scripts/Polymer/webcomponentsjs/webcomponents.js"></script>
    <link rel="import" href="Scripts/Polymer/paper-button/paper-button.html" />
    <script src="Scripts/jquery-2.1.4.min.js"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" >

        var locations = [];
        var map, marker, bounds, infowindow;

        debugger;

        /**************************************Hata*************************************/
        //Giriş yapan kullanıcının Id sini cookie ile yakalarız...
        var cookiedata = document.cookie;
        var cookiesplit = cookiedata.split('=');
        var Id = cookiesplit[1];

        //Servis ile database ye bağlanırız...
        $.ajax({
            type: "POST",
            url: "WebService1.asmx/GetuserLocation",
            data: JSON.stringify({ userId: Id }),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (result) {

                //Servisten database deki konumadı,x ve y kordinatları alıp. locations listesine atarız...
                for (var i = 0; i < result.d.length; i++) {
                    liste = [];
                    liste.push(result.d[i].Name, result.d[i].X, result.d[i].Y);
                    locations.push(liste);
                }

                //Fonksiyona yollarız...
                addmarker();

            },
            error: function (result) {
                alert(result);
            }
        });
        /*******************************************************************************/

        function initialize() {
            //Haritanın özelliklerini belirleriz...
            var mapOptions = {
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);
            infowindow = new google.maps.InfoWindow();

            //Haritaya tıklayınca yeni marker ekleriz...
            google.maps.event.addListener(map, 'click', function (event) {
                placeMarker(event.latLng);
            });

            //Haritanın yapacağı zoom un sınırları yaptırır...
            bounds = new google.maps.LatLngBounds();
            map.fitBounds(bounds);
        }
        google.maps.event.addDomListener(window, 'load', initialize);

        $("document").ready(function () {

            //Giriş yapan kullanıcının Id sini cookie ile yakalarız...
            var cookiedata = document.cookie;
            var cookiesplit = cookiedata.split('=');
            var Id = cookiesplit[1];

            //Servis ile database ye bağlanırız...
            $.ajax({
                type: "POST",
                url: "WebService1.asmx/GetuserLocation",
                data: JSON.stringify({ userId: Id }),
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (result) {

                    //Servisten database deki konumadı,x ve y kordinatları alıp. locations listesine atarız...
                    for (var i = 0; i < result.d.length; i++) {
                        liste = [];
                        liste.push(result.d[i].Name, result.d[i].X, result.d[i].Y);
                        locations.push(liste);
                    }

                    //Fonksiyona yollarız...
                    addmarker();

                },
                error: function (result) {
                    alert(result);
                }
            });
        });

        function addmarker() {
            //Haritaya markerları ekletiriz...
            for (var i = 0; i < locations.length; i++) {
                var pos = new google.maps.LatLng(locations[i][1], locations[i][2]);
                bounds.extend(pos);
                marker = new google.maps.Marker({
                    position: pos,
                    map: map
                });

                //Markerların üstüne tıkladığımızda baloncuk içinde konum adını verir...
                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        infowindow.setContent(locations[i][0]);
                        infowindow.open(map, marker);
                        document.getElementById('<%=txtKonumadi.ClientID%>').value = locations[i][0];
                        document.getElementById('<%=txtKordinatx.ClientID%>').value = locations[i][1];
                        document.getElementById('<%=txtKordinaty.ClientID%>').value = locations[i][2];
                    }
                })(marker, i));
            }
        }

        //Haritada markera tıklayınca veriyi texboxlara yollarız...
        function placeMarker(location) {
            var marker = new google.maps.Marker({
                position: location,
                map: map,
            });
            x = location.lat()
            y = location.lng()
            document.getElementById('<%=txtKonumadi.ClientID%>').value = ""
            document.getElementById('<%=txtKordinatx.ClientID%>').value = x;
            document.getElementById('<%=txtKordinaty.ClientID%>').value = y;
        }

        google.maps.event.addDomListener(window, 'load', initialize);
    </script>


    <script>
        function BtnExit(event) {
            var buttonId = '<%=btnhiddenbutton.ClientID %>';
            document.getElementById(buttonId).click();
        }
    </script>

    <div class="container-fluid p0">
        <div class="col col-md-8" >
            <div class="harita row " id="googleMap" ></div>
        </div>

        <div style="visibility:hidden">
            <asp:Button id="btnhiddenbutton" runat="server" OnClick="btnExit_Click"/>
            <asp:Button id="btnhiddenbutton2" runat="server" OnClick="btnSil_Click"/>
            <asp:Button id="btnhiddenbutton3" runat="server" OnClick="btnEkle_Click"/>
        </div>

        <div class="col col-md-4 row sagkutu" >
            Konum Adı :
            <asp:TextBox Cssclass="konumaadi" ID="txtKonumadi" runat="server" Width="250px"></asp:TextBox><br />
            X Kordinatı :
            <asp:TextBox Cssclass="kordinatx" ID="txtKordinatx" runat="server" Width="250px"></asp:TextBox><br />
            Y Kordinatı :
            <asp:TextBox Cssclass="kordinaty" ID="txtKordinaty" runat="server" Width="250px"></asp:TextBox><br />
            <asp:TextBox ID="txtGizlemeId" runat="server" Visible="false"></asp:TextBox>
            <section>
                <paper-button style="float:right; margin-right:10.5px; margin-top:15px;" raised id="btn" onclick="BtnDelete()">Sil</paper-button>
                <paper-button style="float:right; margin-right:10.5px; margin-top:15px;" raised id="btn2" onclick="BtnSave()">Kaydet</paper-button>
                <br />
            </section>
            <asp:Label ID="labelmesaj" runat="server" /><br />
        </div>

        <script>
            // Gizlenmiş btnhiddenbutton2'yi referans alıyoruz. "btnSil_Click" tetikliyoruz...
            function BtnDelete(event) {
                var buttonIdDelete = '<%=btnhiddenbutton2.ClientID %>';
                document.getElementById(buttonIdDelete).click();
            }

            // Gizlenmiş btnhiddenbutton2'ü referans alıyoruz. "btnEkle_Click" tetikliyoruz...
            function BtnSave(event) {
                var buttonIdSave = '<%=btnhiddenbutton3.ClientID %>';
                document.getElementById(buttonIdSave).click();
            }
        </script>

        <div class="row m0">
            <div class="col col-md-8 p0" >
                <div class="altkutu" >
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateDeleteButton="True" EnableViewState="False" OnRowDeleting="DeleteRowButton_Click" AutoGenerateEditButton="True" OnRowEditing="EditRowButton_Click" CellPadding="4" ForeColor="#333333" GridLines="None" Height="100px" Width="700px" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
