<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WH_OnlineShoesShopping.Products" MasterPageFile="~/Site1.Master" Title="Product Detial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                <asp:Button Text="Buy Now" runat="server" CssClass="btn-Buynow" OnClick="Unnamed_Click"/>
                                <asp:Button Text="Add Cart" runat="server" CssClass="btn-Buynow" ID="_AddCart" OnClick="_AddCart_Click" />
                            </div>

                        </div>

                    </ItemTemplate>
                </asp:DataList>

            </div>

        </div>


    </section>

    <section>
        <div id="section-Reviews">
            <h2>Reviews</h2>
        </div>

        <div class="star_div">
            <div>
                <asp:Label Text="text" runat="server" ID="_ReviewWriteOwner" Font-Size="30px" />

            </div>
            <div id="e_ProductDetail_board_result">
                <div id="e_ProductDetail_board_result_star">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <cc1:Rating ID="Rating1" runat="server"
                                StarCssClass="starRating"
                                FilledStarCssClass="Filledstars"
                                WaitingStarCssClass="Watingstars"
                                EmptyStarCssClass="Emptystars">
                            </cc1:Rating>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div id="e_ProductDetail_board_result_name">
                    <asp:Label ID="review_username" runat="server" Text="" Font-Size="30px" CssClass="review_username"></asp:Label>

                </div>
                <div id="e_ProductDetail_board_result_writeContent">
                  
                    <asp:TextBox ID="review_TB" runat="server" CssClass="TB_rating" placeholder="wirte review here" TextMode="MultiLine"></asp:TextBox>
                   
                </div>


                <div id="e_ProductDetail_board_result_submitBtn">

                    <asp:Button ID="review_BT" runat="server" Text="Submit" CssClass="review_deleteContentBTN" OnClick="review_BT_Click" />
                    <br />
                </div>
                <div id="e_ProductDetail_board_result_resultLbl">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>

                </div>
            </div>

        </div>




        <div class="reiview-result">
            <asp:DataList runat="server" ID="_dl_Review">

                <ItemTemplate>
                    <div>
                        <asp:Label Text='<%#(Eval("name"))%>' runat="server" ID="_SessionUserName" />

                    </div>
                    <div id="e_ProductDetail_board_result_star">
                        <%--need ScriptManager to use UpdatePanel--%>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc1:Rating ID="Rating2" runat="server"
                                    StarCssClass="starRating"
                                    FilledStarCssClass="Filledstars"
                                    WaitingStarCssClass="Watingstars"
                                    EmptyStarCssClass="Emptystars"
                                    CurrentRating='<%#(Eval("grade"))%>'>
                                </cc1:Rating>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>

                    <div id="review_main_content">

                        <asp:Label Text='<%#Eval("content") %>' runat="server" CssClass="review_content"></asp:Label>
                    </div>
                    <div id="review_main_date">

                        <asp:Label Text='<%#Eval("boardDate") %>' runat="server" CssClass="review_date"></asp:Label>
                    </div>
                    <br />
                    <div id="review_main_btn">

                        <%--pass multiple commandargument--%>
                        <asp:Button Text="Delete" runat="server" Visible="false" ID="_deleteBtnForAdmin" CssClass="review_deleteContentBTN"
                            CommandArgument='<%#Eval("username")+","+ Eval("boardNo")%>' OnClick="_deleteBtnForAdmin_Click" />
                    </div>
                    <br />
                    <br />

                </ItemTemplate>

            </asp:DataList>
        </div>




    </section>




</asp:Content>
