using BusinessLayer;
using MotorSurveySystem.WebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem.Transaction.SurveyDetails
{
    public partial class SurveyDetails : System.Web.UI.Page
    {
        MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();
        MotorClmSurDtl objMotorClmSurDtl = new MotorClmSurDtl();
        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        MotorClmSurDtlHistManager objMotorClmSurDtlHistManager = new MotorClmSurDtlHistManager();
        MotorClmSurDtlHist objMotorClmSurDtlHist = new MotorClmSurDtlHist();

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
                    LoadDropDrowns();

                    if (Request.QueryString["surdUid"] != null && Request.QueryString["surUid"] != null && Request.QueryString["currency"] != null)
                    {
                        FetchValues();
                        ddlItemCode.Enabled = false;
                    }
                    else
                    {
                        //AddNewClaim();
                    }
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
                string surdUid = Request.QueryString["surdUid"];
                string surUid = Request.QueryString["surUid"];
                string currency = Request.QueryString["currency"];
                DataTable dt = objMotorClmSurDtlManager.LoadDetails(surdUid, surUid);
                ddlItemCode.SelectedValue = dt.Rows[0]["SURD_ITEM_CODE"].ToString();
                ddlType.SelectedValue = dt.Rows[0]["SURD_TYPE"].ToString();
                txtUnitPrice.Text = dt.Rows[0]["SURD_UNIT_PRICE"].ToString();
                txtQuantity.Text = dt.Rows[0]["SURD_QUANTITY"].ToString();
                txtFcAmt.Text = dt.Rows[0]["SURD_FC_AMT"].ToString();
                txtLcAmt.Text = dt.Rows[0]["SURD_LC_AMT"].ToString();
                btnHistory.Visible = true;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void LoadDropDrowns()
        {
            try
            {
                ddlType.DataSource = objMotorClmSurDtlManager.returnDropDown("ITEM_TYPE");
                ddlType.DataTextField = "TEXT";
                ddlType.DataValueField = "CODE";
                ddlType.DataBind();
                ddlType.Items.Insert(0, new ListItem("--SELECT--", ""));

                ddlItemCode.DataSource = objMotorClmSurDtlManager.returnDropDown("VEH_PARTS");
                ddlItemCode.DataTextField = "TEXT";
                ddlItemCode.DataValueField = "CODE";
                ddlItemCode.DataBind();
                ddlItemCode.Items.Insert(0, new ListItem("--SELECT--", ""));
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateLc();
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateLc();
        }


        protected void CalculateLc()
        {
            try
            {
                decimal unitPrice = 0;
                decimal quantity = 0;


                if (decimal.TryParse(txtUnitPrice.Text, out unitPrice) &&
                    decimal.TryParse(txtQuantity.Text, out quantity))
                {

                    decimal lcAmount = unitPrice * quantity;
                    //txtLcAmt.Text = lcAmount.ToString();
                    txtLcAmt.Text = lcAmount.ToString("#,##0.00");


                    string currency = Request.QueryString["currency"].ToString();
                    decimal s;
                    CurrencyConvertion objCurrencyConversion = new CurrencyConvertion();
                    decimal.TryParse(txtLcAmt.Text, out s);
                    txtFcAmt.Text = (objCurrencyConversion.ConvertCurrency1(s, currency)).ToString("N2");
                }
                else
                {
                    txtLcAmt.Text = string.Empty;
                }
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

                long surUid = Convert.ToInt64(Request.QueryString["surUid"]);
                string currency = Request.QueryString["currency"];

                objMotorClmSurDtl.SurdUid = Request.QueryString["surdUid"] != null ? Convert.ToInt64(Request.QueryString["surdUid"]) : 0;
                objMotorClmSurDtl.SurdSurUid = surUid;
                objMotorClmSurDtl.SurdItemCode = ddlItemCode.SelectedValue;
                objMotorClmSurDtl.SurdType = ddlType.SelectedValue;
                objMotorClmSurDtl.SurdUnitPrice = Convert.ToInt32(txtUnitPrice.Text.Replace(",", ""));
                objMotorClmSurDtl.SurdQuantity = Convert.ToInt32(txtQuantity.Text);
                objMotorClmSurDtl.SurdFcAmt = Convert.ToDouble(txtFcAmt.Text);
                objMotorClmSurDtl.SurdLcAmt = Convert.ToDouble(txtLcAmt.Text);
                objMotorClmSurDtl.SurdCrBy = Session["ID"].ToString();
                //objMotorClmSurDtl.SurdRemarks = txtRemarks.Text;

                (int status, long uid, string message) = objMotorClmSurDtlManager.SaveDetails(objMotorClmSurDtl);

                long surdUid = uid;

                if (status == 1 && message !=null)
                {
                    string msg = objErrorCodeMasterManager.GetErrorMsg("2000");
                    string script = $"swal('{msg}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                      "setTimeout(function(){ window.location.href='SurveyDetails.aspx?surdUid=" + surdUid + "&surUid=" + surUid + "&currency=" + currency + "'; }, 1000);",
                      true);
                }
                else
                {
                    string script = $"swal('{message}', '', 'error');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect",
                      "setTimeout(function(){ window.location.href='SurveyDetails.aspx?surdUid=" + surdUid + "&surUid=" + surUid + "&currency=" + currency + "'; }, 1000);",
                      true);
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void btnHistory_Click(object sender, EventArgs e)
        {
            BindHistoryGrid();
        }

        protected void BindHistoryGrid()
        {
            try
            {
                long id = Convert.ToInt64(Request.QueryString["surdUid"]);
                DataTable dt = objMotorClmSurDtlHistManager.LoadGrid(id);
                gvHistory.DataSource = dt;
                gvHistory.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHistory.PageIndex = e.NewPageIndex;
            BindHistoryGrid();
        }

      

        protected void AddNewBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string currency = Request.QueryString["currency"];
                long surUid = Convert.ToInt64(Request.QueryString["surUid"]);

                Response.Redirect("~/Transaction/SurveyDetails/SurveyDetails.aspx?currency=" + currency + "&surUid=" + surUid);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        
        protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currency = Request.QueryString["currency"];
            long surUid = Convert.ToInt64(Request.QueryString["surUid"]);

            int rowsAffected = objMotorClmSurDtlManager.CheckDuplicate(ddlItemCode.SelectedValue, surUid);
            if (rowsAffected > 0)
            {
                string message = objErrorCodeMasterManager.GetErrorMsg("9012");
                string script = $"swal('{message}', '', 'error');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                //Response.Redirect("~/Transaction/SurveyDetails/SurveyDetails.aspx?currency=" + currency + "&surUid=" + surUid);
                ddlItemCode.SelectedValue = string.Empty;
            }
        }
        protected void backbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string surUid = Request.QueryString["surUid"];
                Response.Redirect("~/Transaction/Survey/Survey.aspx?surUid=" + surUid);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void txtLcAmt_TextChanged(object sender, EventArgs e)
        {
            //string currency = Request.QueryString["currency"].ToString();
            //decimal s;
            //CurrencyConvertion objCurrencyConversion = new CurrencyConvertion();
            //decimal.TryParse(txtLcAmt.Text, out s);
            //txtFcAmt.Text = (objCurrencyConversion.ConvertCurrency(s, currency)).ToString();

            ////FormatTextBox(txtPolGrossFCPrem);
        }
    }
}