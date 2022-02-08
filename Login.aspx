<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AssignmentAppSecurity.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    
      <script src="https://www.google.com/recaptcha/api.js?"></script>


    <style type="text/css">
        #btn_login {
            width: 239px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                
                <legend>Login</legend>
                
                <p>First Name: <asp:TextBox ID="tb_firstnameid" runat ="server" Height="25px" Width="137px" /> 
                <p>Last Name: <asp:TextBox ID="tb_lastnameid" runat ="server" Height="25px" Width="137px" /> 
                <p>Email: <asp:TextBox ID="tb_emailid" runat ="server" Height="25px" Width="137px" /> </p>
                <p>Password: <asp:TextBox ID="tb_pwd" runat ="server" Height="24px" Width="137px" Type ="Password"/> </p>
                <p><asp:Button ID="btnSubmit" runat="server" Text ="Login" OnClick ="LoginMe" Height="27px" Width="133px" /></p>

                <asp:Button ID="bth_Register" runat="server" OnClick="RegisterMe" Text="No account, register here" />
                <br />
                <br />
                <div class="g-recaptcha" data-sitekey="6LdPEjkdAAAAAJQSF1_AOrddsx77IfOd3p2MLfha"></div>
                    <%--<input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>--%>
                <asp:Label ID ="lblMessage" runat ="server" EnableViewState ="False" Text =" "></asp:Label>

                     
              
            </fieldset>


        </div>
    </form>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute(' 6LdPEjkdAAAAAJQSF1_AOrddsx77IfOd3p2MLfha ', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>
