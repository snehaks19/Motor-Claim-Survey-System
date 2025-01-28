<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="ClaimListing.aspx.cs" Inherits="MotorSurveySystem.Transaction.Claim.ClaimListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .btn-success{
            
            margin:10px;
        }
        .btn-primary{
            float:right;
            margin:10px;
        }
        .heads h3{
            text-align:center;
            color:blue;
             margin:10px;
        }
        .text{
            text-align:right;
        }
        .back{
            float:left;
            margin-left:60px;
        }
    </style>
     <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="../../Style/ClaimListing.css" rel="stylesheet" />
    <%--<div class="back">
         
    </div>--%>
    <div class="table-container">
        <div class="d-flex justify-content-between align-items-start">
        <asp:Button ID="Back" runat="server" OnClick="Back_Click" Text="Back" CssClass="btn btn-success back" />
         <h3>CLAIM LISTING</h3>
        <asp:Button ID="AddNewBtn" runat="server" OnClick="AddNewBtn_Click" Text="Add New" CssClass="btn btn-success add" />
            </div>
       
   
    <asp:GridView ID="gvClaimListing" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" Width="100%" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvClaimListing_PageIndexChanging">
    <Columns>

        <asp:TemplateField HeaderText="Claim UID" Visible="false">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmUid" Text='<%# Eval("CLM_UID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Claim No">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmNo" Text='<%# Eval("CLM_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Policy No">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmPolNo" Text='<%# Eval("CLM_POL_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="From Date">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmPolFmDt" Text='<%# Eval("CLM_POL_FM_DT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="To Date">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmPolToDt" Text='<%# Eval("CLM_POL_TO_DT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Assured Name">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmPolAssrName" Text='<%# Eval("CLM_POL_ASSR_NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

       <%-- <asp:TemplateField HeaderText="Assured Mobile">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmPolAssrMob" Text='<%# Eval("CLM_POL_ASSR_MOB") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

        <asp:TemplateField HeaderText="Loss Date">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmLossDt" Text='<%# Eval("CLM_LOSS_DT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Intimation Date">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmIntmDt" Text='<%# Eval("CLM_INTM_DT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Vehicle Make">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmVehMake" Text='<%# Eval("CLM_VEH_MAKE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Vehicle Model">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmVehModel" Text='<%# Eval("CLM_VEH_MODEL") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="Vehicle Value">
              <HeaderStyle CssClass="text-center"/>
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmVehValue" Text='<%# Eval("CLM_VEH_VALUE","{0:N2}") %>' style="text-align: right; display: block;"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Survey Created">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmSurcrYn" Text='<%# Eval("CLM_SUR_CR_YN").ToString() == "Y" ? "Yes" : "No"%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


       <%-- <asp:TemplateField HeaderText="Chassis No">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmVehChassisNo" Text='<%# Eval("CLM_VEH_CHASSIS_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Engine No">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmVegEngineNo" Text='<%# Eval("CLM_VEG_ENGINE_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

       

        <asp:TemplateField HeaderText="Approved Status">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblClmApprStatus" Text='<%# Eval("CLM_APPR_STATUS") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <!-- Edit Button -->
                <asp:LinkButton ID="btnEditClaim" runat="server"  CommandName="Edit" CssClass="btn btn-link" Visible='<%# Eval("CLM_SUR_CR_YN").ToString()=="N" %>' OnClick="btnEditClaim_Click"> 
                    <i class="fas fa-edit" title="Edit"></i> <!-- Edit icon -->
                </asp:LinkButton>

                <!-- View Button -->
                <asp:LinkButton ID="btnViewClaim" runat="server" Visible='<%# Eval("CLM_SUR_CR_YN").ToString()=="Y" %>' CommandName="View" CssClass="btn btn-link" OnClick="btnViewClaim_Click">
                    <i class="fas fa-eye" title="View"></i> <!-- View icon -->
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
         </div>
</asp:Content>
