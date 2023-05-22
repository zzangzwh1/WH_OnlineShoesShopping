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
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
               // Debug.WriteLine($"Reuqset Query String : {Request.QueryString["productId"].ToString()}");
                if (Request.QueryString["productId"] != null)
                {
                    Debug.WriteLine(Request.QueryString["productId"].ToString());
                    string selectedProductId = Request.QueryString["productId"].ToString();
                    _productItemsDisplay.DataSource = OnlineShpping.GetDataTable($"select * from product where productId = {selectedProductId}");

                }
                _productItemsDisplay.DataBind();
            }
        }
    }
}