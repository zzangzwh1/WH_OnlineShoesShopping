using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stripe;
using Stripe.Checkout;
using WH_OnlineShoesShopping.NewFolder1;

namespace WH_OnlineShoesShopping
{
    public partial class MyCart : System.Web.UI.Page
    {

        // sessionId for Stripe checkout
        public string sessionId = "";

        /// <summary>
        /// Integrated Payment Stripe API
        /// </summary>
        protected void ExSessionCreate()
        {
            StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";
            string s = "";
            // recieve items in cart from DB
            Dictionary<string, Dictionary<string, object>> items = OnlineShpping.GetItemsInfoInCart(Session["user"].ToString());

            if (items.Count != 0)
            {
                List<SessionLineItemOptions> itemoptions = new List<SessionLineItemOptions>();
                foreach (KeyValuePair<string, Dictionary<string, object>> item in items)
                {
                    itemoptions.Add(
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = Convert.ToInt64(item.Value["Price"]) * 100,
                                Currency = "cad",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Key,
                                },

                            },

                            //  Name = item.Key,
                           
                            Quantity = Convert.ToInt64(item.Value["amount"]),



                        });

                    // for debug
                    //Debug.WriteLine(item.Key + " : " + " price : " + item.Value["Price"] + " amount : " + item.Value["amount"]);
                }

                var options = new SessionCreateOptions
                {
                    SuccessUrl = "https://localhost:44330/PaymentSuccess.aspx",
                    CancelUrl = "https://localhost:44330/MyCart.aspx/success?id={CHECKOUT_SESSION_ID}",
                    Mode = "payment",
                    LineItems = itemoptions,

                    PaymentMethodTypes = new List<string>()
                {
                    "card"
                }




                };
                var service = new SessionService();
                Session session = service.Create(options);
                sessionId = session.Id;
            }

        }




        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "";
            if (Session["user"] != null)
                ExSessionCreate();

            if (!IsPostBack)
            {

                if (Session["user"] == null)
                {
                    Response.Write("<Script>alert('You must Login If you wish to check Cart Page! '); window.location = 'Default.aspx#Logins' </Script>");
                }
                else
                {
                    Debug.WriteLine($"This is Session in mycart : {Session["user"]}");
                    string sQuery =
                   "select p.productName, p.productPrice,p.productBrand,p.productImage,p.productSize,p.productId,c.amount  "
                 + "from Cart c "
                 + "join Member m on m.memberId = c.memberId "
                 + "join product p on p.productId = c.productId "
                 + $"where m.username = '{Session["user"]}'";

                    // save returned datatable
                    DataTable dt = OnlineShpping.GetDataTable(sQuery);
                    // assign datatable to datasource of datalist
                    _dlMyCart.DataSource = dt;
                    // bind data
                    _dlMyCart.DataBind();

                    // item count
                    int totalItemCount = 0;
                    double totalItemPrice = 0;
                    string sss = "";
                    // loop items in datalist
                    for (int i = 0; i < _dlMyCart.Items.Count; i++)
                    {
                        // find dropdownlist control
                        DropDownList ddl = (DropDownList)_dlMyCart.Items[i].FindControl("e_MyCart_ddl_quantity");
                        // preset default value as amount value from db
                        ddl.SelectedValue = dt.Rows[i]["amount"].ToString();

                        // count quantity of total items in cart
                        totalItemCount += Convert.ToInt32(dt.Rows[i]["amount"]);
                        // count price of total items in cart
                        totalItemPrice += Convert.ToDouble(dt.Rows[i]["productPrice"]) * Convert.ToInt32(dt.Rows[i]["amount"]);
                    }


                    e_MyCart_subTotal.Text = "Subtotal(" + totalItemCount + "items): $" + totalItemPrice;
                }
            }
        }

        protected void e_MyCart_ddl_quantity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            // find same clienID for ddl and remove btn 
            // because productID is in remove button's commandargument 
            // client ID start with => Main_e_MyCart_dl_e_Mycart_remove_[n]
            // ex : first ddl = Main_e_MyCart_dl_e_MyCart_ddl_quantity_0
            // ex : first remove btn = Main_e_MyCart_dl_e_Mycart_remove_0

            string s = "Main__dlMyCart_e_Mycart_remove_" + ddl.ClientID.Last();
            string productID = "";

            // loop items in datalist
            for (int i = 0; i < _dlMyCart.Items.Count; i++)
            {
                // find remove button
                Button btn = (Button)_dlMyCart.Items[i].FindControl("e_Mycart_remove");
                // Debug.WriteLine($"Btton")
                Debug.WriteLine($"This is BTN ID  , {btn.ID}  Client ID : {btn.ClientID} ");

                // check clicked button and save productID from button's commandargument 
                if (btn.ClientID == s)
                    productID = btn.CommandArgument;
            }

            bool quantityupdate = OnlineShpping.UpdateCartItemQantity(Convert.ToInt32(productID), Convert.ToInt32(ddl.SelectedValue), Session["user"].ToString());

            if (quantityupdate)
                Response.Write("<Script>alert('quantity updated successfully'); window.location = 'MyCart.aspx'</Script>");
            else
                Response.Write("<Script>alert('fail to update quantity')</Script>");
        }

        protected void e_Mycart_remove_Click(object sender, EventArgs e)
        {
            //get productID from CommandArgument property of each buttons
            string ProductID = ((Button)sender).CommandArgument.ToString();

            bool itemdeletion = OnlineShpping.RemoveCartItem(Convert.ToInt32(ProductID), Session["user"].ToString());

            if (itemdeletion)
            {
                Response.Write("<Script>alert('item removed successfully')</Script>");
                Server.Transfer("MyCart.aspx");
            }
            else
            {
                Response.Write("<Script>alert('fail to remove item')</Script>");
            }
        }








    }
}