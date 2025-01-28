<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="UserMasterListing.aspx.cs" Inherits="MotorSurveySystem.Master.UserMasterListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<link href="../Style/UserMasterListing.css" rel="stylesheet" />--%>
        <link href="../Style/CodeMasterListingstyleSheet.css" rel="stylesheet" />
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
     
   
     
     <div class="table-container">
         <div class="d-flex justify-content-between align-items-start">
        <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="btn btn-success" onclick="backbtn_Click"/>
         <h3>USER MASTER LISTING</h3>
        <asp:Button ID="AddNewBtn" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="AddNewBtn_Click" />
             </div>
 
    <asp:GridView ID="gvUserMaster" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" AllowPaging="true" PageSize="8" OnPageIndexChanging="gvUserMaster_PageIndexChanging">
    <Columns>
        
        <asp:TemplateField HeaderText="USER ID">
            <ItemTemplate>
                <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("USER_ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

       
        <asp:TemplateField HeaderText="USER NAME">
            <ItemTemplate>
                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("USER_NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

       <%-- <asp:TemplateField HeaderText="PASSWORD">
            <ItemTemplate>
                <asp:Label ID="lblUserPassword" runat="server" Text='<%# Eval("USER_PASSWORD") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

        <asp:TemplateField HeaderText="TYPE">
            <ItemTemplate>
                <asp:Label ID="lblUserType" runat="server" Text='<%# Eval("USER_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

       <%-- <!-- Created By -->
        <asp:TemplateField HeaderText="Created By">
            <ItemTemplate>
                <asp:Label ID="lblUserCrBy" runat="server" Text='<%# Eval("USER_CR_BY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <!-- Created Date -->
        <asp:TemplateField HeaderText="Created Date">
            <ItemTemplate>
                <asp:Label ID="lblUserCrDt" runat="server" Text='<%# Eval("USER_CR_DT", "{0:yyyy-MM-dd}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <!-- Updated By -->
        <asp:TemplateField HeaderText="Updated By">
            <ItemTemplate>
                <asp:Label ID="lblUserUpBy" runat="server" Text='<%# Eval("USER_UP_BY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <!-- Updated Date -->
        <asp:TemplateField HeaderText="Updated Date">
            <ItemTemplate>
                <asp:Label ID="lblUserUpDt" runat="server" Text='<%# Eval("USER_UP_DT", "{0:yyyy-MM-dd}") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

        <asp:TemplateField HeaderText="ACTIVE STATUS">
            <ItemTemplate>
                <asp:Label ID="lblUserActiveYn" runat="server" Text='<%# Eval("USER_ACTIVE_YN").ToString() == "Y" ? "Yes" : "No"  %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ACTION">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-link" OnClick="btnEdit_Click" >
                        <i class="fas fa-edit" title="Edit"></i>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-link" OnClientClick="return confirm('Are you sure you want to delete this record?'); " OnClick="btnDelete_Click">
                        <i class="fas fa-trash" title="Delete"></i>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>
</asp:GridView>
            </div>


</asp:Content>
