<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" UnobtrusiveValidationMode="None" CodeBehind="Survey.aspx.cs" Inherits="MotorSurveySystem.Transaction.Survey.Survey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .numberText {
            text-align: right;
        }

        .text-right {
            text-align: right;
        }
    </style>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="../../Style/Survey.css" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script src="../../Script/Survey.js"></script>


    <div class="heads">
        <div class="d-flex justify-content-between align-items-start">
            <asp:Button ID="Backbtn" runat="server" Text="Back" CssClass="btn btn-success back" OnClick="Backbtn_Click" CausesValidation="false" />
        </div>
        <h3>SURVEY</h3>
        
    </div>
    <div class="container mt-4">
        <asp:Label ID="lblApprove" runat="server" Text="Survey Submitted" ForeColor="Navy" Visible="False" Style="font-weight: bold" /><br />
        <asp:Label ID="lblSurNo" runat="server" Text="" Style="font-weight: bold" ForeColor="Red" />
        <div class="row">
            <!-- Intimation Date -->
            <div class="col-md-4">
                <asp:Label ID="lblIntimationDate" runat="server" AssociatedControlID="txtIntimationDate" Text="Intimation Date:" />
                <asp:TextBox ID="txtIntimationDate" runat="server" CssClass="form-control" ReadOnly="true" />
                <asp:HiddenField ID="hfuid" runat="server" Value="0" />
                <br />
            </div>

            <%--<!-- Survey No -->
        <div class="col-md-4">
            <asp:Label ID="lblSurNo" runat="server" AssociatedControlID="txtSurNo" Text="Survey No:" />
            <asp:TextBox ID="txtSurNo" runat="server" CssClass="form-control" ReadOnly />
            <br />
        </div>--%>

            <!-- Claim No -->
            <div class="col-md-4">
                <asp:Label ID="lblSurClmNo" runat="server" AssociatedControlID="txtSurClmNo" Text="Claim No:" />
                <asp:TextBox ID="txtSurClmNo" runat="server" CssClass="form-control" ReadOnly />
                <br />
            </div>

            <!-- Survey Date -->
            <div class="col-md-4">
                <asp:Label ID="lblSurDate" runat="server" AssociatedControlID="txtSurDate" Text="Survey Date:" />
                <asp:TextBox ID="txtSurDate" runat="server" CssClass="form-control" Placeholder="Enter Issue Date" ReadOnly AutoPostBack="true" />
                <asp:RequiredFieldValidator ID="rfvSurDate" runat="server" ControlToValidate="txtSurDate" ErrorMessage="Survey Date is required." CssClass="text-danger" />

                <br />
            </div>
            <%-- <div class="col-md-4">
                <asp:Label ID="lblSurDate" runat="server" AssociatedControlID="txtSurDate" Text="Survey Date:" />
                <asp:TextBox ID="txtSurDate" runat="server" CssClass="form-control" Placeholder="Enter Issue Date"  ReadOnly AutoPostBack="true"/>
                <asp:RequiredFieldValidator ID="rfvPolIssDt" runat="server" ControlToValidate="txtPolIssDt" ErrorMessage="Issue Date is required." CssClass="text-danger" />
            </div>--%>
        </div>

        <div class="row">

            <!-- Survey Location -->
            <div class="col-md-4">
                <asp:Label ID="lblSurLocation" runat="server" AssociatedControlID="ddlSurLocation" Text="Survey Location:" />
                <span class="required">*</span>
                <asp:DropDownList ID="ddlSurLocation" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Location" Value="" />

                </asp:DropDownList>
                <asp:RequiredFieldValidator
                    ID="rfvSurLocation"
                    runat="server"
                    ControlToValidate="ddlSurLocation"
                    InitialValue=""
                    ErrorMessage="Survey Location is required."
                    CssClass="text-danger"
                    Display="Dynamic"
                    ForeColor="Red"
                    SetFocusOnError="True" />
                <br />
                <br />
            </div>

            <!-- Chassis No -->
            <div class="col-md-4">
                <asp:Label ID="lblSurChassisNo" runat="server" AssociatedControlID="txtSurChassisNo" Text="Chassis No:" />
                <asp:TextBox ID="txtSurChassisNo" runat="server" CssClass="form-control" ReadOnly />
                <br />
            </div>

            <!-- Registration No -->
            <div class="col-md-4">
                <asp:Label ID="lblSurRegnNo" runat="server" AssociatedControlID="txtSurRegnNo" Text="Registration No:" />
                <asp:TextBox ID="txtSurRegnNo" runat="server" CssClass="form-control" ReadOnly />
                <br />
            </div>
        </div>



        <div class="row">


            <!-- Engine No -->
            <div class="col-md-4">
                <asp:Label ID="lblSurEngineNo" runat="server" AssociatedControlID="txtSurEngineNo" Text="Engine No:" />
                <asp:TextBox ID="txtSurEngineNo" runat="server" CssClass="form-control" ReadOnly />
                <br />
            </div>

            <!-- Currency -->
            <div class="col-md-4">
                <asp:Label ID="lblSurCurr" runat="server" AssociatedControlID="ddlSurCurr" Text="Currency:" />
                <span class="required">*</span>
                <asp:DropDownList ID="ddlSurCurr" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator
                    ID="rfvSurCurr"
                    runat="server"
                    ControlToValidate="ddlSurCurr"
                    InitialValue=""
                    ErrorMessage="Currency is required."
                    CssClass="text-danger"
                    Display="Dynamic"
                    ForeColor="Red"
                    SetFocusOnError="True" />
                <br />
                <br />
            </div>

            <!-- LC Amount -->
            <div class="col-md-4">
                <asp:Label ID="lblSurLcAmt" runat="server" AssociatedControlID="txtSurLcAmt" Text="LC Amount:" />
                <asp:TextBox ID="txtSurLcAmt" runat="server" CssClass="form-control numberText" ReadOnly />
                <br />
            </div>


        </div>

        <div class="row">
            <!-- FC Amount -->
            <div class="col-md-4">
                <asp:Label ID="lblSurFcAmt" runat="server" AssociatedControlID="txtSurFcAmt" Text="FC Amount:" />
                <asp:TextBox ID="txtSurFcAmt" runat="server" CssClass="form-control numberText" ReadOnly />
                <br />
            </div>


            <%-- <!-- Status -->
        <div class="col-md-4">
            <asp:Label ID="lblSurStatus" runat="server" AssociatedControlID="ddlSurStatus" Text="Status:" />
            <asp:DropDownList ID="ddlSurStatus" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Status" Value="" />
                <asp:ListItem Text="Pending" Value="P" />
                <asp:ListItem Text="Submitted" Value="S" />
            </asp:DropDownList>
            <br />
        </div>
    </div>

    <div class="row">
        <!-- Approval Status -->
        <div class="col-md-4">
            <asp:Label ID="lblSurApprSts" runat="server" AssociatedControlID="ddlSurApprSts" Text="Approval Status:" />
            <asp:DropDownList ID="ddlSurApprSts" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Approval Status" Value="" />
                <asp:ListItem Text="Not Approved" Value="N" />
                <asp:ListItem Text="Approved" Value="A" />
                <asp:ListItem Text="Rejected" Value="R" />
            </asp:DropDownList>
            <br />
        </div>

        <!-- Approval Date -->
        <div class="col-md-4">
            <asp:Label ID="lblSurApprDt" runat="server" AssociatedControlID="txtSurApprDt" Text="Approval Date:" />
            <asp:TextBox ID="txtSurApprDt" runat="server" CssClass="form-control" TextMode="Date" />
            <br />
        </div>

        <!-- Approved By -->
        <div class="col-md-4">
            <asp:Label ID="lblSurApprBy" runat="server" AssociatedControlID="txtSurApprBy" Text="Approved By:" />
            <asp:TextBox ID="txtSurApprBy" runat="server" CssClass="form-control" />
            <br />
        </div>
    </div>

    <div class="row">
        <!-- Created By -->
        <div class="col-md-4">
            <asp:Label ID="lblSurCrBy" runat="server" AssociatedControlID="txtSurCrBy" Text="Created By:" />
            <asp:TextBox ID="txtSurCrBy" runat="server" CssClass="form-control" />
            <br />
        </div>

        <!-- Created Date -->
        <div class="col-md-4">
            <asp:Label ID="lblSurCrDt" runat="server" AssociatedControlID="txtSurCrDt" Text="Created Date:" />
            <asp:TextBox ID="txtSurCrDt" runat="server" CssClass="form-control" TextMode="Date" />
            <br />
        </div>

        <!-- Updated By -->
        <div class="col-md-4">
            <asp:Label ID="lblSurUpBy" runat="server" AssociatedControlID="txtSurUpBy" Text="Updated By:" />
            <asp:TextBox ID="txtSurUpBy" runat="server" CssClass="form-control" />
            <br />
        </div>
    </div>

    <div class="row">
        <!-- Updated Date -->
        <div class="col-md-4">
            <asp:Label ID="lblSurUpDt" runat="server" AssociatedControlID="txtSurUpDt" Text="Updated Date:" />
            <asp:TextBox ID="txtSurUpDt" runat="server" CssClass="form-control" TextMode="Date" />
            <br />
        </div>
    </div>--%>

            <!--  Buttons -->
            <div class="row justify-content-center">
                <div class="col-md-4 d-flex justify-content-center">
                    <asp:Button ID="btnSaveSurvey" runat="server" Text="Save" CssClass="btn btn-success mx-2" OnClick="btnSaveSurvey_Click" />
                    <asp:Button ID="btnAddDetails" runat="server" Text="Add Details" CssClass="btn btn-success mx-2" OnClick="btnAddDetails_Click" Visible="false" />
                    <%--<asp:Button ID="btnUpdateSurvey" runat="server" Text="Update" CssClass="btn btn-success mx-2" OnClick="btnUpdateSurvey_Click"/>--%>
                </div>
            </div>
        </div>


        <%-- 2nd grid--%>


        <asp:GridView ID="gvSurveyDetails" OnRowCreated="gvSurveyDetails_RowCreated" OnRowDataBound="gvSurveyDetails_RowDataBound" runat="server" CssClass="table table-bordered mt-4" Visible="false" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="SURD UID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSurdUid" runat="server" Text='<%# Eval("SURD_UID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Survey UID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSurdSurUid" runat="server" Text='<%# Eval("SURD_SUR_UID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblSurdItemCode" runat="server" Text='<%# Eval("SURD_ITEM_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label ID="lblSurdType" runat="server" Text='<%# Eval("SURD_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Unit Price">
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSurdUnitPrice" runat="server" Text='<%# Eval("SURD_UNIT_PRICE","{0:N2}") %>' Style="text-align: right; display: block;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Quantity">
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSurdQuantity" runat="server" Text='<%# Eval("SURD_QUANTITY") %>' Style="text-align: right; display: block;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="LC Amount">
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSurdLcAmt" runat="server" Text='<%# Eval("SURD_LC_AMT","{0:N2}") %>' Style="text-align: right; display: block;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="FC Amount">
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSurdFcAmt" runat="server" Text='<%# Eval("SURD_FC_AMT","{0:N2}") %>' Style="text-align: right; display: block;"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <!-- Edit Button -->
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-link"
                            Style="display: inline-block;" OnClick="btnEdit_Click">
            <i class="fas fa-edit" title="Edit"></i>
                        </asp:LinkButton>

                        <!-- Delete Button -->
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-link"
                            OnClientClick="return confirm('Are you sure you want to delete this record?');"
                            OnClick="btnDelete_Click">
            <i class="fas fa-trash" title="Delete"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>




            </Columns>

        </asp:GridView>
        <div class="approve">
            <asp:Button ID="approveBtn" runat="server" Text="Submit" CssClass="btn btn-success" Visible="false" OnClick="approveBtn_Click" OnClientClick="return confirm('Are you sure you want to approve this record?');" />
        </div>
</asp:Content>
