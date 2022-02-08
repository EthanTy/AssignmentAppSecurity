using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;


using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;


using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace AssignmentAppSecurity
{
    public partial class Login : System.Web.UI.Page
    {

        string AssignmentDbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AssignmentDbConnection"].ConnectionString;
        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void RegisterMe(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx", false);

        }
        protected void LoginMe(object sender, EventArgs e)    
        {
            if (ValidateCaptcha() )
            {
                string pwd = tb_pwd.Text.ToString().Trim();
                string emailid = tb_emailid.Text.ToString().Trim();

                string fnameid = tb_firstnameid.Text.ToString().Trim();
                string lnameid = tb_lastnameid.Text.ToString().Trim();



                SHA512Managed hashing = new SHA512Managed();
                string dbHash = getDBHash(emailid, fnameid , lnameid);
                string dbSalt = getDBSalt(emailid, fnameid, lnameid);
                try
                {

                 
                
                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {
                        string pwdWithSalt = pwd + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);
                        if (userHash.Equals(dbHash))
                        {
                            Session["LoggedIn"] = tb_emailid.Text.Trim();

                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;

                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                            Response.Redirect("HomePage.aspx", false);
                        }


                        else
                        {
                            lblMessage.Text = "Userid or Email or password  is not valid. Please try again.";
                           
                        }
                    }
                }

                catch (Exception ex)

                {
                    throw new Exception(ex.ToString());
                }
                finally { }
                }
            else
            {
                lblMessage.Text = "Validate captcha to prove that you are a human.";
            }

        }
        protected string getDBHash(string userid , string firstid , string lastid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(AssignmentDbConnectionString);
            string sql = "select PasswordHash FROM Account WHERE EmailAddress=@USERID AND FirstName =@FIRSTID  AND LastName = @LASTID ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid );
            command.Parameters.AddWithValue("@FIRSTID", firstid);
            command.Parameters.AddWithValue("@LASTID", lastid);


            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;




        }


        protected string getDBSalt(string userid, string firstid, string lastid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(AssignmentDbConnectionString);
            string sql = "select PASSWORDSALT FROM ACCOUNT WHERE EmailAddress=@USERID AND FirstName =@FIRSTID  AND LastName = @LASTID ";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            command.Parameters.AddWithValue("@FIRSTID", firstid);
            command.Parameters.AddWithValue("@LASTID", lastid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }


        



        public bool ValidateCaptcha()
        {
            bool result = true;

            //When user submits the recaptcha form, the user gets a response POST parameter. 
            //captchaResponse consist of the user click pattern. Behaviour analytics! AI :) 
            string captchaResponse = Request.Form["g-recaptcha-response"];

            //To send a GET request to Google along with the response and Secret key.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
           (" https://www.google.com/recaptcha/api/siteverify?secret=6LdPEjkdAAAAAIOxuLfY4yXVp8dZ7I7zCB3IisKy &response=" + captchaResponse);


            try
            {

                //Codes to receive the Response in JSON format from Google Server
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        //The response in JSON format
                        string jsonResponse = readStream.ReadToEnd();

                        //To show the JSON response string for learning purpose
                        //lbl_gScore.Text = jsonResponse.ToString();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        //Create jsonObject to handle the response e.g success or Error
                        //Deserialize Json
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        //Convert the string "False" to bool false or "True" to bool true
                        result = Convert.ToBoolean(jsonObject.success);//

                    }
                }

                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
    }

