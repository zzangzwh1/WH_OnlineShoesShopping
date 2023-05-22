<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="WH_OnlineShoesShopping.Header" %>
<header>

    <a href="Default.aspx" class="logo">MC</a>
    <ul class="navmenu">
        <li><a href="#main-home">
            <asp:Label Text="home" runat="server" />
        </a></li>
        <li><a href="#trending">
            <asp:Label Text="Shop" runat="server" /></a></li>
        <li><a id="_myLink" runat="server" href="#Logins">
            <asp:Label Text="login" runat="server" ID="_loggedInOrNot" CssClass="clickable-label" /></a></li>
        <li>
    </ul>

    <div class="nav-icon">
        <a href="#"><i class='bx bx-search-alt-2'></i></a>
        <a href="#"><i class='bx bx-user'>
            <asp:Label Text="" runat="server" ID="_LoggedIn" Font-Size="10px" /></i></a>
        <a href="#"><i class='bx bx-cart'></i></a>

        <div class="bx bx-menu" id="menu-icon"></div>
    </div>

</header>
