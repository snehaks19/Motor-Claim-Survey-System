<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" UnobtrusiveValidationMode="None" CodeBehind="UserMasterEdit.aspx.cs" Inherits="MotorSurveySystem.Master.UserMasterEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <link href="../Style/UserMasterEditStyleSheet.css" rel="stylesheet" />--%>
    <link href="../Style/CodeMasterEditStylesheet.css" rel="stylesheet" />
   
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <%--<script>
        function togglePassword() {
            var passwordInput = document.getElementById("txtUserPassword");
 
            if (passwordInput.type === "password") {
                passwordInput.type = "text";
            } else {
                passwordInput.type = "password";
            }
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="table-container">
        <div class="d-flex justify-content-between align-items-start">
            <asp:Button ID="backBtn" runat="server" Text="Back" CssClass="btn btn-success btn-right" OnClick="backBtn_Click" CausesValidation="false" />
            <h3>USER MASTER</h3>
            <asp:Button ID="AddBtn" runat="server" Text="Add New" CssClass="btn btn-success btn-right" OnClick="AddBtn_Click" CausesValidation="false" />
        </div>
    </div>

    <br />

    <div class="container mt-4">
        <asp:ScriptManager ID="ScriptManager6" runat="server" />
        <asp:UpdatePanel ID="updUserMaster" runat="server">
            <ContentTemplate>

        <!-- First Row -->
        <div class="row align-items-center mb-3">


            <!-- User ID Field -->
            <div class="col-md-4">
                <asp:Label ID="lblUserId" runat="server" Text="User ID:" CssClass="form-label" />
                <span class="required">*</span>
                <asp:TextBox ID="txtUserId" runat="server" Placeholder="Enter User ID" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUserId_TextChanged" MaxLength="12" />
                <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId"
                    ErrorMessage="User ID is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
            </div>

            <!-- User Type Dropdown -->
            <div class="col-md-4">
                <asp:Label ID="lblUserType" runat="server" Text="User Type:" CssClass="form-label" />
                <span class="required">*</span>
                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ControlToValidate="ddlUserType" InitialValue=""
                    ErrorMessage="User Type is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
            </div>

            <!-- User Name Field -->
            <div class="col-md-4">
                <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="form-label" />
                <span class="required">*</span>
                <asp:TextBox ID="txtUserName" runat="server" Placeholder="Enter User Name" CssClass="form-control" MaxLength="30" />
                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                    ErrorMessage="User Name is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
            </div>
        </div>

        <!-- Second Row -->
         <div class="row align-items-center mb-3">
            <!-- User Password Field -->
            <div class="col-md-4">
                <asp:Label ID="lblUserPassword" runat="server" Text="Password:" CssClass="form-label" />
                <span class="required">*</span>
                <asp:TextBox ID="txtUserPassword" runat="server" Placeholder="Enter User Password" TextMode="Password" CssClass="form-control" ClientIDMode="Static" MaxLength="24" />
                <asp:RequiredFieldValidator ID="rfvUserPassword" runat="server" ControlToValidate="txtUserPassword"
                    ErrorMessage="User Password is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
            </div>

            <!-- User Active Checkbox -->
            <div class="col-md-4">
                <asp:Label ID="lblUserActive" runat="server" Text="Active User:" CssClass="form-label d-block" />
                <asp:CheckBox ID="chkUserActive" runat="server" CssClass="form-check-input" />
            </div>
        </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlUserType" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <!-- Buttons Row -->
        <div class="row justify-content-center"> <!-- Center the buttons horizontally -->
            <div class="col-md-4 d-flex justify-content-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success " OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success " OnClick="btnUpdate_Click" />
            </div>
        </div>
    </div>


</asp:Content>
