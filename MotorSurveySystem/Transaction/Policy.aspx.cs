using BusinessLayer;
using MotorSurveySystem.WebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem.Transaction
{
    public partial class Policy : System.Web.UI.Page
    {
        MotorPolicy objMotorPolicy = new MotorPolicy();
        MotorPolicyManager objMotorPolicyManager = new MotorPolicyManager();
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
                    txtPolIssDt.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtPolIssDt.Enabled = false;
                    txtPolFmDt.Text= DateTime.Now.ToString("dd-MM-yyyy");
                    txtPolToDt.Enabled = false;
                    txtPolGrossLCPrem.Enabled = false;
                    LoadDropdown();

                    if ((Request.QueryString["polUid"]) != null)
                    {
                        FetchPolicyValues();
                    }
                    else
                    {
                        AddNewPolicy();
                    }

                }
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
                ddlPolCurrCode.DataSource = objMotorPolicyManager.returndropdown("CURRENCY");
                ddlPolCurrCode.DataTextField = "TEXT";
                ddlPolCurrCode.DataValueField = "CODE";
                ddlPolCurrCode.DataBind();
                ddlPolCurrCode.Items.Insert(0, new ListItem("--SELECT--", ""));

                ddlPolVehMake.DataSource = objMotorPolicyManager.returndropdown("VEH_MAKE");
                ddlPolVehMake.DataTextField = "TEXT";
                ddlPolVehMake.DataValueField = "CODE";
                ddlPolVehMake.DataBind();
                ddlPolVehMake.Items.Insert(0, new ListItem("--SELECT--", ""));
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        protected void AddNewPolicy()
        {

            try
            {
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                approveBtn.Visible = false;

                NewPolicytitle.Visible = true;
                PolicyDetailsTitle.Visible = false;

                //  polUid  -   sequence generated)
                txtPolNo.Text = string.Empty;
                txtPolIssDt.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtPolIssDt.ReadOnly = true;
                txtPolFmDt.Text = string.Empty;
                txtPolToDt.Text = string.Empty;
                txtPolAssrName.Text = string.Empty;
                txtPolAssrMobile.Text = string.Empty;
                ddlPolCurrCode.SelectedIndex = -1;
                txtPolGrossFCPrem.Text = string.Empty;
                txtPolGrossLCPrem.Text = string.Empty;
                ddlPolVehMake.SelectedIndex = -1;
                ddlPolVehModel.SelectedIndex = -1;
                txtPolVehChassisNo.Text = string.Empty;
                txtPolVehEngineNo.Text = string.Empty;
                txtPolVehRegnNo.Text = string.Empty;
                txtPolVehValue.Text = string.Empty;
                //  POL_APPR_STATUS
                //  POL_APPR_DT
                //  POL_APPR_BY
                //  POL_CR_DT
                //  POL_CR_BY
                //  POL_UP_BY
                //  POL_UP_BY
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void FetchPolicyValues()
        {
            try
            {
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                approveBtn.Visible = true;

                NewPolicytitle.Visible = false;
                PolicyDetailsTitle.Visible = true;

                int polUid = Convert.ToInt32(Request.QueryString["polUid"]);

                if (polUid == 0)
                {
                    txtPolFmDt.Text = string.Empty;
                }

                else
                {
                    DataTable dt = objMotorPolicyManager.LoadDetails(polUid);

                    txtPolNo.Text = dt.Rows[0]["POL_NO"].ToString();
                    txtPolIssDt.Text = DateTime.Now.ToString("dd-MM-yyyy");
                    txtPolFmDt.Text = Convert.ToDateTime(dt.Rows[0]["POL_FM_DT"]).ToString("dd-MM-yyyy");
                    txtPolToDt.Text = Convert.ToDateTime(dt.Rows[0]["POL_TO_DT"]).ToString("dd-MM-yyyy");
                    txtPolAssrName.Text = dt.Rows[0]["POL_ASSR_NAME"].ToString();
                    txtPolAssrMobile.Text = dt.Rows[0]["POL_ASSR_MOBILE"].ToString();
                    ddlPolCurrCode.SelectedValue = dt.Rows[0]["POL_CURR_CODE"].ToString();
                    txtPolGrossFCPrem.Text = dt.Rows[0]["POL_GROSS_FC_PREM"].ToString();
                    txtPolGrossLCPrem.Text = dt.Rows[0]["POL_GROSS_LC_PREM"].ToString();
                    ddlPolVehMake.SelectedValue = dt.Rows[0]["POL_VEH_MAKE"].ToString();
                    ddlPolVehModel.DataSource = objMotorPolicyManager.returndropdown("VEH_MODEL", ddlPolVehMake.SelectedValue);
                    ddlPolVehModel.DataTextField = "TEXT";
                    ddlPolVehModel.DataValueField = "CODE";
                    ddlPolVehModel.DataBind();
                    ddlPolVehModel.Items.Insert(0, new ListItem("--SELECT--", ""));
                    ddlPolVehModel.SelectedValue = dt.Rows[0]["POL_VEH_MODEL"].ToString();
                    txtPolVehChassisNo.Text = dt.Rows[0]["POL_VEH_CHASSIS_NO"].ToString();
                    txtPolVehEngineNo.Text = dt.Rows[0]["POL_VEH_ENGINE_NO"].ToString();
                    txtPolVehRegnNo.Text = dt.Rows[0]["POL_VEH_REGN_NO"].ToString();
                    txtPolVehValue.Text = dt.Rows[0]["POL_VEH_VALUE"].ToString();
                    string status = dt.Rows[0]["POL_APPR_STATUS"].ToString();

                    if (status == "A")
                    {
                        PolicyApproved();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        protected void txtPolFmDt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.TryParse(txtPolFmDt.Text, out DateTime parsedDate))
                {
                    if (parsedDate < DateTime.Today)
                    {
                        string message = objErrorCodeMasterManager.GetErrorMsg("8001");
                        string script = $"swal('{message}', '', 'warning');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='Policy.aspx?polUid=" + hfuid.Value + "'; }, 1000);", true);
                    }

                    else
                    {
                        DateTime fromdate;
                        if (DateTime.TryParse(txtPolFmDt.Text, out fromdate))
                        {
                            DateTime toDate = fromdate.AddDays(364);
                            txtPolToDt.Text = toDate.ToString("dd-MM-yyyy");
                        }
                        else
                        {
                            txtPolToDt.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    txtPolFmDt.Text = string.Empty;
                    txtPolToDt.Text = string.Empty;                    
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        

        protected void txtPolGrossFCPrem_TextChanged1(object sender, EventArgs e)
        {
            if (ddlPolCurrCode.SelectedIndex > 0)
            {
                decimal s;
                CurrencyConvertion objCurrencyConversion = new CurrencyConvertion();
                decimal.TryParse(txtPolGrossFCPrem.Text, out s);
                decimal lcAmt = objCurrencyConversion.ConvertCurrency(s, ddlPolCurrCode.SelectedValue);
                txtPolGrossLCPrem.Text = lcAmt.ToString("F2");
                if (lcAmt > 9999999) 
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("8002");
                    string script = $"swal('{message}', '', 'warning');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true); 
                    txtPolGrossLCPrem.Text = string.Empty;
                    txtPolGrossFCPrem.Text = string.Empty;
                }
            }
            else
            {
                txtPolGrossFCPrem.Text = string.Empty;
                string message = objErrorCodeMasterManager.GetErrorMsg("8003");
                string script = $"swal('{message}', '', 'warning');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
            }
        }

        protected void ddlPolVehMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlPolVehModel.DataSource = objMotorPolicyManager.returndropdown("VEH_MODEL", ddlPolVehMake.SelectedValue);
                ddlPolVehModel.DataTextField = "TEXT";
                ddlPolVehModel.DataValueField = "CODE";
                ddlPolVehModel.DataBind();
                ddlPolVehModel.Items.Insert(0, new ListItem("--SELECT--", ""));
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
                objMotorPolicy.PolIssDt = Convert.ToDateTime(txtPolIssDt.Text).Date;
                objMotorPolicy.PolFmDt = Convert.ToDateTime(txtPolFmDt.Text);
                objMotorPolicy.PolToDt = Convert.ToDateTime(txtPolToDt.Text);
                objMotorPolicy.PolAssrName = txtPolAssrName.Text;
                objMotorPolicy.PolAssrMobile = txtPolAssrMobile.Text;
                objMotorPolicy.PolCurrCode = ddlPolCurrCode.SelectedValue;
                objMotorPolicy.PolGrossFcPrem = Convert.ToDouble(txtPolGrossFCPrem.Text);
                //objMotorPolicy.PolGrossFcPrem = Convert.ToDouble((string.Format("{0:#,##0.00}", txtPolGrossFCPrem.Text))); 
                objMotorPolicy.PolGrossLcPrem = Convert.ToDouble(txtPolGrossLCPrem.Text);
                objMotorPolicy.PolVehMake = ddlPolVehMake.SelectedValue;
                objMotorPolicy.PolVehModel = ddlPolVehModel.SelectedValue;
                objMotorPolicy.PolVehChassisNo = txtPolVehChassisNo.Text;
                objMotorPolicy.PolVehEngineNo = txtPolVehEngineNo.Text;
                objMotorPolicy.PolVehRegnNo = txtPolVehRegnNo.Text;
                objMotorPolicy.PolVehValue = Convert.ToDouble(txtPolVehValue.Text);
                //objMotorPolicy.PolVehValue = Convert.ToDouble((string.Format("{0:#,##0.00}", txtPolVehValue.Text)));
                objMotorPolicy.PolApprStatus = "N";
                //objMotorPolicy.PolApprDt=
                //objMotorPolicy.PolApprBy=
                objMotorPolicy.PolCrBy = Session["ID"].ToString();
                objMotorPolicy.PolCrDt = DateTime.Now;
                //objMotorPolicy.PolUpDt=
                //objMotorPolicy.PolUpBy=

                hfuid.Value = (objMotorPolicyManager.AddPolicy(objMotorPolicy)).ToString();
            
                if (hfuid.Value != "0")
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                      "setTimeout(function(){ window.location.href='Policy.aspx?polUid=" + hfuid.Value + "'; }, 1000);",
                      true);
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
                int polUid = Convert.ToInt32(Request.QueryString["polUid"]);
                objMotorPolicy.PolNo = txtPolNo.Text;
                objMotorPolicy.PolIssDt = Convert.ToDateTime(txtPolIssDt.Text);
                objMotorPolicy.PolFmDt = Convert.ToDateTime(txtPolFmDt.Text);
                objMotorPolicy.PolToDt = Convert.ToDateTime(txtPolToDt.Text);
                objMotorPolicy.PolAssrName = txtPolAssrName.Text;
                objMotorPolicy.PolAssrMobile = txtPolAssrMobile.Text;
                objMotorPolicy.PolCurrCode = ddlPolCurrCode.SelectedValue;
                objMotorPolicy.PolGrossFcPrem = Convert.ToDouble(txtPolGrossFCPrem.Text);
                objMotorPolicy.PolGrossLcPrem = Convert.ToDouble(txtPolGrossLCPrem.Text);
                objMotorPolicy.PolVehMake = ddlPolVehMake.SelectedValue;
                objMotorPolicy.PolVehModel = ddlPolVehModel.SelectedValue;
                objMotorPolicy.PolVehChassisNo = txtPolVehChassisNo.Text;
                objMotorPolicy.PolVehEngineNo = txtPolVehEngineNo.Text;
                objMotorPolicy.PolVehRegnNo = txtPolVehRegnNo.Text;
                objMotorPolicy.PolVehValue = Convert.ToDouble(txtPolVehValue.Text);
                objMotorPolicy.PolUpBy = Session["ID"].ToString();
                objMotorPolicy.PolUpDt = DateTime.Now;

                int rowsAffected = objMotorPolicyManager.UpdatePolicy(objMotorPolicy);

                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2001");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                    "setTimeout(function(){ window.location.href='Policy.aspx?polUid=" + polUid + "'; }, 1000);",
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

        

        protected void approveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string polUid = Request.QueryString["polUid"];
                objMotorPolicy.PolUid = Convert.ToInt64(polUid);
                objMotorPolicy.PolApprBy = Session["ID"].ToString();
                objMotorPolicy.PolApprDt = DateTime.Now;

                int rowsAffected = objMotorPolicyManager.ApprovePolicy(objMotorPolicy);
                if (rowsAffected > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2002");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                    "setTimeout(function(){ window.location.href='Policy.aspx?polUid=" + polUid + "'; }, 1000);",
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

        protected void PolicyApproved()
        {           
            try
            {
                btnSave.Visible = false;
                btnUpdate.Visible = false;
                approveBtn.Visible = false;
                PolicyDetailsTitle.Visible = false;
                ApprovedPolicyTitle.Visible = true;

                approveBtn.Visible = false;
                txtPolNo.Enabled = false;
                txtPolIssDt.Enabled = false;
                txtPolFmDt.Enabled = false;
                txtPolToDt.Enabled = false;
                txtPolAssrName.Enabled = false;
                txtPolAssrMobile.Enabled = false;
                txtPolGrossFCPrem.Enabled = false;
                txtPolGrossLCPrem.Enabled = false;
                txtPolVehChassisNo.Enabled = false;
                txtPolVehEngineNo.Enabled = false;
                txtPolVehRegnNo.Enabled = false;
                txtPolVehValue.Enabled = false;

                ddlPolCurrCode.Enabled = false;
                ddlPolVehMake.Enabled = false;
                ddlPolVehModel.Enabled = false;

            }
            catch (Exception err)
            {
                throw err;
            }
        }        
        protected void ddlPolCurrCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPolCurrCode.SelectedIndex > 0)
                {
                    decimal fcPremium;

                    if (decimal.TryParse(txtPolGrossFCPrem.Text, out fcPremium))
                    {
                        string currencyCode = ddlPolCurrCode.SelectedValue;
                        CurrencyConvertion objCurrencyConversion = new CurrencyConvertion();
                        decimal lcPremium = objCurrencyConversion.ConvertCurrency(fcPremium, currencyCode);
                        txtPolGrossLCPrem.Text = lcPremium.ToString("F2"); // Optionally format the value to 2 decimal places

                    }
                }
                else
                {
                    txtPolGrossFCPrem.Text = string.Empty;
                    txtPolGrossLCPrem.Text = string.Empty;
                    string script = "swal(' Please Select valid currency', '','warning');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PolicyListing.aspx");
        }

        //protected void FormatTextBox(TextBox textBox)
        //{
        //    try
        //    {
        //        string rawValue = textBox.Text.Replace(",", "");

        //        if (decimal.TryParse(rawValue, out decimal numericValue))
        //        {
        //            textBox.Text = numericValue.ToString("N0");
        //        }
        //        else
        //        {
        //            textBox.Text = string.Empty;
        //        }
        //    }
        //    catch (Exception err)
        //    {

        //        throw err;
        //    }
        //}
    }
}
    
