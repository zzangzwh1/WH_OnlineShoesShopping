using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Diagnostics;
using WH_OnlineShoesShopping.NewFolder1;


namespace WH_OnlineShoesShopping
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("This is a debug message.");
            if (!IsPostBack)
            {
                
              _mWizard.SetActiveView(_vLogin);
                _productItemsDisplay.DataSource = OnlineShpping.GetDataTable("Select * from Product");
                _productItemsDisplay.DataBind();
                if (Session["user"] != null)
                {
                    // User is logged in
                    //string username = Session["user"].ToString();
                    // Perform actions for logged-in user
                   // _LoggedIn.Text = username;
                    //_LoggedIn.ForeColor = Color.Red;
                    _mWizard.Visible = false;
                   // _logOutBtn.Visible = true;
                   // _loggedInOrNot.Text = string.Empty;
                    
                    

                }
               /* else
                {
                    // User is not logged in
                    // Perform actions for anonymous user
                    _LoggedIn.Text = string.Empty;
                    _loggedInOrNot.Text = "Login";
                    _logOutBtn.Visible = false;


                }
               */


            }
          
        }

    

        protected void SignUp_Click1(object sender, EventArgs e)
        {
            _mWizard.SetActiveView(_vSignUp);

           
        }
        protected void Close()
        {
            _Name.Text = "";
            _ID.Text = "";
            _Password.Text = "";
            _SconfirmPassword.Text = "";
            _Semail.Text = "";

        }

        protected void _LoginBTN_Click(object sender, EventArgs e)
        {
           
            var login = OnlineShpping.LogIn(_username.Text, _userPassword.Text);
            
            _lbl_failLogin.Text = "";
            bool isLoggedIn = login.Item1;

            if (!isLoggedIn)
            {
                _username.Text = "";
                _userPassword.Text = "";
                _lbl_failLogin.Text = "Input  Invalid ID or Password";

                _lbl_failLogin.ForeColor = Color.Red;
            }
            else
            {
             
                Session["user"] = login.Item2;
              //  _LoggedIn.Text = login.Item2.ToString();
                Response.Redirect("Default.aspx");
              
            }
        }

        protected void RegisterBTN_Click(object sender, EventArgs e)
        {
            //char roleID, string name, string username, string password,  string email
            bool signUp = OnlineShpping.Registeration('U', _Name.Text, _ID.Text, _Password.Text, _Semail.Text);
            if (!signUp)
            {
                Response.Write("<Script>alert('Username already exist!!')</Script>");
                Close();
            }
            else
            {
                Response.Write("<Script>alert('Succesful to Sign up')</Script>");
                Close();
                _mWizard.SetActiveView(_vLogin);
            }
        }
        /// <summary>
        /// when image click it will pass the product ID and will retrieved from when whe product image clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void _productImageBTN_Click(object sender, ImageClickEventArgs e)
        {
            string imgBtn = $"Products.aspx?productId={((ImageButton)sender).CommandArgument.ToString()}";
            Debug.WriteLine(imgBtn);
            Response.Redirect(imgBtn, false);
        }
        /// <summary>
        /// when buy now buy now button click product id will be binded and open the products page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void _productBuy_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string productId = button.CommandArgument;

            // Assuming you have a page named "Product.aspx" where you display the product details
            string productPageUrl = $"Products.aspx?productId={productId}";
            Debug.WriteLine(productPageUrl);
            Response.Redirect(productPageUrl);
        }
    }
}