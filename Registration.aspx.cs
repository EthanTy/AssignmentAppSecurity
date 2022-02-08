using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using System.Drawing;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//using System.Directory;


namespace AssignmentAppSecurity
{
    public partial class Registration : System.Web.UI.Page
    {

        string AssignmentDbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AssignmentDbConnection"].ConnectionString;

        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private string checkPassword(string password)
        {
            var tip = " ";
            //score implementation

            //lenghth less than 12

            if (password.Length < 12)
            {
                
                 tip += " password length should be 12 or more ";
            }

            //score 2 weak
            if (!Regex.IsMatch(password, "[a-z]"))
            {

                 tip += " lower caps ";
            }
           

              
            //score 3 medium
            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                 tip += " Upper caps ";

            }
            //score 4 strong
            if (!Regex.IsMatch(password, "[0-9]"))
            {
                 tip += " Numbers from 0-9 ";

            }
            //score 5 Very strong
            if (!Regex.IsMatch(password, "[@#$%^<&*_+]"))
            {
                 tip += "Needs special charecters ";

            }
            return tip;
            



        }

        //protected void bth_checkPassword_Click_Click(object sender, EventArgs e)
        //{
        //    // implement codes for the button event
        //    // Extract data from textbox
        //    string scores = checkPassword(tb_password.Text);
            
            
        //    //lbl_pwdchecker.Text = "Status : " + scores;
        //    if (scores != " ")
        //    {
        //        lbl_pwdchecker.ForeColor = Color.Red;
        //        return ;
        //    }

        //    else
        //    {
        //        lbl_pwdchecker.ForeColor = Color.Green;
        //        lbl_pwdchecker.Text = "Status : " + "Perfect";
        //    }
            
        //}


        protected bool createAccount()
        {
           
            try
            {
                using (SqlConnection con = new SqlConnection(AssignmentDbConnectionString))
                {   //if(tb_email.Text.Trim() != "@EmailAddress"  ){
                    //Database append code goes into here
                    //}

                    //else{ServerError.Text = "This Email already exists in our system, you may not use it."; }
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@Firstname,@Lastname,@CreditCardNumber,@CCV,@ExpiryDate,@EmailAddress,@PasswordHash,@PasswordSalt,@DateOfBirth,@Photo,@PhotoPath)"))



                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", tb_firstname.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", tb_lastname.Text.Trim());


                            cmd.Parameters.AddWithValue("@CreditCardNumber", encryptData(tb_creditcard.Text.Trim()));                            
                            cmd.Parameters.AddWithValue("@ExpiryDate", encryptData( tb_ExpireD.Text.Trim()));
                            cmd.Parameters.AddWithValue("@CCV", encryptData(tb_CVC.Text.Trim()));

                            cmd.Parameters.AddWithValue("@EmailAddress", tb_email.Text.Trim());
                            cmd.Parameters.AddWithValue("@DateOfBirth", tb_dateofbirth.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);

                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@Photo", "filename");
                            cmd.Parameters.AddWithValue("@PhotoPath", "filename");


                            //if (FileUploadphoto.HasFile)
                            //{
                            //    string filename = FileUploadphoto.FileName.ToString();
                            //    string folderPath = Server.MapPath("~/Images");
                            //    string image = "/Images/" + filename;
                            //    string storeImage = folderPath + filename;

                            //    if (!Directory.Exists(folderPath))
                            //    {
                            //        Directory.CreateDirectory(folderPath);
                            //    }
                            //    FileUploadphoto.SaveAs(storeImage);

                            //    FileUploadphoto.PostedFile.SaveAs(Server.MapPath("~/upload/") + filename);

                            //    cmd.Parameters.AddWithValue("@Photo", filename);
                            //}
                            //else
                            //{
                            //    lbl_Photochecker.Visible = true;
                            //    lbl_Photochecker.Text +="Plz upload the image!!!!";
                            //}



                            //cmd.Parameters.AddWithValue("@MobileVerified", DBNull.Value);
                            //cmd.Parameters.AddWithValue("@EmailVerified", DBNull.Value);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ServerError.Text = "Somthign went Wrong";

                //throw new Exception(ex.ToString());
                return false;

            }
            return true;
        }
        protected void btn_submit_click(object sender, EventArgs e)
        {
            string pwd = HttpUtility.HtmlEncode( tb_password.Text.ToString().Trim()) ;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];


            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);
            SHA512Managed hashing = new SHA512Managed();

            lbl_pwdchecker.Text = tb_password.Text;


            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
            finalHash = Convert.ToBase64String(hashWithSalt);


            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;
            if (createAccount()){
                Response.Redirect("Login.aspx", false);
            }
            



        }
        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)

            {
                //ServerError.Text = "Somthign went Wrong";
              throw new Exception(ex.ToString());
            }

            finally { }
            return cipherText;
        }
        protected void tb_creditcard_TextChanged(object sender, EventArgs e)
        {

        }
    }
}