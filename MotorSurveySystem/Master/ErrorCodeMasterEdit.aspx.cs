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
    public partial class ErrorCodeMasterEdit : System.Web.UI.Page
    {
        ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
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
                    LoadDropDowns();

                    if (Request.QueryString["errCode"] != null && Request.QueryString["errType"] != null)
                    {
                        EditErrorCodeMaster();
                    }
                    else
                    {
                        AddNewErrorCodeMaster();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void LoadDropDowns()
        {
            try
            {
                ddlErrType.DataSource = objErrorCodeMasterManager.returndropdown("ERROR_TYPE");

                ddlErrType.DataTextField = "TEXT";
                ddlErrType.DataValueField = "CODE";
                ddlErrType.DataBind();
                ddlErrType.Items.Insert(0, new ListItem("--SELECT--", ""));
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void EditErrorCodeMaster()
        {
            try
            {
                btnSave.Visible = false;
                btnUpdate.Visible = true;

                string errCode = Request.QueryString["errCode"];
                string errType = Request.QueryString["errType"];

                if ((!string.IsNullOrEmpty(errCode)) && (!string.IsNullOrEmpty(errType)))
                {

                    DataTable dt = objErrorCodeMasterManager.LoadDetails(errCode, errType);
                    txtErrCode.Text = dt.Rows[0]["ERR_CODE"].ToString();
                    txtErrCode.Enabled = false;
                    ddlErrType.SelectedValue = dt.Rows[0]["ERR_TYPE"].ToString();
                    ddlErrType.Enabled = false;
                    txtErrDesc.Text = dt.Rows[0]["ERR_DESC"].ToString();

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void AddNewErrorCodeMaster()
        {
            try
            {
                btnSave.Visible = true;
                btnUpdate.Visible = false;

                txtErrCode.Text = string.Empty;
                ddlErrType.SelectedValue = string.Empty;
                txtErrDesc.Text = string.Empty;
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ErrorCodeMasterListing.aspx");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ErrorCodeMasterEdit.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objErrorCodeMaster.ErrCode = txtErrCode.Text.ToUpper().Trim();
                objErrorCodeMaster.ErrType = ddlErrType.SelectedValue;
                objErrorCodeMaster.ErrDesc = txtErrDesc.Text.Trim();
                objErrorCodeMaster.ErrCrBy = Session["ID"].ToString();
                objErrorCodeMaster.ErrCrDt = DateTime.Now;

                int rowsAffected = objErrorCodeMasterManager.AddNewErrorCodeMasterValue(objErrorCodeMaster);

                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                 "setTimeout(function(){ window.location.href='ErrorCodeMasterEdit.aspx?errCode=" + objErrorCodeMaster.ErrCode + "&errType=" + objErrorCodeMaster.ErrType + "'; }, 1000);",
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
                objErrorCodeMaster.ErrCode = txtErrCode.Text.Trim();
                objErrorCodeMaster.ErrType = ddlErrType.SelectedValue;
                objErrorCodeMaster.ErrDesc = txtErrDesc.Text.Trim();

                objErrorCodeMaster.ErrUpBy = Session["ID"].ToString();
                objErrorCodeMaster.ErrUpDt = DateTime.Now;
                int rowsAffected = objErrorCodeMasterManager.UpdateErrorCodeMasterValue(objErrorCodeMaster);

                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2001");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
            "setTimeout(function(){ window.location.href='ErrorCodeMasterEdit.aspx?errCode=" + objErrorCodeMaster.ErrCode + "&errType=" + objErrorCodeMaster.ErrType + "'; }, 1000);",
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

        protected void txtErrCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                objErrorCodeMaster.ErrCode = (txtErrCode.Text).ToUpper().Trim();
                objErrorCodeMaster.ErrType = (ddlErrType.SelectedValue).ToUpper().Trim();

                int row = objErrorCodeMasterManager.CheckDuplicate(objErrorCodeMaster);

                if (row > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("4000");
                    string script = $"swal('{message}', '', 'warning');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    ddlErrType.Text = string.Empty;
                    txtErrCode.Text = string.Empty;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //protected void ddlErrType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ErrorCodeMaster objErrorCodeMaster = new ErrorCodeMaster();
        //    ErrorCodeMasterManager objErrorMasterManager = new ErrorCodeMasterManager();

        //    objErrorCodeMaster.ErrCode = (txtErrCode.Text).ToUpper();
        //    objErrorCodeMaster.ErrType = (ddlErrType.SelectedValue).ToUpper();

        //    int row = objErrorMasterManager.CheckDuplicate(objErrorCodeMaster);

        //    if (row > 0)
        //    {
        //        string duplicateScript = "swal('Code Already Exists!', 'The code you entered already exists in the database.', 'warning');";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "DuplicateAlert", duplicateScript, true);
        //        //txtErrCode.Text = string.Empty;
        //        ddlErrType.SelectedValue = string.Empty;

        //    }
        //}
    }
}