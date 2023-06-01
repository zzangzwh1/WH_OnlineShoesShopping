using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WH_OnlineShoesShopping.NewFolder1;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace WH_OnlineShoesShopping
{
    public partial class PaymentSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";
                // create session service
                var service = new SessionService();
                var apiResult = service.Get(Request.QueryString["id"]);
                decimal totalPrice = (decimal)apiResult.AmountTotal / 100;
                OnlineShpping.InsertOrders(Session["user"].ToString(), totalPrice);

          

            }
        }

        protected void e_Success_goHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void e_Success_shop_Click(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx#trending");
           
        }
    }
}