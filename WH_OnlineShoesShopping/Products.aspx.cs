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
                    _productItemsDisplay.DataSource = OnlineShpping.GetDataTable($"select * from product where productId = '{selectedProductId}'");
                    _dl_Review.DataSource = OnlineShpping.GetDataTable
                   ("select * from product p join board b on p.productId = b.productId " +
                   $"join Member m on b.memberId = m.memberId where p.productId = '{selectedProductId}' order by b.boardDate desc ");
                }
                _productItemsDisplay.DataBind();
                _dl_Review.DataBind();
                if (Session["user"] != null)
                {
                    _ReviewWriteOwner.Text = Session["user"].ToString();
                    //  string username = OnlineShpping.GetFullName()
                    // _SessionUserName.Text = 
                     isValidBtn();
                    review_BT.Enabled = true;
                 

                }
                else
                { 
                    UpdatePanel1.Visible = false;
                    _ReviewWriteOwner.Text = "";
                    review_username.Visible = false;
                    review_TB.Visible = false;
                    review_BT.Visible = false;
                }
                // set review scores as readonly
                for (int i = 0; i < _dl_Review.Items.Count; i++)
                {
                    AjaxControlToolkit.Rating rate = (AjaxControlToolkit.Rating)_dl_Review.Items[i].FindControl("Rating2");

                    if (rate != null)
                        rate.ReadOnly = true;
                }


            }
        }
        protected void isValidBtn()
        {
            for (int i = 0; i < _dl_Review.Items.Count; i++)
            {
                Button btn = (Button)_dl_Review.Items[i].FindControl("_deleteBtnForAdmin");

                // split commandargument (passed two command argument with ,)
                string[] args = btn.CommandArgument.ToString().Split(new char[] { ',' });

                // enablte delete button for only admin or user themself
                if (Session["user"].ToString() == "Admin" || args[0] == Session["user"].ToString())
                    btn.Visible = true;
                else
                    btn.Visible = false;
            }
        }

        protected void _deleteBtnForAdmin_Click(object sender, EventArgs e)
        {
           
            Button btn = (Button)sender;
            string selectedProduct = Request.QueryString["productId"];

            // Split command argument (passed two command arguments with comma)
            string[] args = btn.CommandArgument.ToString().Split(new char[] { ',' });

            // Extract the boardNo from the command argument
            int boardNo = Convert.ToInt32(args[1]);

            // BoardDeleteBtn - return boolean for delete content is succeeded from board table
            bool deleteContent = OnlineShpping.BoardDeleteBtn(boardNo);

            if (deleteContent)
            {
                Response.Write($"<script>alert('Review Content is successfully deleted'); " +
                               $"window.location='Products.aspx?productId={selectedProduct}';</script>");
            }
            else
            {
                Response.Write("<script>alert('Failed to delete review');</script>");
            }
        }

        protected void review_BT_Click(object sender, EventArgs e)
        {
            if (review_TB.Text == "")
            {
                Response.Write("<Script>alert('You did not input any word in review!')</Script>");
                return;
            }

            string review = $"Score : {Rating1.CurrentRating}\n"
                          + $"Review : {review_TB.Text}";

            Label2.Text = review;

            string username = Session["user"].ToString();

            // check if passed productID is exist
            if (Request.QueryString["productId"] != null)
            {
                string selectedProduct = Request.QueryString["productId"].ToString();

                // SubmitReview - return boolean for submitted review is succeeded
                // memberId, content, grade, productId, boardDate
                bool submitReview = OnlineShpping.SubmitReview(username, review_TB.Text, Rating1.CurrentRating, Convert.ToInt32(selectedProduct));

                if (submitReview)
                {
                    Response.Write("<Script>alert('review submitted')</Script>");
                    review_TB.Text = "";
                }
                else
                    Response.Write("<Script>alert('fail to submit')</Script>");

                Response.Redirect("Products.aspx?productId=" + selectedProduct, false);
            }
            else
                Debug.WriteLine("Error from [products.aspx.cs] : No proudct ID");
        }
    }
}