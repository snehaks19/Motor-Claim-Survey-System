using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem.Transaction.Claim
{
    public partial class Claim : System.Web.UI.Page
    {
        MotorClaim objMotorClaim = new MotorClaim();
        MotorClaimManager objMotorClaimManager = new MotorClaimManager();
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

                    LoadDropdown();

                    txtClmIntmDt.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtClmRegDt.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    
                    if (Request.QueryString["clmUid"] != null)
                    {
                        FetchValues();
                    }
                    else
                    {
                        AddNewClaim();
                    }
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void AddNewClaim()
        {
            try
            {
                btnSaveClaim.Visible = true;
                btnUpdateClaim.Visible = false;
                btnSurvey.Visible = false;

                txtClmNo.Enabled = false;
                txtClmPolFmDt.Enabled = false;
                txtClmPolToDt.Enabled = false;
                txtClmPolAssrName.Enabled = false;
                txtClmPolAssrMob.Enabled = false;
                txtClmVehChassisNo.Enabled = false;
                txtClmVehEngineNo.Enabled = false;
                txtClmVehRegnNo.Enabled = false;
                txtClmVehValue.Enabled = false;
     
               

                ddlClmVehModel.Enabled = false;
                ddlClmVehMake.Enabled = false;

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void LoadDropdown()
        {
            try
            {
                ddlClmPolNo.DataSource = objMotorClaimManager.returnDropDown();
                ddlClmPolNo.DataTextField = "POL_NO";
                ddlClmPolNo.DataValueField = "POL_NO";
                ddlClmPolNo.DataBind();
                ddlClmPolNo.Items.Insert(0, new ListItem("--SELECT--", ""));
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void ddlClmPolNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var polNo = ddlClmPolNo.SelectedValue;
                DataTable dt = objMotorClaimManager.LoadPolicyDetails(polNo);

                txtClmPolFmDt.Text = Convert.ToDateTime(dt.Rows[0]["POL_FM_DT"]).ToString("yyyy-MM-dd");
                txtClmPolToDt.Text = Convert.ToDateTime(dt.Rows[0]["POL_TO_DT"]).ToString("yyyy-MM-dd");
                txtClmPolAssrName.Text = dt.Rows[0]["POL_ASSR_NAME"].ToString();
                txtClmPolAssrMob.Text = dt.Rows[0]["POL_ASSR_MOBILE"].ToString();

                ddlClmVehMake.DataSource = objMotorClaimManager.returndropdown("VEH_MAKE");
                ddlClmVehMake.DataTextField = "TEXT";
                ddlClmVehMake.DataValueField = "CODE";
                ddlClmVehMake.DataBind();
                ddlClmVehMake.SelectedValue = dt.Rows[0]["POL_VEH_MAKE"].ToString();

                ddlClmVehModel.DataSource = objMotorClaimManager.returndropdown("VEH_MODEL", ddlClmVehMake.SelectedValue);
                ddlClmVehModel.DataTextField = "TEXT";
                ddlClmVehModel.DataValueField = "CODE";
                ddlClmVehModel.DataBind();
                ddlClmVehModel.SelectedValue = dt.Rows[0]["POL_VEH_MODEL"].ToString();

                txtClmVehChassisNo.Text = dt.Rows[0]["POL_VEH_CHASSIS_NO"].ToString();
                txtClmVehEngineNo.Text = dt.Rows[0]["POL_VEH_ENGINE_NO"].ToString();
                txtClmVehRegnNo.Text = dt.Rows[0]["POL_VEH_REGN_NO"].ToString();
                txtClmVehValue.Text = dt.Rows[0]["POL_VEH_VALUE"].ToString();
            }
            catch (Exception err)
            {

                throw err;
            }

                   
        }

        protected void txtClmLossDt_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (ddlClmPolNo.SelectedIndex != 0)
                {
                    DateTime fromDate, toDate, lossDate;
                    fromDate = Convert.ToDateTime(txtClmPolFmDt.Text);
                    toDate = Convert.ToDateTime(txtClmPolToDt.Text);
                    if (DateTime.TryParse(txtClmLossDt.Text, out DateTime parsedDate))
                    {
                        lossDate = Convert.ToDateTime(txtClmLossDt.Text);

                        if (lossDate >= fromDate && lossDate <= toDate)
                        {

                        }
                        else
                        {
                            txtClmLossDt.Text = string.Empty;
                            string message = objErrorCodeMasterManager.GetErrorMsg("9000");
                            string script = $"swal('{message}', '', 'error');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                        }
                    }
                    else
                    {
                        txtClmLossDt.Text = string.Empty;
                    }
                }
                else
                {
                    txtClmLossDt.Text = string.Empty;
                    string message = objErrorCodeMasterManager.GetErrorMsg("9002");
                    string script = $"swal('{message}', '', 'error');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
                
               
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void txtClmIntmDt_TextChanged(object sender, EventArgs e)
        {
            DateTime intimationDate;
            DateTime? lossDate = string.IsNullOrEmpty(txtClmLossDt.Text) ? (DateTime?)null : Convert.ToDateTime(txtClmLossDt.Text);
            if (lossDate != null)
            {
                lossDate = Convert.ToDateTime(txtClmLossDt.Text);

                if (DateTime.TryParse(txtClmIntmDt.Text, out DateTime parsedDate))
                {

                    intimationDate = Convert.ToDateTime(txtClmIntmDt.Text);


                    if (intimationDate < lossDate)
                    {
                        txtClmIntmDt.Text = string.Empty;
                        string message = objErrorCodeMasterManager.GetErrorMsg("9003");
                        string script = $"swal('{message}', '', 'error');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    }
                    else if (intimationDate > lossDate)
                    {
                        txtClmIntmDt.Text = string.Empty;
                        string message = objErrorCodeMasterManager.GetErrorMsg("9004");
                        string script = $"swal('{message}', '', 'error');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    }
                    else
                    {

                    }

                }
                else
                {
                    txtClmIntmDt.Text = string.Empty;
                }
            }
            else
            {
                txtClmIntmDt.Text = string.Empty;
                string message = objErrorCodeMasterManager.GetErrorMsg("9005");
                string script = $"swal('{message}', '', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }
        protected void txtClmRegDt_TextChanged(object sender, EventArgs e)
        {
            DateTime regDate;
            DateTime? lossDate = string.IsNullOrEmpty(txtClmLossDt.Text) ? (DateTime?)null : Convert.ToDateTime(txtClmLossDt.Text);
            

            if (lossDate != null)
            {
                if (DateTime.TryParse(txtClmRegDt.Text, out DateTime parsedDate))
                {
                    regDate = Convert.ToDateTime(txtClmRegDt.Text);
                    if (regDate < lossDate)
                    {
                        txtClmRegDt.Text = string.Empty;
                        string message = objErrorCodeMasterManager.GetErrorMsg("9006");
                        string script = $"swal('{message}', '', 'error');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    }
                    else if (regDate > lossDate)
                    {
                        txtClmRegDt.Text = string.Empty;
                        string message = objErrorCodeMasterManager.GetErrorMsg("9007");
                        string script = $"swal('{message}', '', 'error');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    }
                    else
                    {
                        //  code
                    }
                }
                else
                {
                    txtClmRegDt.Text = string.Empty;
                }
            }
            else
            {
                txtClmRegDt.Text = string.Empty;
                string message = objErrorCodeMasterManager.GetErrorMsg("9008");
                string script = $"swal('{message}', '', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }

        protected void btnSaveClaim_Click(object sender, EventArgs e)
        {

            try
            {
                objMotorClaim.ClmPolNo = ddlClmPolNo.SelectedValue;
                objMotorClaim.ClmPolFmDt = Convert.ToDateTime(txtClmPolFmDt.Text);
                objMotorClaim.ClmPolToDt = Convert.ToDateTime(txtClmPolToDt.Text);
                objMotorClaim.ClmPolAssrName = txtClmPolAssrName.Text;
                objMotorClaim.ClmPolAssrMob = txtClmPolAssrMob.Text;
                objMotorClaim.ClmLossDt = Convert.ToDateTime(txtClmLossDt.Text);
                objMotorClaim.ClmIntmDt = Convert.ToDateTime(txtClmIntmDt.Text);
                objMotorClaim.ClmRegDt = Convert.ToDateTime(txtClmRegDt.Text);
                objMotorClaim.ClmPolRepNo = txtClmPolRepNo.Text;
                objMotorClaim.ClmPolRepDtl = txtClmPolRepDtl.Text;
                objMotorClaim.ClmLossDesc = txtClmLossDesc.Text;
                objMotorClaim.ClmVehMake = ddlClmVehMake.SelectedValue;
                objMotorClaim.ClmVehModel = ddlClmVehModel.SelectedValue;
                objMotorClaim.ClmVehChassisNo = txtClmVehChassisNo.Text;
                objMotorClaim.ClmVehEngineNo = txtClmVehEngineNo.Text;
                objMotorClaim.ClmVehRegnNo = txtClmVehRegnNo.Text;
                objMotorClaim.ClmVehValue = Convert.ToDouble(txtClmVehValue.Text);
                objMotorClaim.ClmCrBy = Session["ID"].ToString();
                objMotorClaim.ClmCrDt = DateTime.Now;

                hfClmUid.Value = objMotorClaimManager.AddClaim(objMotorClaim).ToString();

                if (hfClmUid.Value != "0")
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                      "setTimeout(function(){ window.location.href='Claim.aspx?clmUid=" + hfClmUid.Value + "'; }, 1000);",
                      true);
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

       
        protected void FetchValues()
        {
            try
            {
                btnSaveClaim.Visible = false;
                btnUpdateClaim.Visible = true;

                txtClmNo.Enabled = false;
                txtClmPolFmDt.Enabled = false;
                txtClmPolToDt.Enabled = false;
                txtClmPolAssrName.Enabled = false;
                txtClmPolAssrMob.Enabled = false;
                txtClmVehChassisNo.Enabled = false;
                txtClmVehEngineNo.Enabled = false;
                txtClmVehRegnNo.Enabled = false;
                txtClmVehValue.Enabled = false;

                ddlClmVehModel.Enabled = false;
                ddlClmVehMake.Enabled = false;


                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                int claimUid = Convert.ToInt32(Request.QueryString["clmUid"]);

                DataTable dt = objMotorClaimManager.LoadDetails(claimUid);
                txtClmNo.Text = dt.Rows[0]["CLM_NO"].ToString();
                ddlClmPolNo.SelectedValue = dt.Rows[0]["CLM_POL_NO"].ToString();
                txtClmPolFmDt.Text = Convert.ToDateTime(dt.Rows[0]["CLM_POL_FM_DT"]).ToString("yyyy-MM-dd");
                txtClmPolToDt.Text = Convert.ToDateTime(dt.Rows[0]["CLM_POL_TO_DT"]).ToString("yyyy-MM-dd");
                txtClmPolAssrName.Text = dt.Rows[0]["CLM_POL_ASSR_NAME"].ToString();
                txtClmPolAssrMob.Text = dt.Rows[0]["CLM_POL_ASSR_MOB"].ToString();
                txtClmLossDt.Text = Convert.ToDateTime(dt.Rows[0]["CLM_LOSS_DT"]).ToString("dd-MM-yyyy");
                txtClmIntmDt.Text = Convert.ToDateTime(dt.Rows[0]["CLM_INTM_DT"]).ToString("dd-MM-yyyy");
                txtClmRegDt.Text = Convert.ToDateTime(dt.Rows[0]["CLM_REG_DT"]).ToString("dd-MM-yyyy");
                txtClmPolRepNo.Text = dt.Rows[0]["CLM_POL_REP_NO"].ToString();
                txtClmPolRepDtl.Text = dt.Rows[0]["CLM_POL_REP_DTL"].ToString();
                txtClmLossDesc.Text = dt.Rows[0]["CLM_LOSS_DESC"].ToString();

                ddlClmVehMake.DataSource = objMotorClaimManager.returndropdown("VEH_MAKE");
                ddlClmVehMake.DataTextField = "TEXT";
                ddlClmVehMake.DataValueField = "CODE";
                ddlClmVehMake.DataBind();
                ddlClmVehMake.SelectedValue = dt.Rows[0]["CLM_VEH_MAKE"].ToString();

                ddlClmVehModel.DataSource = objMotorClaimManager.returndropdown("VEH_MODEL", ddlClmVehMake.SelectedValue);
                ddlClmVehModel.DataTextField = "TEXT";
                ddlClmVehModel.DataValueField = "CODE";
                ddlClmVehModel.DataBind();
                ddlClmVehModel.SelectedValue = dt.Rows[0]["CLM_VEH_MODEL"].ToString();

                txtClmVehChassisNo.Text = dt.Rows[0]["CLM_VEH_CHASSIS_NO"].ToString();
                txtClmVehEngineNo.Text = dt.Rows[0]["CLM_VEG_ENGINE_NO"].ToString();
                txtClmVehRegnNo.Text = dt.Rows[0]["CLM_VEH_REGN_NO"].ToString();
                txtClmVehValue.Text = dt.Rows[0]["CLM_VEH_VALUE"].ToString();

                if (dt.Rows[0]["CLM_SUR_NO"].ToString() != null)
                {
                    lblSurveyNo.Text = dt.Rows[0]["CLM_SUR_NO"].ToString();                   
                }

                string status = dt.Rows[0]["CLM_SUR_CR_YN"].ToString();

                if (status == "Y")
                {
                    btnSurvey.Visible = false;
                    btnUpdateClaim.Visible = false;
                    //AddNewBtn.Visible = false;
                    ddlClmPolNo.Enabled = false;
                    ddlClmPolNo.Enabled = false;
                    txtClmLossDt.Enabled = false;
                    txtClmIntmDt.Enabled = false;
                    txtClmRegDt.Enabled = false;
                    txtClmPolRepNo.Enabled = false;
                    txtClmPolRepDtl.Enabled = false;
                    txtClmLossDesc.Enabled = false;
                    ddlClmVehMake.Enabled = false;
                    ddlClmVehModel.Enabled = false;
                }

                string surveyStatus = objMotorClaimManager.FindSurveyStatus(txtClmNo.Text);
                if (surveyStatus == "S")
                {
                    btnApprove.Visible = true;
                    btnViewSurvey.Visible = true;
                    btnReject.Visible = true;
                }

                string claimstatus = objMotorClaimManager.FindClaimStatus(txtClmNo.Text);
                if (claimstatus == "A" || claimstatus == "R")
                {
                    btnApprove.Visible = false;
                    btnViewSurvey.Visible = false;
                    btnReject.Visible = false;

                    if (claimstatus == "A")
                    {
                        lblClaimApprStatus.Visible = true;
                        lblClaimApprStatus.Text = "Claim Approved";
                        btnPrint.Visible = true;
                    }
                    else if (claimstatus == "R")
                    {
                        lblClaimRejStatus.Visible = true;
                        lblClaimRejStatus.Text = "Claim Rejected";
                    }
                }

            }
            catch (Exception err)
            {
                throw;
            }
        }

        protected void btnUpdateClaim_Click(object sender, EventArgs e)
        {
            try
            {
                MotorClaim objMotorClaim = new MotorClaim();
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();

                //ddlClmPolNo.Enabled = false;

                int clmuid = Convert.ToInt32(Request.QueryString["clmuid"]);
                objMotorClaim.ClmUid = clmuid;
                objMotorClaim.ClmNo = txtClmNo.Text;
                objMotorClaim.ClmPolNo = ddlClmPolNo.SelectedValue;
                objMotorClaim.ClmPolFmDt = Convert.ToDateTime(txtClmPolFmDt.Text);
                objMotorClaim.ClmPolToDt = Convert.ToDateTime(txtClmPolToDt.Text);
                objMotorClaim.ClmPolAssrName = txtClmPolAssrName.Text;
                objMotorClaim.ClmPolAssrMob = txtClmPolAssrMob.Text;
                objMotorClaim.ClmLossDt = Convert.ToDateTime(txtClmLossDt.Text);
                objMotorClaim.ClmIntmDt = Convert.ToDateTime(txtClmIntmDt.Text);
                objMotorClaim.ClmRegDt = Convert.ToDateTime(txtClmRegDt.Text);
                objMotorClaim.ClmPolRepNo = txtClmPolRepNo.Text;
                objMotorClaim.ClmPolRepDtl = txtClmPolRepDtl.Text;
                objMotorClaim.ClmLossDesc = txtClmLossDesc.Text;
                objMotorClaim.ClmVehMake = ddlClmVehMake.SelectedValue;
                objMotorClaim.ClmVehModel = ddlClmVehModel.SelectedValue;
                objMotorClaim.ClmVehChassisNo = txtClmVehChassisNo.Text;
                objMotorClaim.ClmVehEngineNo = txtClmVehEngineNo.Text;
                objMotorClaim.ClmVehRegnNo = txtClmVehRegnNo.Text;
                objMotorClaim.ClmVehValue = Convert.ToDouble(txtClmVehValue.Text);
                objMotorClaim.ClmUpBy = Session["ID"].ToString();
                objMotorClaim.ClmUpDt = DateTime.Now;

                int rowsAffected = objMotorClaimManager.UpdateClaim(objMotorClaim);
                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2001");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                    "setTimeout(function(){ window.location.href='Claim.aspx?clmUid=" + clmuid + "'; }, 1000);",
                    true);
                }
                else
                {
                    string script = @"
                swal({
                    title: 'Submission Failed!',
                    text: 'Invalid credentials, please try again.',
                    icon: 'error',
                    buttons: {
                        confirm: {
                            text: 'Try Again',
                            value: true
                        }
                    }
                });";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void btnSurvey_Click(object sender, EventArgs e)
        {
            try
            {

                long clmUid = Convert.ToInt64(Request.QueryString["clmUid"]);
                string userId = Session["ID"].ToString();

                (int status, string message) = objMotorClaimManager.SurveyRequest(clmUid, userId);

                if (status == 1 && message == "Successfully send for intimaion")
                {
                    string msg = objErrorCodeMasterManager.GetErrorMsg("2003");
                    string script = $"swal('{msg}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
              "setTimeout(function(){ window.location.href='Claim.aspx?clmUid=" + clmUid + "'; }, 1000);",
              true);
                }
                else
                {
                    //  code
                }

            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimListing.aspx");
        }

        protected void AddNewBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Claim.aspx");
        }


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                long clmUid = Convert.ToInt64(Request.QueryString["clmUid"]);
                int row = objMotorClaimManager.ClaimApproved(clmUid);
                if (row > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2005");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
              "setTimeout(function(){ window.location.href='Claim.aspx?clmUid=" + clmUid + "'; }, 1000);",
              true);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void btnViewSurvey_Click(object sender, EventArgs e)
        {
            try
            {
                string surNo = lblSurveyNo.Text;
                long surUid = objMotorClaimManager.getSurUid(surNo);
                string type = Session["UserType"].ToString();
                Response.Redirect("~/Transaction/Survey/Survey.aspx?surUid=" + surUid + "&userType=" + type);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                long clmUid = Convert.ToInt64(Request.QueryString["clmUid"]);
                int row = objMotorClaimManager.ClaimRejected(clmUid);
                if (row > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2006");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirectReject",
                        "setTimeout(function(){ window.location.href='Claim.aspx?clmUid=" + clmUid + "'; }, 1000);",
                        true);
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                MotorPolicyManager objMotorPolicyManager = new MotorPolicyManager();
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();
                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
                MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();

                long clmUid = Convert.ToInt64(Request.QueryString["clmUid"]);
                int polUid = objMotorPolicyManager.fetchUid(ddlClmPolNo.SelectedValue);
                long surUid = objMotorClaimManager.getSurUid(lblSurveyNo.Text);
                //long surdUid = objMotorClmSurDtlManager.getSurdUid(surUid);

                //Response.Redirect("~/Report/SurveyReport.aspx?clmUid="+clmUid+"&polUid="+polUid+ "&surUid=" + surUid);
                string url = $"/Report/SurveyReport.aspx?clmUid=" + clmUid + "&polUid=" + polUid + "&surUid=" + surUid;
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenNewTab", $"window.open('{url}', '_blank');", true);

            }
            catch (Exception err)
            {

                throw err;
            }

        }

        protected void txtClmPolRepNo_TextChanged(object sender, EventArgs e)
        {
            
            string clmUid = Request.QueryString["clmUid"];
            string reptNo = txtClmPolRepNo.Text.ToString().Trim();

            if (clmUid != null)
            {

                string oldNo = objMotorClaimManager.GetReportNo(clmUid);
                if (reptNo == oldNo)
                {
                    return;
                }
                else
                {

                    int rows = objMotorClaimManager.CheckDuplicate(reptNo);

                    if (rows > 0)
                    {
                        string message = objErrorCodeMasterManager.GetErrorMsg("9010");
                        string script = $"swal('{message}', '', 'warning');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                        txtClmPolRepNo.Text = string.Empty;
                    }
                }
                
            }
            else
            {

                int rows = objMotorClaimManager.CheckDuplicate(reptNo);

                if (rows > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("9010");
                    string script = $"swal('{message}', '', 'warning');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    txtClmPolRepNo.Text = string.Empty;
                }
            }
            
            
            
            
            
        }
    }
}