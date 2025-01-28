<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="Policy.aspx.cs" Inherits="MotorSurveySystem.Transaction.Policy" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <link href="../Style/Policy.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-3.7.1.js"></script>
    <script src="https://code.jquery.com/ui/1.14.0/jquery-ui.min.js"></script>

    <script src="../Script/dates.js"></script>

    <div class="heads">
        <div class="d-flex justify-content-between align-items-start">
            <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="btn btn-primary back" OnClick="backbtn_Click" CausesValidation="false" />
            <asp:Button ID="approveBtn" runat="server" Text="Approve" CssClass="btn btn-primary right" OnClick="approveBtn_Click" />
        </div>
        <h2>
            <asp:Label ID="NewPolicytitle" runat="server" Text="NEW POLICY"></asp:Label>
        </h2>
        <h2>
            <asp:Label ID="PolicyDetailsTitle" runat="server" Text="POLICY DETAILS"></asp:Label>
        </h2>
        <h2>
            <asp:Label ID="ApprovedPolicyTitle" runat="server" Text="POLICY APPROVED" Visible="false"></asp:Label>
        </h2>
    </div>


    <div class="container mt-4">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div class="row">
            <!-- Policy No -->
            <div class="col-md-4">
                <asp:Label ID="lblPolNo" runat="server" AssociatedControlID="txtPolNo" Text="Policy No:" />
                <asp:TextBox ID="txtPolNo" runat="server" CssClass="form-control" ReadOnly="true" />
                <asp:HiddenField ID="hfuid" runat="server" Value="0" />
            </div>

            <!-- Issue Date -->
            <div class="col-md-4">
                <asp:Label ID="lblPolIssDt" runat="server" AssociatedControlID="txtPolIssDt" Text="Issue Date:" />
                <asp:TextBox ID="txtPolIssDt" runat="server" CssClass="form-control" Placeholder="Enter Issue Date" AutoPostBack="true" />
                <asp:RequiredFieldValidator ID="rfvPolIssDt" runat="server" ControlToValidate="txtPolIssDt" ErrorMessage="Issue Date is required." CssClass="text-danger" />
            </div>

            <!-- From Date -->
            <div class="col-md-4">
                <asp:UpdatePanel ID="updFromDate" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblPolFmDt" runat="server" AssociatedControlID="txtPolFmDt" Text="From Date:" />
                        <span class="required">*</span>
                        <asp:TextBox ID="txtPolFmDt" runat="server" ClientIDMode="Static" CssClass="form-control date-picker" Placeholder="Enter From Date" OnTextChanged="txtPolFmDt_TextChanged" AutoPostBack="true" />
                        <asp:RequiredFieldValidator ID="rfvPolFmDt" runat="server" ControlToValidate="txtPolFmDt" ErrorMessage="From Date is required." CssClass="text-danger" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtPolFmDt" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <!-- To Date -->
            <div class="col-md-4">
                <asp:UpdatePanel ID="updToDate" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblPolToDt" runat="server" AssociatedControlID="txtPolToDt" Text="To Date:" />
                        <asp:TextBox ID="txtPolToDt" runat="server" CssClass="form-control" Placeholder="Enter To Date" ReadOnly="true" />
                        <asp:RequiredFieldValidator ID="rfvPolToDt" runat="server" ControlToValidate="txtPolToDt" ErrorMessage="To Date is required." CssClass="text-danger" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <!-- Assured Name -->
            <div class="col-md-4">
                <asp:Label ID="lblPolAssrName" runat="server" AssociatedControlID="txtPolAssrName" Text="Assured Name:" />
                <span class="required">*</span>
                <asp:TextBox ID="txtPolAssrName" runat="server" CssClass="form-control" Placeholder="Enter Assured Name" MaxLength="120" />
                <asp:RequiredFieldValidator ID="rfvPolAssrName" runat="server" ControlToValidate="txtPolAssrName" ErrorMessage="Assured Name is required." CssClass="text-danger" />
            </div>

            <!-- Assured Mobile -->
            <div class="col-md-4">
                <asp:Label ID="lblPolAssrMobile" runat="server" AssociatedControlID="txtPolAssrMobile" Text="Assured Mobile:" />
                <span class="required">*</span>
                <asp:TextBox ID="txtPolAssrMobile" runat="server" CssClass="form-control" Placeholder="Enter Assured Mobile" onkeypress="validateNumericOnly(event)" MaxLength="10" />
                <asp:RequiredFieldValidator ID="rfvPolAssrMobile" runat="server" ControlToValidate="txtPolAssrMobile" ErrorMessage="Assured Mobile No is required." CssClass="text-danger" />

                <!-- Regular Expression Validator -->
                <asp:RegularExpressionValidator
                    ID="revPolAssrMobile"
                    runat="server"
                    ControlToValidate="txtPolAssrMobile"
                    ErrorMessage="Mobile number must be 10 digits."
                    CssClass="text-danger"
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationExpression="^\d{10}$" />
            </div>

        </div>
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <!-- Currency Code -->

                    <div class="col-md-4">

                        <asp:Label ID="lblPolCurrCode" runat="server" AssociatedControlID="ddlPolCurrCode" Text="Currency Code:" />
                        <span class="required">*</span>
                        <asp:DropDownList ID="ddlPolCurrCode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlPolCurrCode_TextChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvPolCurrCode" runat="server" ControlToValidate="ddlPolCurrCode" ErrorMessage="Currency Code is required." CssClass="text-danger" InitialValue="" />
                    </div>

                    <!-- Gross FC Premium -->
                    <div class="col-md-4">
                        <asp:Label ID="lblPolGrossFCPrem" runat="server" AssociatedControlID="txtPolGrossFCPrem" Text="Gross FC Premium:" />
                        <span class="required">*</span>
                        <asp:TextBox ID="txtPolGrossFCPrem" runat="server" class="" CssClass="form-control numberText" Style="text-align: right;" Placeholder="Enter Gross FC Premium" AutoPostBack="true" OnTextChanged="txtPolGrossFCPrem_TextChanged1" MaxLength="12" />

                        <asp:RequiredFieldValidator ID="rfvPolGrossFCPrem" runat="server" ControlToValidate="txtPolGrossFCPrem" ErrorMessage="Gross FC Premium is required." CssClass="text-danger" />
                    </div>

                    <!-- Gross LC Premium -->
                    <div class="col-md-4">
                        <asp:Label ID="lblPolGrossLCPrem" runat="server" AssociatedControlID="txtPolGrossLCPrem" Text="Gross LC Premium:" />
                        <asp:TextBox ID="txtPolGrossLCPrem" runat="server" CssClass="form-control numberText" Style="text-align: right;" Placeholder="Enter Gross LC Premium" AutoPostBack="true" ReadOnly="true" />
                    </div>
                </div>

                <div class="row">
                    <!-- Vehicle Make -->
                    <div class="col-md-4">
                        <asp:Label ID="lblPolVehMake" runat="server" AssociatedControlID="ddlPolVehMake" Text="Vehicle Make:" />
                        <span class="required">*</span>
                        <asp:DropDownList ID="ddlPolVehMake" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPolVehMake_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvPolVehMake" runat="server" ControlToValidate="ddlPolVehMake" ErrorMessage="Vehicle Make is required." CssClass="text-danger" InitialValue="" />
                    </div>

                    <!-- Vehicle Model -->
                    <div class="col-md-4">
                        <asp:Label ID="lblPolVehModel" runat="server" AssociatedControlID="ddlPolVehModel" Text="Vehicle Model:" />
                        <span class="required">*</span>
                        <asp:DropDownList ID="ddlPolVehModel" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvPolVehModel" runat="server" ControlToValidate="ddlPolVehModel" ErrorMessage="Vehicle Model is required." CssClass="text-danger" InitialValue="" />
                    </div>

                    <!-- Chassis Number -->
                    <div class="col-md-4">
                        <asp:Label ID="lblPolVehChassisNo" runat="server" AssociatedControlID="txtPolVehChassisNo" Text="Chassis Number:" />
                        <span class="required">*</span>
                        <asp:TextBox ID="txtPolVehChassisNo" runat="server" CssClass="form-control chsno" Placeholder="Enter Chassis Number" MaxLength="30" />

                        <asp:RequiredFieldValidator ID="rfvPolVehChassisNo" runat="server" ControlToValidate="txtPolVehChassisNo" ErrorMessage="Chassis Number is required." CssClass="text-danger" />
                    </div>
                </div>

                <div class="row">
                    <!-- Engine Number -->
                    <div class="col-md-4">
                        <asp:Label ID="lblPolVehEngineNo" runat="server" AssociatedControlID="txtPolVehEngineNo" Text="Engine Number:" />
                        <span class="required">*</span>
                        <asp:TextBox ID="txtPolVehEngineNo" runat="server" CssClass="form-control engno" Placeholder="Enter Engine Number" MaxLength="30" />
                        <asp:RequiredFieldValidator ID="rfvPolVehEngineNo" runat="server" ControlToValidate="txtPolVehEngineNo" ErrorMessage="Engine Number is required." CssClass="text-danger" />
                    </div>

                    <!-- Registration Number -->
                    <div class="col-md-4">
                        <asp:Label ID="lblPolVehRegnNo" runat="server" AssociatedControlID="txtPolVehRegnNo" Text="Registration Number:" />
                        <span class="required">*</span>
                        <asp:TextBox ID="txtPolVehRegnNo" runat="server" CssClass="form-control regno" Placeholder="Enter Registration Number" MaxLength="30" />
                        <asp:RequiredFieldValidator ID="rfvPolVehRegnNo" runat="server" ControlToValidate="txtPolVehRegnNo" ErrorMessage="Registration Number is required." CssClass="text-danger" />
                    </div>

                    <!-- Vehicle Value -->
                    <div class="col-md-4">
                        <asp:Label ID="lblPolVehValue" runat="server" AssociatedControlID="txtPolVehValue" Text="Vehicle Value:" />
                        <span class="required">*</span>
                        <asp:TextBox ID="txtPolVehValue" runat="server" CssClass="form-control numberText" Style="text-align: right;" Placeholder="Enter Vehicle Value" onkeypress="validateNumericInput(event)" AutoPostBack="true" MaxLength="12" />
                        <asp:RequiredFieldValidator ID="rfvPolVehValue" runat="server" ControlToValidate="txtPolVehValue" ErrorMessage="Vehicle Value is required." CssClass="text-danger" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlPolCurrCode" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtPolGrossFCPrem" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlPolVehMake" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtPolVehValue" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <!-- Buttons -->
        <div class="row justify-content-center">
            <div class="col-md-4 d-flex justify-content-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success mx-2" OnClick="btnSave_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success mx-2" OnClick="btnUpdate_Click" />
            </div>
        </div>
    </div>
</asp:Content>
