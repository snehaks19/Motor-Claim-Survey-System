<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.Master" AutoEventWireup="true" CodeBehind="CodesMasterListing.aspx.cs" Inherits="MotorSurveySystem.Master.CodesMasterListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Style/CodeMasterListingstyleSheet.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <div class="table-container">
        <div class="d-flex justify-content-between align-items-start">
            <asp:Button ID="backbtn" runat="server" Text="Back" CssClass="btn btn-success" OnClick="backbtn_Click" />
            <h3>CODES MASTER LISTING</h3>
            <asp:Button ID="AddNewBtn" runat="server" Text="Add New" CssClass="btn btn-success" OnClick="AddNewBtn_Click" />
        </div>

        <asp:GridView ID="gvCodesMaster" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" AllowPaging="true" PageSize="8" OnPageIndexChanging="gvCodesMaster_PageIndexChanging">

            <Columns>

                <asp:TemplateField HeaderText="CODE">

                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCMCode" Text='<%# Eval("CM_CODE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCMCode" Text='<%# Bind("CM_CODE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="TYPE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCMType" Text='<%# Eval("CM_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCMType" Text='<%# Bind("CM_TYPE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCMDesc" Text='<%# Eval("CM_DESC") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCMDesc" Text='<%# Bind("CM_DESC") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="VALUE">
                    <HeaderStyle CssClass="text-center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCMValue" Text='<%# Eval("CM_VALUE") %>' Style="text-align: right; display: block;"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCMValue" Text='<%# Bind("CM_VALUE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <%-- <asp:TemplateField HeaderText="PARENT CODE">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblParentCode" Text='<%# Eval("CM_PARENT_CODE") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="txtParentCode" Text='<%# Bind("CM_PARENT_CODE") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="ACTIVE STATUS">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCMActiveYN" Text='<%# Eval("CM_ACTIVE_YN").ToString() == "Y" ? "Yes" : "No" %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCMActiveYN" Text='<%# Bind("CM_ACTIVE_YN") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ACTION">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-link" OnClick="btnEdit_Click">
                    <i class="fas fa-edit" title="Edit"></i> 
                </asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-link"
                            OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');">
                    <i class="fas fa-trash" title="Delete"></i>
                </asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
