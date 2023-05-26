using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.UI;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Drawing;
using System.Web;

namespace WH_OnlineShoesShopping.NewFolder1
{
    public class OnlineShpping
    {
        public static string sConnection = ConfigurationManager.ConnectionStrings["mike_Ecommerce"].ConnectionString;
        public OnlineShpping() { }


        #region SignUp Registration
        public static bool Registeration(char roleID, string name, string username, string password, string email)
        {
            string sqlQuery = "INSERT INTO Member (roleID, name, username, password, email) VALUES (@roleID, @name, @username, @password, @email)";
            string s = "";

            using (SqlConnection conn = new SqlConnection(sConnection))
            {


                conn.Open();
                using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
                {
                    dataCommand.Parameters.AddWithValue("@roleID", roleID);
                    dataCommand.Parameters.AddWithValue("@name", name);
                    dataCommand.Parameters.AddWithValue("@username", username);
                    dataCommand.Parameters.AddWithValue("@password", password);
                    dataCommand.Parameters.AddWithValue("@email", email);


                    string existusernames = "select username from Member";
                    using (SqlCommand data = new SqlCommand(existusernames, conn))
                    {
                        using (SqlDataReader reader = data.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["username"].ToString() == username)
                                    return false;


                            }
                        }
                    }


                    try
                    {
                        dataCommand.ExecuteNonQuery();
                        System.Diagnostics.Debug.Write("success");
                    }

                    catch (Exception err)
                    {
                        System.Diagnostics.Debug.Write("Fail to execute non query from Sign up: " + err.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }


                    return true;
                }

            }
        }
        #endregion
        #region login
        public static Tuple<bool, string> LogIn(string userID, string password)
        {
            string query = "SELECT * FROM Member WHERE username = @username AND password = @password";

            using (SqlConnection conn = new SqlConnection(sConnection))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@username", userID);
                    command.Parameters.AddWithValue("@password", password);

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        string username = reader["username"].ToString();
                        reader.Close();
                        conn.Close();
                        return Tuple.Create(true, username);
                    }

                    reader.Close();
                    conn.Close();

                    return Tuple.Create(false, string.Empty);
                }
            }
        }

        #endregion

        public static DataTable GetDataTable(string query)
        {

            using (SqlConnection conn = new SqlConnection(sConnection))
            {
                using (SqlCommand dataCommand = new SqlCommand(query, conn))
                {
                    conn.Open();

                    // execute sqlcommand
                    dataCommand.ExecuteNonQuery();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(dataCommand);

                    // fill datatable
                    da.Fill(dt);

                    return dt;
                }
            }


        }
        public static string GetFullName(string username)
        {
            string sqlQuery = "SELECT name FROM Member WHERE name = @username";
            string fullName = "";

            using (SqlConnection conn = new SqlConnection(sConnection))
            {
                using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
                {
                    dataCommand.Parameters.AddWithValue("@username", username);

                    conn.Open();

                    try
                    {
                        SqlDataReader dr = dataCommand.ExecuteReader();

                        if (dr.Read())
                        {
                            fullName = dr["name"].ToString();
                        }

                        Debug.WriteLine("Success: Full name retrieved");
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine("Error: Failed to retrieve full name - " + err.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return fullName;
        }
        #region Insert Data into Board
        public static bool SubmitReview(string username, string content, int grade, int productID)
        {
            //insert into Board(memberID, content, boardDate, grade, productID)
            //select memberID, 'test', GETDATE(), 3, 4
            //from Member
            //where username = 'yyoo2'
            string s = "";
            DateTime dateTime = DateTime.Now;
           
            string sqlQuery = "INSERT INTO board(memberId, content, grade, productId, boardDate) "
                            + "SELECT memberID, @content,  @grade, @productId,@boardDate "  
                            + "FROM Member "
                            + $"where username = '{username}'";

            using (SqlConnection conn = new SqlConnection(sConnection))
            {
                using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
                {
                    dataCommand.Parameters.AddWithValue("@content", content);
                    dataCommand.Parameters.AddWithValue("@grade", grade);
                    dataCommand.Parameters.AddWithValue("@productId", productID);
                    dataCommand.Parameters.AddWithValue("@boardDate", dateTime);
                    

                    conn.Open();


                    try
                    {
                        dataCommand.ExecuteNonQuery();
                        Debug.Write("success to submit reveiw");
                    }
                    catch (Exception err)
                    {
                        Debug.Write("Fail to submit reveiw: " + err.Message);
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return true;
                }
            }
        }
        #endregion
        #region ContentDelete
        public static bool BoardDeleteBtn(int boardNum)
        {
            // query to delete review from board table
            string sqlQuery = $" delete from board"
                            + $" where boardNo = {boardNum}";

            using (SqlConnection conn = new SqlConnection(sConnection))
            {
                using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
                {

                    conn.Open();
                    try
                    {
                        dataCommand.ExecuteNonQuery();
                    }
                    catch (Exception err)
                    {
                        Debug.Write("Error from [BoardDeleteBtn]: " + err.Message);
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return true;
                }
            }

        }
        #endregion

        public static bool InsertOrUpdateCart(int productId, string username)
        {
            string sqlQuery = "declare @memID int ";
            sqlQuery += "SELECT @memID = memberId ";
            sqlQuery += "FROM Member ";
            sqlQuery += "where username = '" + username + "' ";
            sqlQuery += "if not exists (select * from Cart where productId = " + productId + " and memberId = @memID ) ";
            sqlQuery += "   begin ";
            sqlQuery += "   insert into Cart values (@productID,@memID, @amount ) ";
            sqlQuery += "   end ";
            sqlQuery += "else ";
            sqlQuery += "   update Cart ";
            sqlQuery += "   set amount = amount + @amount ";
            sqlQuery += "   where memberId = @memID and productId = @productID ";

            using (SqlConnection conn = new SqlConnection(sConnection))
            {
                using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
                {
                    //dataCommand.Parameters.AddWithValue("@memberID", memberID);
                    dataCommand.Parameters.AddWithValue("@productID", productId);
                    dataCommand.Parameters.AddWithValue("@amount", 1);

                    conn.Open();

                    try
                    {
                        dataCommand.ExecuteNonQuery();
                        Debug.Write("success to inserting item in cart");
                    }
                    catch (Exception err)
                    {
                        Debug.Write("Fail to add item to cart: " + err.Message);
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return true;
                }
            }
        }

        #region
        public static bool UpdateCartItemQantity(int productID, int newquantity, string username)
        {

            string sqlQuery = "update Cart ";
            sqlQuery += "set amount = @newquantity ";
            sqlQuery += "from Cart c ";
            sqlQuery += "join Member m on m.memberId = c.memberId ";
            sqlQuery += "where productId = @productID and m.username = @username ";

            using (SqlConnection conn = new SqlConnection(sConnection))
            {
                using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
                {
                    dataCommand.Parameters.AddWithValue("@productID", productID);
                    dataCommand.Parameters.AddWithValue("@newquantity", newquantity);
                    dataCommand.Parameters.AddWithValue("@username", username);

                    conn.Open();

                    try
                    {
                        dataCommand.ExecuteNonQuery();
                        Debug.Write("success to update item quantity from cart");
                    }
                    catch (Exception err)
                    {
                        Debug.Write("Fail to update item quantity from cart: " + err.Message);
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return true;
                }
            }
        }
        #endregion
        // delete selected item from cart
        public static bool RemoveCartItem(int productID, string username)
        {
            string sqlQuery = "delete Cart ";
            sqlQuery += "from Cart c ";
            sqlQuery += "join Member m on m.memberId = c.memberId ";
            sqlQuery += "where productId = @productId and m.username = @username ";

            using (SqlConnection conn = new SqlConnection(sConnection))
            {
                using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
                {
                    dataCommand.Parameters.AddWithValue("@productId", productID);
                    dataCommand.Parameters.AddWithValue("@username", username);

                    conn.Open();

                    try
                    {
                        dataCommand.ExecuteNonQuery();
                        Debug.Write("success to delete item from cart");
                    }
                    catch (Exception err)
                    {
                        Debug.Write("Fail to delete item from cart: " + err.Message);
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }

                    return true;
                }
            }
        }

    }
}

    
