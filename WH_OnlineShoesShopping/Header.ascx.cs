using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WH_OnlineShoesShopping.NewFolder1;

namespace WH_OnlineShoesShopping
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                if (Session["user"] != null)
                {
                    // User is logged in
                    string username = Session["user"].ToString();
                    // Perform actions for logged-in user
                    _LoggedIn.Text = username;
                    _LoggedIn.ForeColor = Color.Red;
                   
                    //_logOutBtn.Visible = true;
                    _loggedInOrNot.Text = "Log Out";
                    _myLink.HRef = "~/LogOut.aspx";
                    _myCart.HRef = "~/MyCart.aspx";
                    _cartIcon.Visible = true;
                    int itemCount = OnlineShpping.GetTotalItemCount(username);                   
                    _text.Text = itemCount.ToString();
                    

                }
                else
                {
                    // User is not logged in
                    // Perform actions for anonymous user
                    _LoggedIn.Text = string.Empty;
                    _loggedInOrNot.Text = "Login";
                   // _logOutBtn.Visible = false;


                }
            }
           
        }
   
      
    }
}