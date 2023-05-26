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

       /* protected void StripeCheckoutSession()
        {
            // recieve items in cart from DB
            Dictionary<string, Dictionary<string, object>> items = OnlineShpping.GetItemsInfoInCart(Session["user"].ToString());

            // list of SessionLineItemOptions(for stripe session)
            List<SessionLineItemOptions> itemoptions = new List<SessionLineItemOptions>();

            // add product infos in cart to sessionlineitemoptions 
            foreach (KeyValuePair<string, Dictionary<string, object>> item in items)
            {
                itemoptions.Add(
                    new SessionLineItemOptions
                    {
                        Name = item.Key,
                        Amount = Convert.ToInt64(item.Value["Price"]) * 100,
                        Currency = "cad",
                        Quantity = Convert.ToInt64(item.Value["amount"]),
                    });

                // for debug
                //Debug.WriteLine(item.Key + " : " + " price : " + item.Value["Price"] + " amount : " + item.Value["amount"]);
            }

            // if any items in cart create stripe session options
            if (items.Count > 0)
            {
                // create the checkout session with stripe and set 'sessionId' with secret key
                StripeConfiguration.ApiKey = "sk_test_51KfUlDEE8YeCCXUqpUBuBC3HuoOtSbAC8heEV8D7Uun0AxVHnYMhq5xinvnNZJd0z1cnHfzgXoss7l5M3DITurqk00uxk1vUdK";

                var options = new SessionCreateOptions
                {
                    CustomerEmail = EcommerceRegister.GetUsersEmail(Session["user"].ToString()),
                    // go to PaymentSuccess page and pass value of CHECKOUT_SESSION_ID when payment success
                    // id - will be used from paymentsuccess.aspx.cs 
                    SuccessUrl = "http://laptopworld.azurewebsites.net/PaymentSuccess.aspx?id={CHECKOUT_SESSION_ID}",
                    CancelUrl = "http://laptopworld.azurewebsites.net/MyCart.aspx",

                    // test at local server
                    //SuccessUrl = "http://localhost:52827/PaymentSuccess.aspx?id={CHECKOUT_SESSION_ID}",
                    //CancelUrl = "http://localhost:52827/MyCart.aspx",

                    PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                    LineItems = itemoptions,
                    ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                    {
                        AllowedCountries = new List<string>
                    {
                        "CA",
                    },
                    },
                    ShippingOptions = new List<SessionShippingOptionOptions>
                {
                    // free
                    new SessionShippingOptionOptions
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 0,
                                Currency = "cad",
                            },
                            DisplayName = "Free Shipping",
                            // Delivers between 5-7 business days
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 5,
                                },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 7,
                                },
                            },
                        }
                    },
                    new SessionShippingOptionOptions
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions
                            {
                                Amount = 1500,
                                Currency = "cad",
                            },
                            DisplayName = "UPS One day delivery",
                            // Delivers in exactly 1 business day
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions
                                {
                                    Unit = "business_day",
                                    Value = 1,
                                },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions
                                {
                                    Unit = "business_day",
                                    Value = 1,
                                },
                            },
                        }
                    }
                },
                };

                // create stripe session service object
                var service = new SessionService();
                // create stripe session by using session service and pass session options
                Session session = service.Create(options);
                // initialize sessionID
                // sessionID will be passed to function 'Payment()' in MyCart.aspx
                sessionId = session.Id;
            }
        }
       */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = "";
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

        /* protected void e_Mycart_remove_Click(object sender, EventArgs e)
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
        */
    }
}