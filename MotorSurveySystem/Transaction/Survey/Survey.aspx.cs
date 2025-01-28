using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem.Transaction.Survey
{
    public partial class Survey : System.Web.UI.Page
    {
        MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
        MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
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

                    if (Request.QueryString["surUid"] != null)
                    {

                        FetchValues();

                        BindGrid();
                        if (gvSurveyDetails.Rows.Count > 0)
                        {
                            gvSurveyDetails.Visible = true;
                        }
                    }

                    else
                    {

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
                ddlSurLocation.DataSource = objMotorClmSurHdrManager.returndropdown("LOCATION");

                ddlSurLocation.DataTextField = "TEXT";
                ddlSurLocation.DataValueField = "CODE";
                ddlSurLocation.DataBind();
                ddlSurLocation.Items.Insert(0, new ListItem("--SELECT--", ""));

                ddlSurCurr.DataSource = objMotorClmSurHdrManager.returndropdown("CURRENCY");

                ddlSurCurr.DataTextField = "TEXT";
                ddlSurCurr.DataValueField = "CODE";
                ddlSurCurr.DataBind();
                ddlSurCurr.Items.Insert(0, new ListItem("--SELECT--", ""));
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

                txtIntimationDate.Enabled = false;
                txtSurClmNo.Enabled = false;
                txtSurDate.Enabled = false;
                txtSurChassisNo.Enabled = false;
                txtSurRegnNo.Enabled = false;
                txtSurEngineNo.Enabled = false;
                txtSurFcAmt.Enabled = false;
                txtSurLcAmt.Enabled = false;
                int SurUid = Convert.ToInt32(Request.QueryString["surUid"]);

                DataTable dt = objMotorClmSurHdrManager.LoadDetails(SurUid);

                ddlSurLocation.SelectedValue = dt.Rows[0]["SUR_LOCATION"].ToString();
                ddlSurCurr.SelectedValue = dt.Rows[0]["SUR_CURR"].ToString();


                if (ddlSurCurr.SelectedValue != "" && ddlSurLocation.SelectedValue != "")
                {
                    btnAddDetails.Visible = true;
                }

                txtIntimationDate.Text = dt.Rows[0]["SUR_CR_DT"].ToString();
                lblSurNo.Text = dt.Rows[0]["SUR_NO"].ToString();
                txtSurClmNo.Text = dt.Rows[0]["SUR_CLM_NO"].ToString();
                txtSurDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                
                txtSurChassisNo.Text = dt.Rows[0]["SUR_CHASSIS_NO"].ToString();
                txtSurRegnNo.Text = dt.Rows[0]["SUR_REGN_NO"].ToString();
                txtSurEngineNo.Text = dt.Rows[0]["SUR_ENGINE_NO"].ToString();
                txtSurFcAmt.Text = dt.Rows[0]["SUR_FC_AMT"].ToString();
                //txtSurLcAmt.Text = dt.Rows[0]["SUR_LC_AMT"].ToString();
                txtSurLcAmt.Text = (dt.Rows[0]["SUR_LC_AMT"] != DBNull.Value)? Convert.ToDecimal(dt.Rows[0]["SUR_LC_AMT"]).ToString("N2") : "";



                int row = objMotorClmSurHdrManager.LoadDetailsTable(SurUid);

                if (row >= 1)
                {
                    approveBtn.Visible = true;
                    disableHeaderFields();
                }

                int status = objMotorClmSurHdrManager.FetchSurveyStatus(SurUid);
                if (status == 1)
                {
                    approveBtn.Visible = false;
                    btnAddDetails.Visible = false;
                    lblApprove.Visible = true;

                    //btnAddDetails.Enabled = false;
                    //btnAddDetails.Text = "Approved";
                }

            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void disableHeaderFields()
        {
            //btnAddDetails.Visible = false;
            //approveBtn.Visible = false;

            try
            {
                txtSurClmNo.Enabled = false;
                txtSurDate.Enabled = false;
                ddlSurLocation.Enabled = false;
                txtSurChassisNo.Enabled = false;
                txtSurRegnNo.Enabled = false;
                txtSurEngineNo.Enabled = false;
                ddlSurCurr.Enabled = false;
                txtSurFcAmt.Enabled = false;
                txtSurLcAmt.Enabled = false;

                btnSaveSurvey.Visible = false;
            }
            catch (Exception err)
            {

                throw err;
            }

        }

        protected void btnSaveSurvey_Click(object sender, EventArgs e)
        {
            try
            {
                //objMotorClmSurHdr.SurClmNo = txtSurClmNo.Text;
                long SurUid = Convert.ToInt64(Request.QueryString["surUid"]);
                objMotorClmSurHdr.SurUid = SurUid;
                objMotorClmSurHdr.SurDate = Convert.ToDateTime(txtSurDate.Text);
                objMotorClmSurHdr.SurLocation = ddlSurLocation.SelectedValue;
                objMotorClmSurHdr.SurCurr = ddlSurCurr.SelectedValue;
                objMotorClmSurHdr.SurCrBy = Session["ID"].ToString();
                //objMotorClmSurHdr.SurDate = Convert.ToDateTime(txtSurDate.Text);
                //objMotorClmSurHdr.SurChassisNo = txtSurChassisNo.Text;
                //objMotorClmSurHdr.SurRegnNo = txtSurRegnNo.Text;
                //objMotorClmSurHdr.SurEngineNo = txtSurEngineNo.Text;
                //objMotorClmSurHdr.SurFcAmt = Convert.ToDouble(txtSurFcAmt.Text);
                //objMotorClmSurHdr.SurLcAmt = Convert.ToDouble(txtSurLcAmt.Text);           
                //hfuid.Value = SurUid.ToString();

                int rows = objMotorClmSurHdrManager.SaveSurvey(objMotorClmSurHdr);

                if (rows > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("2000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect","setTimeout(function(){ window.location.href='Survey.aspx?surUid=" + SurUid + "'; }, 1000);",true);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                long surUid = Convert.ToInt64(Request.QueryString["surUid"]);

                string currency = ddlSurCurr.SelectedValue;
                Response.Redirect("~/Transaction/SurveyDetails/SurveyDetails.aspx?currency=" + currency + "&surUid=" + surUid);
                //gvSurveyDetails.Visible = true;   
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void BindGrid()
        {
            try
            {
                string surUid = Request.QueryString["surUid"];
                DataTable dt = objMotorClmSurHdrManager.LoadSurveyDetailsGrid(surUid);
                gvSurveyDetails.DataSource = dt;
                gvSurveyDetails.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

                string surdUid = ((Label)row.FindControl("lblSurdUid")).Text;
                string surUid = ((Label)row.FindControl("lblSurdSurUid")).Text;
                string currency = ddlSurCurr.SelectedValue;

                Response.Redirect($"~/Transaction/SurveyDetails/SurveyDetails.aspx?surdUid={surdUid}&surUid={surUid}&currency={currency}");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                LinkButton btnEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

                string surdUid = ((Label)row.FindControl("lblSurdUid")).Text;
                string surUid = ((Label)row.FindControl("lblSurdSurUid")).Text;

                int rows = objMotorClmSurHdrManager.Delete(surdUid, surUid);

                if (rows > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("3000");
                    string script = $"swal('{message}', '', 'success');";
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", $"setTimeout(function(){{ window.location.href='Survey.aspx?surUid={surUid}'; }}, 1000);", true);
                    BindGrid();
                }
            }

            catch (Exception err)
            {
                string msg = objErrorCodeMasterManager.GetErrorMsg("5000");
                string script = $"swal('{msg}', '','error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", $"setTimeout(function(){{ window.location.href='Survey.aspx?surUid={surUid}'; }}, 1000);", true);

            }
        }

        protected void approveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                long surUid = Convert.ToInt64(Request.QueryString["surUid"]);
                objMotorClmSurHdr.SurClmUid = objMotorClmSurHdrManager.FetchClmUid(surUid);

                Dictionary<string, object> dict = new Dictionary<string, object>();

                dict["CLM_UID"] = objMotorClmSurHdr.SurClmUid;

                long clmUid = Convert.ToInt64(dict["CLM_UID"]);

                string userId = Session["ID"].ToString();

                (int status, string message) = objMotorClmSurHdrManager.Approve(surUid, clmUid, userId);

                if (status == 1 && message != null)
                {
                    string msg = objErrorCodeMasterManager.GetErrorMsg("2004");
                    string script = $"swal('{msg}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
              "setTimeout(function(){ window.location.href='Survey.aspx?surUid=" + surUid + "'; }, 1000);",
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            //LinkButton btnEdit = (LinkButton)sender;
            //GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

            ////string surUid = ((Label)row.FindControl("lblSurdSurUid")).Text;
            //string surdUid = ((Label)row.FindControl("lblSurdUid")).Text;

            //Response.Redirect($"SurveyDetails.aspx?surdUid={surdUid}");
        }

        protected void Backbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string type = Request.QueryString["userType"];
                if (type == "U")
                {
                    string clmNo = txtSurClmNo.Text;
                    long clmUid = objMotorClmSurHdrManager.getClmUid(clmNo);
                    Response.Redirect("~/Transaction/Claim/Claim.aspx?clmUid=" + clmUid);
                }
                else
                {
                    Response.Redirect("SurveyListing.aspx");
                }
            }
            catch (Exception err)
            {

                throw err;
            }

        }

        protected void gvSurveyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string status = DataBinder.Eval(e.Row.DataItem, "SURD_STATUS").ToString();

                    // Set the Action column visibility based on the status
                    e.Row.Cells[8].Visible = status == "P";
                }
            }
            catch (Exception err)
            {

                throw err;
            }


        }

        protected void gvSurveyDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    bool actionColumnVisible = gvSurveyDetails.Rows.Cast<GridViewRow>()
                                                .Any(row => DataBinder.Eval(row.DataItem, "SURD_STATUS")?.ToString() == "P");

                    e.Row.Cells[8].Visible = actionColumnVisible; // Adjust the index if necessary
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}
