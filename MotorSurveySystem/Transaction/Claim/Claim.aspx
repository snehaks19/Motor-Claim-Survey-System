<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="Claim.aspx.cs" UnobtrusiveValidationMode="None" Inherits="MotorSurveySystem.Transaction.Claim.Claim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <link href="../../Style/Claim.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-3.7.1.js"></script>
    <script src="https://code.jquery.com/ui/1.14.0/jquery-ui.min.js"></script>
    <script src="../../Script/dates.js"></script>
    <script src="../../Script/Claim.js"></script>

    <style>
        /*.numberText {
            text-align: right;
        }*/
        /*.buttons{
            float:right;
        }*/
        /*.back {
            float: left;
            margin-left: 120px;
            margin-top: 20px;
        }*/

        /*.btn-success {
            background-color: navy;
        }*/
    </style>


    <%-- <div class="buttons" style="display: flex; justify-content: space-between; align-items: center;">
        <!-- Left-aligned Back button -->
        <div>
            <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="btn btn-success back" OnClick="backbtn_Click" CausesValidation="false" />
        </div>

        <!-- Right-aligned elements -->
        <div style="display: flex; align-items: center; margin-right: 120px; margin-top: 40px;">
            <asp:Label ID="lblSurveyNo" runat="server" Text="" Style="color: red; font-weight: bold; padding-inline-end: 50px;" />
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-success mx-2" OnClick="btnPrint_Click" Visible="false" />
        </div>
    </div>--%>
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <div class="heads">
        <div class="d-flex justify-content-between align-items-start">
            <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="btn btn-success back" OnClick="backbtn_Click" CausesValidation="false" />
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-success mx-2" OnClick="btnPrint_Click" Visible="false" />
        </div>
        <h2>CLAIM DETAILS</h2>

    </div>

    <div class="container mt-4">
        <asp:Label ID="lblSurveyNo" runat="server" Text="" Style="color: red; font-weight: bold; padding-inline-end: 50px;" /></br>
        <asp:Label ID="lblClaimApprStatus" runat="server" Text="" CssClass="text-success" Visible="false" Style="text-align: left; display: block; font-weight: bold; font-size: 20px;" />
        <asp:Label ID="lblClaimRejStatus" runat="server" Text="" CssClass="text-danger" Visible="false" Style="text-align: left; display: block; font-weight: bold; font-size: 20px;" />

        <br />

        <asp:UpdatePanel ID="polNoUpdatePanel" runat="server">
            <ContentTemplate>

                <div class="row">
                    <!-- Claim UID (hidden) -->
                    <asp:HiddenField ID="hfClmUid" runat="server" Value="0" />

                    <!-- Claim No -->
                    <div class="col-md-4">
                        <asp:Label ID="lblClmNo" runat="server" AssociatedControlID="txtClmNo" Text="Claim No:" />
                        <asp:TextBox ID="txtClmNo" runat="server" CssClass="form-control" ReadOnly="true" />
                        <br />
                    </div>

                    <!-- Policy No (Dropdown) -->
                    <div class="col-md-4">
                        <asp:Label ID="lblClmPolNo" runat="server" AssociatedControlID="ddlClmPolNo" Text="Policy No:" />
                        <span class="required">*</span>
                        <asp:DropDownList ID="ddlClmPolNo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClmPolNo_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator ID="rfvClmPolNo" runat="server" ControlToValidate="ddlClmPolNo"
                            ErrorMessage="Policy No is required."
                            ForeColor="Red"
                            Display="Dynamic"
                            InitialValue=""
                            CssClass="text-danger" />
                        <br />
                    </div>

                    <!-- From Date -->
                    <div class="col-md-4">                      
                        <asp:Label ID="lblClmPolFmDt" runat="server" AssociatedControlID="txtClmPolFmDt" Text="From Date:" />
                        <asp:TextBox ID="txtClmPolFmDt" runat="server" CssClass="form-control" TextMode="Date" ReadOnly="true" />
                        <br />                     
                    </div>
                </div>

                <div class="row">
                    <!-- To Date -->
                    <div class="col-md-4">
                        <asp:Label ID="lblClmPolToDt" runat="server" AssociatedControlID="txtClmPolToDt" Text="To Date:" />
                        <asp:TextBox ID="txtClmPolToDt" runat="server" CssClass="form-control" TextMode="Date" ReadOnly="true" />
                        <br />
                    </div>

                    <!-- Assured Name -->
                    <div class="col-md-4">
                        <asp:Label ID="lblClmPolAssrName" runat="server" AssociatedControlID="txtClmPolAssrName" Text="Assured Name:" />
                        <asp:TextBox ID="txtClmPolAssrName" runat="server" CssClass="form-control" />
                        <br />
                    </div>

                    <!-- Assured Mobile -->
                    <div class="col-md-4">
                        <asp:Label ID="lblClmPolAssrMob" runat="server" AssociatedControlID="txtClmPolAssrMob" Text="Assured Mobile:" />
                        <asp:TextBox ID="txtClmPolAssrMob" runat="server" CssClass="form-control" MaxLength="10" ReadOnly="true" />
                        <br />
                    </div>
                </div>

            

        <div class="row">

            <!-- Loss Date -->
            <div class="col-md-4">
                <asp:Label ID="lblClmLossDt" runat="server" AssociatedControlID="txtClmLossDt" Text="Loss Date:" />
                <span class="required">*</span>
                <asp:TextBox ID="txtClmLossDt" runat="server" CssClass="form-control date-picker" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtClmLossDt_TextChanged" />
                <asp:RequiredFieldValidator ID="rfvClmLossDt" runat="server" ControlToValidate="txtClmLossDt"
                    ErrorMessage="Loss Date is required." CssClass="text-danger" Display="Dynamic" />


            </div>

            <!-- Intimation Date -->
            <div class="col-md-4">
                <asp:Label ID="lblClmIntmDt" runat="server" AssociatedControlID="txtClmIntmDt" Text="Intimation Date:" />
                <span class="required">*</span>
                <asp:TextBox ID="txtClmIntmDt" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtClmIntmDt_TextChanged" />
                <asp:RequiredFieldValidator ID="rfvClmIntmDt" runat="server" ControlToValidate="txtClmIntmDt"
                    ErrorMessage="Intimation Date is required." CssClass="text-danger" Display="Dynamic" />
                <br />
            </div>

            <!-- Registration Date -->
            <div class="col-md-4">
                <asp:Label ID="lblClmRegDt" runat="server" AssociatedControlID="txtClmRegDt" Text="Registration Date:" />
                <span class="required">*</span>
                <asp:TextBox ID="txtClmRegDt" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtClmRegDt_TextChanged" />
                <asp:RequiredFieldValidator ID="rfvClmRegDt" runat="server" ControlToValidate="txtClmRegDt"
                    ErrorMessage="Registration Date is required." CssClass="text-danger" Display="Dynamic" />
                <br />
            </div>
        </div>
                

        <div class="row">
            <!-- Police Report No -->
            <div class="col-md-4">
                <asp:Label ID="lblClmPolRepNo" runat="server" AssociatedControlID="txtClmPolRepNo" Text="Police Report No:" />
                <span class="required">*</span>
                <asp:TextBox ID="txtClmPolRepNo" runat="server" CssClass="form-control" MaxLength="10" OnTextChanged="txtClmPolRepNo_TextChanged" AutoPostBack="true" />
                <asp:RequiredFieldValidator ID="rfvClmPolRepNo" runat="server" ControlToValidate="txtClmPolRepNo"
                    ErrorMessage="Policy Report No is required." CssClass="text-danger" Display="Dynamic" />
                <br />
            </div>

            <!-- Police Report Details -->
            <div class="col-md-4">
                <asp:Label ID="lblClmPolRepDtl" runat="server" AssociatedControlID="txtClmPolRepDtl" Text="Police Report Details:"></asp:Label>
                <span class="required">*</span>
                <asp:TextBox ID="txtClmPolRepDtl" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="120" />
                <asp:RequiredFieldValidator ID="rfvClmPolRepDtl" runat="server" ControlToValidate="txtClmPolRepDtl"
                    ErrorMessage="Policy Report Details are required." CssClass="text-danger" Display="Dynamic" />
                <br />
            </div>

            <!-- Loss Description -->
            <div class="col-md-4">
                <asp:Label ID="lblClmLossDesc" runat="server" AssociatedControlID="txtClmLossDesc" Text="Loss Description:" />
                <span class="required">*</span>
                <asp:TextBox ID="txtClmLossDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" MaxLength="120" />
                <asp:RequiredFieldValidator ID="rfvClmLossDesc" runat="server" ControlToValidate="txtClmLossDesc"
                    ErrorMessage="Loss Description is required." CssClass="text-danger" Display="Dynamic" />
                <br />
            </div>
        </div>


        <div class="row">
            <%--<!-- Vehicle Make -->
            <div class="col-md-4">
                <asp:Label ID="lblClmVehMake" runat="server" AssociatedControlID="txtClmVehMake" Text="Vehicle Make:" />
                <asp:TextBox ID="txtClmVehMake" runat="server" CssClass="form-control" ReadOnly />
                <br />
            </div>

            <!-- Vehicle Model -->
            <div class="col-md-4">
                <asp:Label ID="lblClmVehModel" runat="server" AssociatedControlID="txtClmVehModel" Text="Vehicle Model:" />
                <asp:TextBox ID="txtClmVehModel" runat="server" CssClass="form-control" ReadOnly />
                <br />
            </div>--%>



            <div class="col-md-4">
                <asp:Label ID="lblClmVehMake" runat="server" AssociatedControlID="ddlClmVehMake" Text="Vehicle Make:" />
                <asp:DropDownList ID="ddlClmVehMake" runat="server" CssClass="form-control" Enabled="false">
                </asp:DropDownList>
                <br />
            </div>

            <!-- Vehicle Model -->
            <div class="col-md-4">
                <asp:Label ID="lblClmVehModel" runat="server" AssociatedControlID="ddlClmVehModel" Text="Vehicle Model:" />
                <asp:DropDownList ID="ddlClmVehModel" runat="server" CssClass="form-control" Enabled="false">
                </asp:DropDownList>
                <br />
            </div>



            <!-- Chassis Number -->
            <div class="col-md-4">
                <asp:Label ID="lblClmVehChassisNo" runat="server" AssociatedControlID="txtClmVehChassisNo" Text="Chassis Number:" />
                <asp:TextBox ID="txtClmVehChassisNo" runat="server" CssClass="form-control" ReadOnly="true" />
                <br />
            </div>
        </div>

        <div class="row">
            <!-- Engine Number -->
            <div class="col-md-4">
                <asp:Label ID="lblClmVehEngineNo" runat="server" AssociatedControlID="txtClmVehEngineNo" Text="Engine Number:" ReadOnly="true" />
                <asp:TextBox ID="txtClmVehEngineNo" runat="server" CssClass="form-control" ReadOnly="true" />
                <br />
            </div>

            <!-- Registration Number -->
            <div class="col-md-4">
                <asp:Label ID="lblClmVehRegnNo" runat="server" AssociatedControlID="txtClmVehRegnNo" Text="Registration Number:" ReadOnly="true" />
                <asp:TextBox ID="txtClmVehRegnNo" runat="server" CssClass="form-control" ReadOnly="true" />
                <br />
            </div>

            <!-- Vehicle Value -->
            <div class="col-md-4">
                <asp:Label ID="lblClmVehValue" runat="server" AssociatedControlID="txtClmVehValue" Text="Vehicle Value:" />
                <asp:TextBox ID="txtClmVehValue" runat="server" CssClass="form-control numberText" ReadOnly="true" />
                <br />
            </div>
        </div>

</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlClmPolNo" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtClmLossDt" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtClmPolRepNo" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <%--<div class="row">
            <!-- Survey Number -->
            <div class="col-md-4">
                <asp:Label ID="lblClmSurNo" runat="server" AssociatedControlID="txtClmSurNo" Text="Survey Number:" />
                <asp:TextBox ID="txtClmSurNo" runat="server" CssClass="form-control" ReadOnly/>
                <br />
            </div>
        </div>--%>

        <!-- Submit and Update Buttons -->
        <div class="row justify-content-center">
            <div class="col-md-4 d-flex justify-content-center">
                <asp:Button ID="btnSaveClaim" runat="server" Text="Save" CssClass="btn btn-success mx-2" OnClick="btnSaveClaim_Click" />
                <asp:Button ID="btnUpdateClaim" runat="server" Text="Update" CssClass="btn btn-success mx-2" OnClick="btnUpdateClaim_Click" />
                <asp:Button ID="btnSurvey" runat="server" Text="Intimate to Survey" CssClass="btn btn-success mx-2" OnClick="btnSurvey_Click" />
                <asp:Button ID="btnViewSurvey" runat="server" Text="Survey" CssClass="btn btn-success mx-2" OnClick="btnViewSurvey_Click" Visible="false" />
                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success mx-2" OnClick="btnApprove_Click" Visible="false" />
                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-success mx-2" OnClick="btnReject_Click" Visible="false" />

            </div>
        </div>
    </div>
</asp:Content>
