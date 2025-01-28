using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace MotorSurveySystem.Master
{
    public partial class CodeMasterEdit : System.Web.UI.Page
    {
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
                    if (Request.QueryString["cmCode"] != null && Request.QueryString["cmType"] != null)
                    {
                        EditCodeMaster();
                    }
                    else
                    {
                        AddNewCodeMaster();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void EditCodeMaster()
        {
            try
            {
                btnSave.Visible = false;
                btnUpdate.Visible = true;

                CodesMasterManager objCodesMasterManager = new CodesMasterManager();
                string cmCode = Request.QueryString["cmCode"];
                string cmType = Request.QueryString["cmType"];

                if ((!string.IsNullOrEmpty(cmCode)) && (!string.IsNullOrEmpty(cmType)))
                {

                    DataTable dt = objCodesMasterManager.LoadDetails(cmCode, cmType);

                    txtCmCode.Text = dt.Rows[0]["CM_CODE"].ToString();
                    txtCmCode.Enabled = false;
                    txtCmType.Text = dt.Rows[0]["CM_TYPE"].ToString();
                    txtCmType.Enabled = false;
                    txtCmDesc.Text = dt.Rows[0]["CM_DESC"].ToString();
                    //txtParentCode.Text = dt.Rows[0]["CM_PARENT_CODE"].ToString();
                    txtCmValue.Text = dt.Rows[0]["CM_VALUE"].ToString();
                    chkCmActiveYn.Checked = dt.Rows[0]["CM_ACTIVE_YN"].ToString() == "Y";

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private void AddNewCodeMaster()
        {
            try
            {
                btnSave.Visible = true;
                btnUpdate.Visible = false;

                txtCmCode.Text = string.Empty;
                txtCmDesc.Text = string.Empty;
                txtCmType.Text = string.Empty;
                //txtParentCode.Text = string.Empty;
                txtCmValue.Text = string.Empty;
                chkCmActiveYn.Checked = true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CodesMaster objCodesMaster = new CodesMaster();
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                objCodesMaster.CmCode = txtCmCode.Text.Trim().ToUpper();
                objCodesMaster.CmType = txtCmType.Text.Trim().ToUpper();
                objCodesMaster.CmDesc = txtCmDesc.Text.Trim();
                //objCodesMaster.CmParentCode = txtParentCode.Text;

                //objCodesMaster.CmValue = (!string.IsNullOrEmpty(txtCmValue.Text) ? Convert.ToDouble(txtCmValue.Text) : (double?)0);
                objCodesMaster.CmValue = string.IsNullOrEmpty(txtCmValue.Text) ? (double?)null : Convert.ToDouble(txtCmValue.Text);


                objCodesMaster.CmActiveYn = chkCmActiveYn.Checked ? "Y" : "N";
                objCodesMaster.CmCrBy = Session["ID"].ToString();
                objCodesMaster.CmCrDt = DateTime.Now;

                int rowsAffected = objCodesMasterManager.AddNewCodeMasterValue(objCodesMaster);

                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                 "setTimeout(function(){ window.location.href='CodeMasterEdit.aspx?cmCode=" + objCodesMaster.CmCode + "&cmType=" + objCodesMaster.CmType + "'; }, 1000);",
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
                CodesMaster objCodesMaster = new CodesMaster();
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                objCodesMaster.CmCode = txtCmCode.Text.ToUpper().Trim();
                objCodesMaster.CmType = txtCmType.Text.ToUpper().Trim();
                objCodesMaster.CmDesc = txtCmDesc.Text.Trim();
                //objCodesMaster.CmParentCode = txtParentCode.Text;

                objCodesMaster.CmValue = (!string.IsNullOrEmpty(txtCmValue.Text) ? Convert.ToDouble(txtCmValue.Text) : (double?)null);

                //objCodesMaster.CmValue = Convert.ToInt32(txtCmValue.Text);


                objCodesMaster.CmActiveYn = chkCmActiveYn.Checked ? "Y" : "N";
                objCodesMaster.CmUpBy = Session["ID"].ToString();
                objCodesMaster.CmUpDt = DateTime.Now;
                int rowsAffected = objCodesMasterManager.UpdateCodeMasterValue(objCodesMaster);

                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2001");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
            "setTimeout(function(){ window.location.href='CodeMasterEdit.aspx?cmCode=" + objCodesMaster.CmCode + "&cmType=" + objCodesMaster.CmType + "'; }, 1000);",
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CodesMasterListing.aspx");
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CodesMasterListing.aspx");
        }

        protected void txtCmCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CodesMaster objCodesMaster = new CodesMaster();
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                objCodesMaster.CmCode = (txtCmCode.Text).ToUpper().Trim();
                objCodesMaster.CmType = (txtCmType.Text).ToUpper().Trim();

                int row = objCodesMasterManager.CheckDuplicateUser(objCodesMaster);

                if (row > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("4000");
                    string script = $"swal('{message}', '', 'warning');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    txtCmCode.Text = string.Empty;
                    txtCmType.Text = string.Empty;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CodeMasterEdit.aspx");
        }
    }
}

