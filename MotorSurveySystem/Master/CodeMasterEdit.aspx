<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" UnobtrusiveValidationMode="None" CodeBehind="CodeMasterEdit.aspx.cs" Inherits="MotorSurveySystem.Master.CodeMasterEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Style/CodeMasterEditStylesheet.css" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Script/CodeMaster.js"></script>
    <div class="table-container">


        <div class="d-flex justify-content-between align-items-start">
            <asp:Button ID="backBtn" runat="server" Text="Back" CssClass="btn btn-success" OnClick="backBtn_Click" CausesValidation="false" />
            <h3>CODES MASTER</h3>
            <asp:Button ID="AddBtn" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="AddBtn_Click" CausesValidation="false" />
        </div>

        <br />
        
        <asp:ScriptManager ID="ScriptManager5" runat="server" />
        

        <div class="container mt-4">
            <asp:UpdatePanel ID="updCodesMaster" runat="server">
            <ContentTemplate>
            <!-- First Row: Code, Type, Description -->
            <div class="row align-items-center mb-3">
                <!-- Code Field ccv -->
                <div class="col-md-4">
                    <asp:Label ID="lblCmCode" runat="server" AssociatedControlID="txtCmCode" Text="Code:" CssClass="form-label" />
                    <span class="required">*</span>
                    <asp:TextBox ID="txtCmCode" runat="server" Placeholder="Enter Code" AutoPostBack="true" OnTextChanged="txtCmCode_TextChanged" CssClass="form-control" MaxLength="12" />
                    <asp:RequiredFieldValidator ID="rfvCmCode" runat="server" ControlToValidate="txtCmCode" ErrorMessage="Code is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                </div>
                /
                <!-- Type Field -->
                <div class="col-md-4">
                    <asp:Label ID="lblCmType" runat="server" AssociatedControlID="txtCmType" Text="Type:" CssClass="form-label" />
                    <span class="required">*</span>
                    <asp:TextBox ID="txtCmType" runat="server" Placeholder="Enter Type" AutoPostBack="true" OnTextChanged="txtCmCode_TextChanged" CssClass="form-control" MaxLength="12" />
                    <asp:RequiredFieldValidator ID="rfvCmType" runat="server" ControlToValidate="txtCmType" ErrorMessage="Type is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                </div>

                <!-- Description Field -->
                <div class="col-md-4">
                    <asp:Label ID="lblCmDesc" runat="server" AssociatedControlID="txtCmDesc" Text="Description:" CssClass="form-label" />
                    <asp:TextBox ID="txtCmDesc" runat="server" Placeholder="Enter Description" CssClass="form-control" MaxLength="240" />
                </div>
            </div>

            <!-- Second Row: Parent Code, Value, Active -->
            <div class="row align-items-center mb-3">


                <!-- Value Field -->
                <div class="col-md-4">
                    <asp:Label ID="lblCmValue" runat="server" AssociatedControlID="txtCmValue" Text="Value:" CssClass="form-label" />
                    <asp:TextBox ID="txtCmValue" runat="server" Placeholder="Enter Value" CssClass="form-control" onkeypress="validateNumericInput(event)" oninput="limitToTwoDecimalPlaces(this)" MaxLength="9" Style="text-align: right;" />
                </div>

                <!-- Active Status Field -->
                <div class="col-md-4 d-flex align-items-center">
                    <asp:Label ID="lblActive" runat="server" Text="Active(Y/N):" AssociatedControlID="chkCmActiveYn" CssClass="form-label" />
                    <asp:CheckBox ID="chkCmActiveYn" runat="server" CssClass="ms-3" />
                </div>
            </div>
                 </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtCmType" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>

            <br />
            
           
            <!-- Buttons Row -->
            <div class="row justify-content-center">
                <!-- Center the buttons horizontally -->
                <div class="col-md-4 d-flex justify-content-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success mx-2" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success mx-2" OnClick="btnUpdate_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
