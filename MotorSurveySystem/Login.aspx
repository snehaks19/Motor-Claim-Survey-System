<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" UnobtrusiveValidationMode="None" Inherits="MotorSurveySystem.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Motor Survey System</title> 
    <link href="Style/Login.css" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="login-image">
              <h1 class="title">MOTOR CLAIM<br />SURVEY SYSTEM</h1>
            </div>
            <div class="login-form">
                <h1>LOGIN</h1>
                <!-- User ID -->
                <asp:Label ID="lblUserId" runat="server" Text="User ID:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtUserId" runat="server" CssClass="form-input" Placeholder="Enter your user ID" required="true" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId" ErrorMessage="Please enter your user ID" CssClass="form-error"></asp:RequiredFieldValidator>

                <!-- Password -->
                <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-input" TextMode="Password" Placeholder="Enter your password" required="true" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please enter your password" CssClass="form-error"></asp:RequiredFieldValidator>

                <!-- Login Button -->
                <asp:Button ID="btnLogin" runat="server" CssClass="submit-btn" Text="Login" OnClick="btnLogin_Click" />
            </div>
        </div>
    </form>
</body>

</html>
