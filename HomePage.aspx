<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="AssignmentAppSecurity.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset style="height: 243px">
                <legend>Homepage</legend>

                <br />

        

                <asp:Label ID="lblMessage" runat="server" EnableViewState="false"/>
                <br />
                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AssignmentDbConnection %>" SelectCommand="SELECT [LastName], [FirstName], [Id], [DateOfBirth], [Photo], [PhotoPath] FROM [Account]"></asp:SqlDataSource>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                    <Columns>
                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="DateOfBirth" HeaderText="DateOfBirth" SortExpression="DateOfBirth" />
                        <asp:BoundField DataField="Photo" HeaderText="Photo" SortExpression="Photo" />
                        <asp:BoundField DataField="PhotoPath" HeaderText="PhotoPath" SortExpression="PhotoPath" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Image ID="Image1" runat="server" Height="147px" ImageUrl='<%# Eval("PhotoPath") %>' Width="154px" />
                    </EmptyDataTemplate>
                </asp:GridView>
                <br />
                
                <p>
                <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="LogoutMe" Visible="false"/>

                </p>
        

            </fieldset>
        </div>
    </form>
</body>
</html>
