<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="PolicyListing.aspx.cs" Inherits="MotorSurveySystem.Transaction.PolicyListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>            
    </style>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <link href="../Style/PolicyListing.css" rel="stylesheet" />

    <div class="table-container">
        <div class="d-flex justify-content-between align-items-start">
        <asp:Button ID="Back" runat="server" OnClick="Back_Click" Text="Back" CssClass="btn btn-success back" />
            <h3>POLICY LISTING</h3>
        <asp:Button ID="AddNewBtn" runat="server" OnClick="AddNewBtn_Click" Text="Add New" CssClass="btn btn-success" />
        </div>
        


        <asp:GridView ID="gvPolicyListing" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8" OnPageIndexChanging="gvPolicyListing_PageIndexChanging" CssClass="table table-striped table-bordered">
            <Columns>

                <asp:TemplateField HeaderText="Policy UID" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPoluid" Text='<%# Eval("POL_UID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Policy No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPolNo" Text='<%# Eval("POL_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Issue Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPolIssDt" Text='<%# Eval("POL_ISS_DT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="From Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPolFmDt" Text='<%# Eval("POL_FM_DT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="To Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPolToDt" Text='<%# Eval("POL_TO_DT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Assured Name">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPolAssrName" CssClass="grid-column-limit" Text='<%# Eval("POL_ASSR_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%-- <asp:TemplateField HeaderText="Assured Mobile">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblPolAssrMobile" Text='<%# Eval("POL_ASSR_MOBILE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Currency Code">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblPolCurrCode" Text='<%# Eval("POL_CURR_CODE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Gross FC Premium">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblPolGrossFCPrem" Text='<%# Eval("POL_GROSS_FC_PREM") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Gross LC Premium">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblPolGrossLCPrem" Text='<%# Eval("POL_GROSS_LC_PREM") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Vehicle Make">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPolVehMake" Text='<%# Eval("POL_VEH_MAKE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Vehicle Model">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPolVehModel" Text='<%# Eval("POL_VEH_MODEL") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Chassis No">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblPolVehChassisNo" Text='<%# Eval("POL_VEH_CHASSIS_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Engine No">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblPolVehEngineNo" Text='<%# Eval("POL_VEH_ENGINE_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Regn No">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblPolVehRegnNo" Text='<%# Eval("POL_VEH_REGN_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Vehicle Value">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblPolVehValue" Text='<%# Eval("POL_VEH_VALUE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Approved Status">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblapprovedStatus" Text='<%# Eval("POL_APPR_STATUS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <!-- Edit Button -->
                        <asp:LinkButton ID="btnEdit" runat="server" Visible='<%# Eval("POL_APPR_STATUS").ToString()=="Not Approved" %>' CommandName="Edit" CssClass="btn btn-link" OnClick="btnEdit_Click" Style="display: inline-block;">
            <i class="fas fa-edit" title="Edit"></i> <!-- Edit icon -->
                        </asp:LinkButton>

                        <!-- View Button -->
                        <asp:LinkButton ID="btnView" runat="server" OnClick="btnView_Click" Visible='<%# Eval("POL_APPR_STATUS").ToString()=="Approved" %>' CommandName="View" CssClass="btn btn-link" Style="display: inline-block;">
            <i class="fas fa-eye" title="View" ></i> <!-- View icon -->
                        </asp:LinkButton>

                        <%--<asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-link" 
                    OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');">
                    <i class="fas fa-trash" title="Delete"></i> <!-- Delete icon -->
                </asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
