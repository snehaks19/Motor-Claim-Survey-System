using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem.Master
{
    public partial class UserMasterEdit : System.Web.UI.Page
    {
        UserMaster objUserMaster = new UserMaster();
        UserMasterManager objUserMasterManager = new UserMasterManager();
        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["ID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                if (!IsPostBack)
                {
                    LoadDropdowns();

                    if (Request.QueryString["userId"] != null)

                    {
                        EditUserMaster();
                    }
                    else
                    {
                        AddNewUserMaster();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void LoadDropdowns()
        {
            try
            {
                ddlUserType.DataSource = objUserMasterManager.returndropdown("USER_TYPE");

                ddlUserType.DataTextField = "TEXT";
                ddlUserType.DataValueField = "CODE";
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("--SELECT--", ""));
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void AddNewUserMaster()
        {
            try
            {
                btnSave.Visible = true;
                btnUpdate.Visible = false;

                ddlUserType.SelectedValue = string.Empty;
                txtUserId.Text = string.Empty;
                txtUserName.Text = string.Empty;
                txtUserPassword.Text = string.Empty;
                chkUserActive.Checked = true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void EditUserMaster()
        {
            try
            {
                btnSave.Visible = false;
                btnUpdate.Visible = true;

                string UserId = Request.QueryString["UserId"];
                //string UserType = Request.QueryString["UserType"];

                if ((!string.IsNullOrEmpty(UserId)))
                {

                    DataTable dt = objUserMasterManager.LoadDetails(UserId);
                    txtUserId.Text = dt.Rows[0]["USER_ID"].ToString();
                    txtUserId.Enabled = false;
                    ddlUserType.SelectedValue = dt.Rows[0]["USER_TYPE"].ToString();
                    ddlUserType.Enabled = false;
                    txtUserName.Text = dt.Rows[0]["USER_NAME"].ToString();
                    //txtUserPassword.Text = dt.Rows[0]["USER_PASSWORD"].ToString();
                    txtUserPassword.Attributes["value"] = dt.Rows[0]["USER_PASSWORD"].ToString();
                    chkUserActive.Checked = dt.Rows[0]["USER_ACTIVE_YN"].ToString() == "Y";

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserMasterEdit.aspx");
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserMasterListing.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objUserMaster.UserType = ddlUserType.SelectedValue;
                objUserMaster.UserId = txtUserId.Text;
                objUserMaster.UserName = txtUserName.Text;
                objUserMaster.UserPassword = txtUserPassword.Text;
                objUserMaster.UserCrBy = Session["ID"].ToString();
                objUserMaster.UserCrDt = DateTime.Now;
                objUserMaster.UserActiveYn= chkUserActive.Checked ? "Y" : "N";

                int rowsAffected = objUserMasterManager.AddNewUserMasterValue(objUserMaster);

                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                 "setTimeout(function(){ window.location.href='UserMasterEdit.aspx?userId=" + objUserMaster.UserId + "'; }, 1000);",
                 true);

                }
                else
                {
                    //code
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                objUserMaster.UserType = ddlUserType.SelectedValue;
                objUserMaster.UserId = txtUserId.Text;
                objUserMaster.UserName = txtUserName.Text;
                objUserMaster.UserPassword = txtUserPassword.Text;
                objUserMaster.UserActiveYn = chkUserActive.Checked ? "Y" : "N";
                objUserMaster.UserUpBy = Session["ID"].ToString();
                objUserMaster.UserUpDt = DateTime.Now;
                int rowsAffected = objUserMasterManager.UpdateUserMasterValue(objUserMaster);

                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2001");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
            "setTimeout(function(){ window.location.href='UserMasterEdit.aspx?userId=" + objUserMaster.UserId + "'; }, 1000);",
            true);
                }
                else
                {

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {

            try
            {
                objUserMaster.UserId = (txtUserId.Text).ToUpper().Trim();
                //objCodesMaster.CmType = (txtCmType.Text).ToUpper();

                int row = objUserMasterManager.CheckDuplicateUser(objUserMaster);

                if (row > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("4000");
                    string script = $"swal('{message}', '', 'warning');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    txtUserId.Text = string.Empty;

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}