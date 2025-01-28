<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="SurveyListing.aspx.cs" Inherits="MotorSurveySystem.Transaction.Survey.SurveyListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../Style/CodeMasterListingstyleSheet.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
<%--    <link href="../../Style/SurveyListing.css" rel="stylesheet" />--%>
        
    <style>
        .text-right {
            text-align: right;
            display: block;
        }
    </style>
    <div class="table-container">
        <div>
            <h3>SURVEY LISTING</h3>
            
        </div>


        <!-- Survey Listing Grid -->
        <asp:GridView ID="gvSurveyListing" runat="server" CssClass="table table-bordered table-striped" Width="650px" AutoGenerateColumns="False" AllowPaging="true" PageSize="8" OnPageIndexChanging="gvSurveyListing_PageIndexChanging">
            <Columns>

                <asp:TemplateField HeaderText="Survey UID" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblSurveyUid" runat="server" Text='<%# Eval("SUR_UID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <%-- <asp:TemplateField HeaderText="Claim UID">
                    <ItemTemplate>
                        <asp:Label ID="lblClaimUid" runat="server" Text='<%# Eval("SUR_CLM_UID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Survey No">
                    <ItemTemplate>
                        <asp:Label ID="lblSurNo" runat="server" Text='<%# Eval("SUR_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Claim No">
                    <ItemTemplate>
                        <asp:Label ID="lblClmNo" runat="server" Text='<%# Eval("SUR_CLM_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Survey Date">
                    <ItemTemplate>
                        <asp:Label ID="lblSurDt" runat="server" Text='<%# Eval("SUR_DATE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Survey Location">
                    <ItemTemplate>
                        <asp:Label ID="lblSurLocation" runat="server" Text='<%# Eval("SUR_LOCATION") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Chassis No">
                    <ItemTemplate>
                        <asp:Label ID="lblChassisNo" runat="server" Text='<%# Eval("SUR_CHASSIS_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Registration No">
                    <ItemTemplate>
                        <asp:Label ID="lblRegnNo" runat="server" Text='<%# Eval("SUR_REGN_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <%-- <asp:TemplateField HeaderText="Engine No">
                    <ItemTemplate>
                        <asp:Label ID="lblEngineNo" runat="server" Text='<%# Eval("SUR_ENGINE_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>


                <%--                <asp:TemplateField HeaderText="Currency">
                    <ItemTemplate>
                        <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("SUR_CURR") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>


                <asp:TemplateField HeaderText="FC Amount">
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label ID="lblFCAmount" runat="server" Text='<%# Eval("SUR_FC_AMT", "{0:N2}") %>' CssClass="text-right"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="LC Amount">
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label ID="lblLCAmount" runat="server" Text='<%# Eval("SUR_LC_AMT", "{0:N2}") %>' CssClass="text-right"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("SUR_STATUS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Approval Status">
                    <ItemTemplate>
                        <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Eval("SUR_APPR_STS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <%-- <asp:TemplateField HeaderText="Approval Date">
                    <ItemTemplate>
                        <asp:Label ID="lblApprovalDate" runat="server" Text='<%# Eval("SUR_APPR_DT", "{0:yyyy-MM-dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Approved By">
                    <ItemTemplate>
                        <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Eval("SUR_CR_BY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Created By">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("SUR_CR_BY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Created Date">
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("SUR_CR_DT", "{0:yyyy-MM-dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Updated By">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdatedBy" runat="server" Text='<%# Eval("SUR_UP_BY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Updated Date">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdatedDate" runat="server" Text='<%# Eval("SUR_UP_DT", "{0:yyyy-MM-dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <!-- Edit Button -->
                        <asp:LinkButton ID="btnEditSurvey" runat="server" CommandName="Edit" CssClass="btn btn-link" OnClick="btnEditSurvey_Click1"> <%--Visible='<%# Eval("CLM_SUR_CR_YN").ToString()=="N" %>'--%>
                    <i class="fas fa-edit" title="Edit"></i> <!-- Edit icon -->
                        </asp:LinkButton>

                        <!-- View Button -->
                        <%--<asp:LinkButton ID="btnViewClaim" runat="server" Visible='<%# Eval("CLM_SUR_CR_YN").ToString()=="Y" %>' CommandName="View" CssClass="btn btn-link" OnClick="btnViewClaim_Click">
                    <i class="fas fa-eye" title="View"></i> <!-- View icon -->
                </asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
