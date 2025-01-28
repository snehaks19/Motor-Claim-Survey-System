<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="ErrorCodeMasterListing.aspx.cs" Inherits="MotorSurveySystem.Master.ErrorCodeMasterListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <link href="../Style/CodeMasterListingstyleSheet.css" rel="stylesheet" />

    <%--<link href="../Style/ErrorMasterListingStyleSheet.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <div class="table-container">
        <div class="d-flex justify-content-between align-items-start">
            <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="btn btn-success" OnClick="backbtn_Click" />
            <h3>ERROR CODE MASTER</h3>
            <asp:Button ID="AddNewBtn" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="AddNewBtn_Click" />
        </div>


        <asp:GridView ID="gvErrorCodeMaster" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvErrorCodeMaster_PageIndexChanging">
            <Columns>

                <asp:TemplateField HeaderText="ERROR CODE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrCode" Text='<%# Eval("ERR_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ERROR TYPE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrType" Text='<%# Eval("ERR_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrDesc" Text='<%# Eval("ERR_DESC") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ACTION">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-link" OnClick="btnEdit_Click">
                        <i class="fas fa-edit" title="Edit"></i>
                    </asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-link" OnClientClick="return confirm('Are you sure you want to delete this record?');" OnClick="btnDelete_Click">
                        <i class="fas fa-trash" title="Delete"></i>
                    </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
