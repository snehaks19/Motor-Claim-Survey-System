<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" UnobtrusiveValidationMode="None" CodeBehind="ErrorCodeMasterEdit.aspx.cs" Inherits="MotorSurveySystem.Master.ErrorCodeMasterEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <link href="../Style/ErrorCodeMasterStyleSheet.css" rel="stylesheet" />--%>
    <link href="../Style/CodeMasterEditStylesheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <div class="table-container">
        <div class="d-flex justify-content-between align-items-start">
            <asp:Button ID="backBtn" runat="server" Text="Back" CssClass="btn btn-success btn-right" OnClick="backBtn_Click" CausesValidation="false" />
            <h3>ERROR CODE MASTER</h3>
            <asp:Button ID="AddBtn" runat="server" Text="Add New" CssClass="btn btn-success btn-right" OnClick="AddBtn_Click" CausesValidation="false" />
        </div>
        <br />
        <asp:ScriptManager ID="ScriptManager4" runat="server" />
        <div class="container mt-4">
            <!-- First Row: Error Code, Error Type, Error Description -->
            <asp:UpdatePanel ID="updErrType" runat="server">
                <ContentTemplate>
                    <div class="row align-items-center mb-3">

                        <!-- Error Code Field -->
                        <div class="col-md-4">
                            <asp:Label ID="lblErrCode" runat="server" AssociatedControlID="txtErrCode" Text="Error Code:" CssClass="form-label" />
                            <span class="required">*</span>
                            <asp:TextBox ID="txtErrCode" runat="server" Placeholder="Enter Error Code" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtErrCode_TextChanged" MaxLength="12" />
                            <asp:RequiredFieldValidator ID="rfvErrCode" runat="server" ControlToValidate="txtErrCode" ErrorMessage="Error Code is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                        </div>

                        <!-- Error Type Field -->
                        <div class="col-md-4">
                            <asp:Label ID="lblErrType" runat="server" AssociatedControlID="ddlErrType" Text="Error Type:" CssClass="form-label" />
                            <span class="required">*</span>
                            <asp:DropDownList ID="ddlErrType" runat="server" CssClass="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvErrType" runat="server" ControlToValidate="ddlErrType" InitialValue="" ErrorMessage="Error Type is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                        </div>

                        <!-- Error Description Field -->
                        <div class="col-md-4">
                            <asp:Label ID="lblErrDesc" runat="server" AssociatedControlID="txtErrDesc" Text="Error Description:" CssClass="form-label" />
                            <span class="required">*</span>
                            <asp:TextBox ID="txtErrDesc" runat="server" Placeholder="Enter Description" CssClass="form-control" MaxLength="240" />
                            <asp:RequiredFieldValidator ID="RfvErrDesc" runat="server" ControlToValidate="txtErrDesc" ErrorMessage="Error Description is required." CssClass="text-danger" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" />
                        </div>
                    </div>
                    <br />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlErrType" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>

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
