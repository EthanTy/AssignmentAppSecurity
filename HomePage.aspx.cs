using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssignmentAppSecurity
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"]!= null && Session["AuthToken"] != null && Request.Cookies["AUthtoken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    lblMessage.Text = "Your Logged in. ";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    btnLogout.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }
            
        }

        protected void LogoutMe(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            if (Request.Cookies["ASP.NET_SessionId"]!= null)
            {
                Request.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Request.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                Response.Cookies.Add(Request.Cookies["ASP.NET_SessionId"]);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Request.Cookies["AuthToken"].Value = string.Empty;
                Request.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
                Response.Cookies.Add(Request.Cookies["AuthToken"]);
            }
            
            //token isnt changing...

            Response.Redirect("Login.aspx", false);


        }
    }
}