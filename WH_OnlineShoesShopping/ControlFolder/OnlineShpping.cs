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
        /*     public static Tuple<bool, string> LogIn(string userID, string password)
             {
                 string query = "select * from Member where username =@username and password =@password";
                 using (SqlConnection conn = new SqlConnection(sConnection))
                 {
                     using (SqlCommand command = new SqlCommand(query, conn))
                     {
                         command.Parameters.AddWithValue("username", userID);
                         command.Parameters.AddWithValue("password", password);
                         SqlDataAdapter sda = new SqlDataAdapter(command);
                         DataTable dt = new DataTable();
                         sda.Fill(dt);

                         conn.Open();

                         try
                         {
                             command.ExecuteNonQuery();
                             System.Diagnostics.Debug.Write("success Login");
                         }
                         catch (Exception err)
                         {
                             System.Diagnostics.Debug.Write("Fail to execute non query from Sign up: " + err.Message);
                         }
                         finally
                         {
                             conn.Close();
                         }

                         if (dt.Rows.Count > 0)
                         {// Admin , admin
                             string s = "";
                             s = (from DataRow dr in dt.Rows
                                  where ((string)dr.ItemArray[3]).ToLower() == userID.ToLower()
                                  select dr.ItemArray[3]).First().ToString();

                             return Tuple.Create(true, s);
                         }

                         return Tuple.Create(false, "");
                     }
                 }
             }*/
        #endregion
    }
}

    
