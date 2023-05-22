<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WH_OnlineShoesShopping.WebForm1" MasterPageFile="~/Site1.Master" Title="Default Page" %>

<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">

    <section class="main-home" id="main-home">

            <div class="down-arrow">
                <a href="#trending" class="down"><i class='bx bx-down-arrow-alt'></i></a>
            </div>
        </section>
        <br />

        <section class="trending-down" id="trending">
            <div classs="product-container">
                <div class="center-text">
                    <h2>Our <span>Shoes</span></h2>
                </div>
                <br />
                <div class="products-div">
                    <asp:Repeater runat="server" ID="_productItemsDisplay">
                        <ItemTemplate>
                            <div class="product-item">
                                <div class="product-image">
                                
                                    <asp:ImageButton ImageUrl='<%# Eval("productImage")%>' runat="server" CommandArgument='<%# Eval("productId") %>' Width="400px" ID="_productImageBTN" OnClick="_productImageBTN_Click" />
                                </div>
                                <div class="namePrice">
                                    <asp:Label Text='<%#Eval("productName")%>' runat="server" />
                                    <span class="spanProductPrice">
                                        <asp:Label Text='<%#string.Concat("$", Eval("productPrice", "{0:0.00}")) %>' runat="server" />
                                    </span>
                                </div>
                                <div class="buy">
                                    <asp:Button Text="Buy Now" runat="server" CssClass="buyButton" ID="_productBuy" OnClick="_productBuy_Click" CommandArgument='<%# Eval("productId") %>'/>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>


            </div>


        </section>

        <asp:MultiView runat="server" ID="_mWizard">
            <asp:View runat="server" ID="_vLogin">
                <aside class="Logins" id="Logins">
                    <section class="form">
                        <p id="heading">Login</p>
                        <div class="field">
                            <svg class="input-icon" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                                <path d="M13.106 7.222c0-2.967-2.249-5.032-5.482-5.032-3.35 0-5.646 2.318-5.646 5.702 0 3.493 2.235 5.708 5.762 5.708.862 0 1.689-.123 2.304-.335v-.862c-.43.199-1.354.328-2.29.328-2.926 0-4.813-1.88-4.813-4.798 0-2.844 1.921-4.881 4.594-4.881 2.735 0 4.608 1.688 4.608 4.156 0 1.682-.554 2.769-1.416 2.769-.492 0-.772-.28-.772-.76V5.206H8.923v.834h-.11c-.266-.595-.881-.964-1.6-.964-1.4 0-2.378 1.162-2.378 2.823 0 1.737.957 2.906 2.379 2.906.8 0 1.415-.39 1.709-1.087h.11c.081.67.703 1.148 1.503 1.148 1.572 0 2.57-1.415 2.57-3.643zm-7.177.704c0-1.197.54-1.907 1.456-1.907.93 0 1.524.738 1.524 1.907S8.308 9.84 7.371 9.84c-.895 0-1.442-.725-1.442-1.914z"></path>
                            </svg>
                            <asp:TextBox runat="server" CssClass="input-field" placeholder="ID" ID="_username" />
                        </div>

                        <div class="field">
                            <svg class="input-icon" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                                <path d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z"></path>
                            </svg>
                            <asp:TextBox runat="server" CssClass="input-field" placeholder="Password" TextMode="Password" ID="_userPassword" />
                        </div>
                        <div>
                            <asp:Label Text="" runat="server" ID="_lbl_failLogin" />
                        </div>
                        <div class="btn">
                            <asp:Button Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Login&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" CssClass="button1" ID="_LoginBTN" OnClick="_LoginBTN_Click" />
                            <asp:Button Text="Sign Up" runat="server" OnClick="SignUp_Click1" CssClass="button2" />

                        </div>


                    </section>
                </aside>
            </asp:View>
            <asp:View runat="server" ID="_vSignUp">
                <aside class="Logins" id="Logins">
                    <section class="form">
                        <p id="heading">Sign Up</p>
                        <div class="field">

                            <asp:TextBox runat="server" CssClass="input-field" placeholder="Name" ID="_Name" />


                        </div>
                        <div>
                            <asp:Label Text="" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                runat="server"
                                ErrorMessage="Letters Only  "
                                ControlToValidate="_Name"
                                ValidationExpression="[A-Za-z]+"
                                ForeColor="Red" Font-Size="12px">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="Empty" Display="Dynamic"
                                ControlToValidate="_Name" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                        </div>
                        <div class="field">

                            <asp:TextBox runat="server" CssClass="input-field" placeholder="ID" ID="_ID" />

                        </div>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="Empty" Display="Dynamic"
                                ControlToValidate="_ID" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                runat="server"
                                ErrorMessage="Letters&Numbers "
                                ControlToValidate="_ID"
                                ValidationExpression="[A-Za-z\d]{5,18}"
                                ForeColor="Red" Font-Size="12px">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="field"></div>
                        <div class="field">

                            <asp:TextBox runat="server" CssClass="input-field" placeholder="Password" TextMode="Password" ID="_Password" />

                        </div>
                        <div>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                runat="server"
                                ErrorMessage="1upper,lower,number,special character and min 8length"
                                ControlToValidate="_Password"
                                ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"
                                ForeColor="Red" Font-Size="12px">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="Empty" Display="Dynamic"
                                ControlToValidate="_Password" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>
                        </div>
                        <div class="field">

                            <asp:TextBox runat="server" CssClass="input-field" placeholder="Confirm Password" TextMode="Password" ID="_SconfirmPassword" />
                        </div>
                        <div>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Your password doesnt Match" ForeColor="Red" Display="Dynamic" ControlToCompare="_Password" ControlToValidate="_SconfirmPassword" Font-Size="12px"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Comfirm Password" Display="Dynamic" ControlToValidate="_SconfirmPassword" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>
                        </div>
                        <div class="field">

                            <asp:TextBox runat="server" CssClass="input-field" placeholder="Email" ID="_Semail" />
                        </div>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator"
                                Text="You Must Input Your Mail Address" Display="Dynamic"
                                ControlToValidate="_Semail" ForeColor="Red" Font-Size="12px"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                runat="server"
                                ErrorMessage="Invalid Email Address"
                                ControlToValidate="_Semail"
                                ValidationExpression="^[\w\.]+@([\w-]+\.)+[\w-]{2,4}$"
                                ForeColor="Red" Font-Size="12px">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="field">
                        </div>
                        <div class="btn">

                            <asp:Button Text="Register" runat="server" CssClass="button2" ID="RegisterBTN" OnClick="RegisterBTN_Click" />

                        </div>

                    </section>
            </asp:View>

        </asp:MultiView>
 </asp:Content>




