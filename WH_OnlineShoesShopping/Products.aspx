<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WH_OnlineShoesShopping.Products" MasterPageFile="~/Site1.Master" Title="Product Detial" %>


<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">
    <!--Elevate zoom (image zoom) -->
  <script src="https://cdnjs.cloudflare.com/ajax/libs/elevatezoom/2.2.3/jquery.elevatezoom.js"></script>
    <script type="text/javascript">
        $(function () {
       
            $(".e_productImg").elevateZoom({
                cursor: 'pointer',
                imageCrossfade: true,
                loadingIcon: 'loading.gif'

            });
           
        });
    </script>

    <section class="trending-down" id="trending">
        <div classs="product-container">

            <div class="products-div">

                <asp:DataList ID="_productItemsDisplay" runat="server">
                    <ItemTemplate>
                        <div class="product-item">
                            <div class="product-image">
                                <asp:Image ImageUrl='<%# Eval("productImage")%>' runat="server" CssClass="e_productImg" Width="400px" />

                            </div>
                            <div class="namePrice">
                                <asp:Label Text='<%#Eval("productBrand")%>' runat="server" />
                                <asp:Label Text='<%#Eval("productName")%>' runat="server" />

                                <span class="spanProductPrice">
                                    <asp:Label Text='<%#string.Concat("$", Eval("productPrice", "{0:0.00}")) %>' runat="server" />
                                </span>
                            </div>
                    
                            <br />

                            <div class="product-buyBtns-container">
                                <asp:Button Text="Buy Now" runat="server" CssClass="btn-Buynow" />
                                <asp:Button Text="Add Cart" runat="server" CssClass="btn-Buynow" />
                            </div>

                        </div>

                    </ItemTemplate>
                </asp:DataList>

            </div>

        </div>


    </section>
 
   
</asp:Content>
