using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WH_OnlineShoesShopping
{
    public partial class PaymentSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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