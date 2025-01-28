using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;


namespace MotorSurveySystem.Master
{
    public partial class CodesMasterListing : System.Web.UI.Page
    {
        CodesMasterManager ObjCodesMasterManager = new CodesMasterManager();
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
                    BindGridForCodesMaster();
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        private void BindGridForCodesMaster()
        {
            try
            {
                DataTable dt = ObjCodesMasterManager.LoadCodesMasterGrid();
                gvCodesMaster.DataSource = dt;
                gvCodesMaster.DataBind();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void AddNewBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CodeMasterEdit.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

                string pcmCode = ((Label)row.FindControl("lblCMCode")).Text;
                string pcmType = ((Label)row.FindControl("lblCMType")).Text;

                Response.Redirect($"CodeMasterEdit.aspx?cmCode={pcmCode}&cmType={pcmType}");
            }
            catch (Exception err)
            {
                throw err;
            }
            // Response.Redirect($"CodeMaster.aspx?cmcode={Code}&cmtype={Type}");
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CodesMasterManager objCodesMasterManager = new CodesMasterManager();

                LinkButton btnEdit = (LinkButton)sender;

                GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

                string cmCode = ((Label)row.FindControl("lblCMCode")).Text;
                string cmType = ((Label)row.FindControl("lblCMType")).Text;

                int rows = objCodesMasterManager.DeleteCodesMaster(cmCode, cmType);

                if (rows > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("3000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='CodesMasterListing.aspx'; }, 1000);", true);

                    BindGridForCodesMaster();
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        protected void gvCodesMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCodesMaster.PageIndex = e.NewPageIndex;
            BindGridForCodesMaster();
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserDashboard.aspx");
        }
    }
    }
