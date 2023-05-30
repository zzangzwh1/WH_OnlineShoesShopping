<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="WH_OnlineShoesShopping.PaymentSuccess"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Success</title>
        <link href="css/StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="e_Success">
            <div id="e_Success_img" runat="server" >
                <img src="https://static.tratta.io/tratta.io/ivr/iwr-payright--ben-5.webp" class="center" />
            </div>
          
            <div id="e_Sucess_btns">
                <asp:Button Text="Go Home" runat="server" ID="e_Success_goHome" OnClick="e_Success_goHome_Click" />
                 <asp:Button Text="Continue Shop" runat="server" ID="e_Success_shop" OnClick="e_Success_shop_Click"/>
                       
            </div>
        </div>
    </form>
</body>
</html> 