<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyCart.aspx.cs" Inherits="WH_OnlineShoesShopping.MyCart" Title="My Cart" MasterPageFile="~/Site1.Master" %>
  <%--payment api - stripe--%>
<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">
<script src="https://js.stripe.com/v3/"></script>

    <script type="text/javascript">
        function Payment() {
            //get masterpage form id
            var masterFormId = document.getElementById("MasterForm").id;
            // create stripe object with publishable key
            var stripe = Stripe('pk_test_TYooMQauvdEDq54NiTphI7jx');
            // getelement using form from master page
            var form = document.getElementById(masterFormId);
            // add submit event on the master page form
            form.addEventListener('submit', function (e) {
                // prevent default submit
                e.preventDefault();
                // redirect to checkout session with stripe sessionID
                stripe.redirectToCheckout({
                    sessionId: "<%= sessionId %>"
                });
            })
 }
    </script> 
</script>

    <section id="myCart-Section">
        <div id="myCart-div">
            <asp:Label Text="My Shopping Cart" runat="server" CssClass="myCart-Label" />

        </div>
        <br />
        <br />
        <div id="myCart-Item">
            <asp:DataList runat="server" ID="_dlMyCart">
                <ItemTemplate>
                    <div>
                        <asp:Image ImageUrl='<%#  Eval("productImage") %>' runat="server" Width="200px"    />
                    </div>
                    <div>
                    </div>
                    <div>
                        <asp:Label Text='<%# Eval("productBrand") %>' runat="server" />
                        <asp:Label Text='<%# Eval("productName") %>' runat="server" />
                    </div>
                    <div>
                        <asp:Label Text='<%# Eval("productSize") %>' runat="server" />
                    </div>
                           <div>
                        <asp:Label Text='<%# Eval("productPrice") %>' runat="server" />
                    </div>
                     
                    <div>
                        <asp:DropDownList runat="server" ID="e_MyCart_ddl_quantity" AutoPostBack="true" OnSelectedIndexChanged="e_MyCart_ddl_quantity_SelectedIndexChanged">
                                     <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                                <asp:ListItem Text="4" Value="4" />
                                <asp:ListItem Text="5" Value="5" />
                                <asp:ListItem Text="6" Value="6" />
                                <asp:ListItem Text="7" Value="7" />
                                <asp:ListItem Text="8" Value="8" />
                                <asp:ListItem Text="9" Value="9" />
                        </asp:DropDownList>
                    </div>
              

                      <div>
                <asp:Button Text="Remove" runat="server" ID="e_Mycart_remove" CommandName="delete" CommandArgument='<%#Eval("productId") %>' OnClick="e_Mycart_remove_Click" CssClass="myCart-btn" />
            </div>
                       <br />
                </ItemTemplate>
            </asp:DataList>
          


        </div>
        <div>
              <asp:Label ID="e_MyCart_subTotal" Text="" runat="server" CssClass="e_MyCart_totalPrice" />
        </div>
        <br />
        <div id="btn-purchase">
          
             <button type="submit" runat="server" class="e_ProductDetail_addToCartImg" onclick="Payment()" > BuyNOW</button> 
        </div>

     

    </section>
       

</asp:Content>
