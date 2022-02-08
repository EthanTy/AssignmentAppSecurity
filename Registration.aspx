<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AssignmentAppSecurity.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My registration</title>
    <script type ="text/javascript">
        function validate() {
           
            var str = document.getElementById('<%=tb_password.ClientID%>').value
            var CCN = document.getElementById('<%=tb_creditcard.ClientID%>').value
            var EXP = document.getElementById('<%=tb_ExpireD.ClientID%>').value
            var error = false


            document.getElementById("lbl_CCchecker").innerHTML = " "
            document.getElementById("lbl_pwdchecker").innerHTML = " "
            document.getElementById("lbl_EXPchecker").innerHTML = " "
            document.getElementById("bth_submit_Click").disabled = true

           
            if (EXP.search(/[0-9] {0,7}$/) == -1) {
                document.getElementById("lbl_EXPchecker").innerHTML += "The Expiery Date must be written as so mm/yyyy";
                document.getElementById("lbl_EXPchecker").style.color = "Red";
                error = true
            }
            if (CCN.search(/^[\d]{16,16}$/) == -1) {
                document.getElementById("lbl_CCchecker").innerHTML += "CreditCard must be 16 Digits";
                document.getElementById("lbl_CCchecker").style.color = "Red";
                error = true
            }


            if (str.length < 12) {
                document.getElementById("lbl_pwdchecker").innerHTML += "Password length must be at least 12 charecters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                console.log("too_short");
                error = true

            }
            if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML += "Password requires at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                console.log("no_number");
                error = true
            }
            if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerhtml += "password requires at least 1 capital";
                document.getElementById("lbl_pwdchecker").style.color = "red";
                console.log("no_uppercaps");
                error = true

            }
            if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerhtml += "password requires at least 1 lower caps";
                document.getElementById("lbl_pwdchecker").style.color = "red";
                console.log("no_lowercaps");
                error = true
            }
            if (str.search(/[@#$%^<&*\_+]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerhtml += "password requires at least 1 special charecter";
                document.getElementById("lbl_pwdchecker").style.color = "red";
                console.log("no-special charecter");
                error = true
            }
            console.log(error)
            if (error == false) {

                
                document.getElementById("bth_submit_Click").disabled = false
            }

            
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            Registration<br />
            First Name<asp:TextBox ID="tb_firstname" runat="server" Width="209px" onkeyup="javascript:validate()"></asp:TextBox>
            <br />
            Last Name<asp:TextBox ID="tb_lastname" runat="server" Width="209px" onkeyup="javascript:validate()"></asp:TextBox>
            <br />


            Credit Card Number<asp:TextBox ID="tb_creditcard" runat="server" TextMode="Number" Width="209px" onkeyup="javascript:validate()" OnTextChanged="tb_creditcard_TextChanged"></asp:TextBox>


            <asp:Label ID="lbl_CCchecker" runat="server" Text =" "></asp:Label>
            <br />
            Expiration Date <asp:TextBox ID="tb_ExpireD" runat="server"  placeholder="" Width="209px" onkeyup="javascript:validate()"></asp:TextBox>
            <asp:Label ID="lbl_EXPchecker" runat="server" Text =" "></asp:Label>


            <br />
            CVC<asp:TextBox ID="tb_CVC" runat="server" TextMode="Number" Width="75px" onkeyup="javascript:validate()"></asp:TextBox>

            <br />

            Email Address<asp:TextBox ID="tb_email" runat="server" Width="209px" onkeyup="javascript:validate()"></asp:TextBox>
            <br />
            Date Of birth<asp:TextBox ID="tb_dateofbirth" runat="server" placeholder="mm/dd/yyyy" TextMode="Date" ReadOnly="false"  onchange="javascript:validate()" ></asp:TextBox>
            <br />
            Password<asp:TextBox ID="tb_password" runat="server" Width="209px" onkeyup="javascript:validate()" Type ="Password" ></asp:TextBox>
            <asp:Label ID="lbl_pwdchecker" runat="server" Text =" "></asp:Label>
            <br />
            Photo<asp:FileUpload ID="FileUploadphoto" runat="server"  onchange="javascript:validate()"/>
            <asp:Label ID="lbl_Photochecker" runat="server" Text =" "></asp:Label>
            <br />
            
            <asp:Button ID="bth_submit_Click" runat="server" OnClick="btn_submit_click" Text="Submit" disabled="disabled" />
            
            <asp:Label ID="ServerError" runat="server" Text =" "></asp:Label>
        </div>
    </form>
</body>
    </html>