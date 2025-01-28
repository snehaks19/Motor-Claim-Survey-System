<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" UnobtrusiveValidationMode="None" AutoEventWireup="true" CodeBehind="SurveyDetails.aspx.cs" Inherits="MotorSurveySystem.Transaction.SurveyDetails.SurveyDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../Style/SurveyDetails.css" rel="stylesheet" />
    <script src="../../Script/Survey.js"></script>
    <style>
        .numberText,.right{
            text-align:right;
        }
        .heads address{
            float:right;
        }
       
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script src="../../Script/SurveyListing.js"></script>
    <script src="../../Script/Survey.js"></script>
    <div class="heads">
        <div class="d-flex justify-content-between align-items-start">
        <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="btn btn-success back" OnClick="backbtn_Click" CausesValidation="false"/>
        <asp:Button ID="AddNewBtn" runat="server" Text="Add New" CssClass="btn btn-success add" OnClick="AddNewBtn_Click" CausesValidation="false"/>
      </div>
        <h3>SURVEY DETAILS</h3>
        
    </div>
    <asp:ScriptManager ID="ScriptManager3" runat="server" />
    <div class="container mt-4">
        <asp:UpdatePanel ID="updSurveyDetails" runat="server">
            <ContentTemplate>

        <div class="row">
            <!-- Item Code -->
            <div class="col-md-4">
                <asp:Label ID="lblItemCode" runat="server" AssociatedControlID="ddlItemCode" Text="Item :" />
                <span class="required">*</span>
                <asp:DropDownList ID="ddlItemCode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged" AutoPostBack="true">
                   
                </asp:DropDownList>
               

                <!-- Required Field Validator -->
                <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" 
                    ControlToValidate="ddlItemCode" 
                    InitialValue="" 
                    ErrorMessage="Please select an item code." 
                    CssClass="text-danger" 
                    Display="Dynamic" />
                 <br />
            </div>


            <!-- Type -->
            <div class="col-md-4">
                <asp:Label ID="lblType" runat="server" AssociatedControlID="ddlType" Text="Type:" />
                <span class="required">*</span>
                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                    
                </asp:DropDownList>
                <asp:RequiredFieldValidator 
                    ID="rfvType" 
                    runat="server" 
                    ControlToValidate="ddlType" 
                    ErrorMessage="Type is required." 
                    ForeColor="Red" 
                    Display="Dynamic" 
                    InitialValue="" />
                 <br />
            </div>

            <!-- Unit Price -->
            <div class="col-md-4">
                <asp:Label ID="lblUnitPrice" runat="server" AssociatedControlID="txtUnitPrice" Text="Unit Price:"/>
                <span class="required">*</span>
                <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control numberText right"  AutoPostBack="true" onkeypress="validateNumericOnly(event)"   OnTextChanged="txtUnitPrice_TextChanged" MaxLength="7" ClientIDMode="Static"/>
                <asp:RequiredFieldValidator 
                    ID="rfvUnitPrice" 
                    runat="server" 
                    ControlToValidate="txtUnitPrice" 
                    ErrorMessage="Unit Price is required." 
                    ForeColor="Red" 
                    Display="Dynamic" 
                    InitialValue="" />
                <br />
            </div>
        </div>

        <div class="row">
            <!-- Quantity -->
            <div class="col-md-4">
                <asp:Label ID="lblQuantity" runat="server" AssociatedControlID="txtQuantity" Text="Quantity:" />
                <span class="required">*</span>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control right" onkeypress="validateNumericOnly(event)" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged" maxlength="3" />
                <asp:RequiredFieldValidator 
                    ID="rfvQuantity" 
                    runat="server" 
                    ControlToValidate="txtQuantity" 
                    ErrorMessage="Quantity is required." 
                    ForeColor="Red" 
                    Display="Dynamic" 
                    InitialValue="" />
                        <br />
            </div>

           

          

             <!-- LC Amount -->
            <div class="col-md-4">
                <asp:Label ID="lblLcAmt" runat="server" AssociatedControlID="txtLcAmt" Text="LC Amount:" />
                <asp:TextBox ID="txtLcAmt" runat="server" CssClass="form-control numberText"   OnTextChanged="txtLcAmt_TextChanged" AutoPostBack="true" ReadOnly="true"/>
                <br />
            </div>

            <!-- FC Amount -->
            <div class="col-md-4">
                <asp:Label ID="lblFcAmt" runat="server" AssociatedControlID="txtFcAmt" Text="FC Amount:" />
                <asp:TextBox ID="txtFcAmt" runat="server" CssClass="form-control numberText" AutoPostBack="true"  ReadOnly="true" />
                <br />
            </div>
        </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlItemCode" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtUnitPrice" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <%--<div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblRemarks" runat="server" AssociatedControlID="txtRemarks" Text="Remarks:" />
                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"/>
                <br />
            </div>
            </div>--%>

          

        <!-- Buttons -->
        <div class="row justify-content-center mt-3">
            <div class="col-md-4 d-flex justify-content-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success mx-2" OnClick="btnSave_Click"/>
                <asp:Button ID="btnHistory" runat="server" Text="History" CssClass="btn btn-success" OnClick="btnHistory_Click" Visible="false"/>
            </div>
        </div>
    </div>

    <%--History Grid--%>
    <div class="d-flex justify-content-center">
    <asp:GridView ID="gvHistory" runat="server" CssClass="table table-bordered mt-4" AutoGenerateColumns="False" AllowPaging="true" PageSize="8" OnPageIndexChanging="gvHistory_PageIndexChanging" Width="1000px">
    <Columns>
       

        <asp:TemplateField HeaderText="Item">
            <ItemTemplate>
                <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("SURDH_ITEM_CODE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Type">
            <ItemTemplate>
                <asp:Label ID="lblType" runat="server" Text='<%# Eval("SURDH_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Unit Price">
             <HeaderStyle CssClass="text-center"/>
            <ItemTemplate>
                <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("SURDH_UNIT_PRICE","{0:N2}") %>'  Style="text-align: right; display: block;"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Quantity">
             <HeaderStyle CssClass="text-center"/>
            <ItemTemplate>
                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("SURDH_QUANTITY") %>'  Style="text-align: right; display: block;"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="LC Amount">
             <HeaderStyle CssClass="text-center"/>
            <ItemTemplate>
                <asp:Label ID="lblLcAmt" runat="server" Text='<%# Eval("SURDH_LC_AMT","{0:N2}") %>'  Style="text-align: right; display: block;"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>  

        <asp:TemplateField HeaderText="FC Amount">
             <HeaderStyle CssClass="text-center"/>
            <ItemTemplate>
                <asp:Label ID="lblFcAmt" runat="server" Text='<%# Eval("SURDH_FC_AMT","{0:N2}") %>'  Style="text-align: right; display: block;"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Label ID="lblAction" runat="server" Text='<%# Eval("SURDH_ACTION") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        

        <%--<asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("SURDH_REMARKS") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

        
    </Columns>
</asp:GridView>
        </div>
</asp:Content>
