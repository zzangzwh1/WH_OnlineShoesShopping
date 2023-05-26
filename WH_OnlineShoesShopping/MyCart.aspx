<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyCart.aspx.cs" Inherits="WH_OnlineShoesShopping.MyCart" Title="My Cart" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">
    <section id="myCart-Section">
        <div id="myCart-div">
            <asp:Label Text="My Shopping Cart" runat="server" CssClass="myCart-Label" />

        </div>
        <div id="myCart-Item">
            <asp:DataList runat="server" ID="_dlMyCart">
                <ItemTemplate>
                    <div>
                        <asp:Image ImageUrl='<%#  Eval("productImage") %>' runat="server"    />
                    </div>
                    <div>
                        <asp:Label Text='<%# Eval("productBrand") %>' runat="server" />
                    </div>
                    <div>
                        <asp:Label Text='<%# Eval("productName") %>' runat="server" />
                    </div>
                    <div>
                        <asp:Label Text='<%# Eval("productSize") %>' runat="server" />
                    </div>
                           <div>
                        <asp:Label Text='<%# Eval("productPrice") %>' runat="server" />
                    </div>
                     

                 
                </ItemTemplate>
            </asp:DataList>


        </div>
        <div>
              <asp:Label ID="e_MyCart_subTotal" Text="" runat="server" CssClass="e_MyCart_totalPrice" />
        </div>

    </section>

</asp:Content>
