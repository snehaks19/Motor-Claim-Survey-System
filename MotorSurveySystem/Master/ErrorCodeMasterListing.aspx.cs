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
    public partial class ErrorCodeMasterListing : System.Web.UI.Page
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
                    BindGrid();
                }
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
                DataTable dt = objErrorCodeMasterManager.LoadGrid();
                gvErrorCodeMaster.DataSource = dt;
                gvErrorCodeMaster.DataBind();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void AddNewBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ErrorCodeMasterEdit.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

                string pErrCode = ((Label)row.FindControl("lblErrCode")).Text;
                string pErrType = ((Label)row.FindControl("lblErrType")).Text;

                Response.Redirect($"ErrorCodeMasterEdit.aspx?errCode={pErrCode}&errType={pErrType}");
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

                string pErrCode = ((Label)row.FindControl("lblErrCode")).Text;
                string pErrType = ((Label)row.FindControl("lblErrType")).Text;

                int rows = objErrorCodeMasterManager.Delete(pErrCode, pErrType);

                if (rows > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("3000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='ErrorCodeMasterListing.aspx'; }, 1000);", true);
                    BindGrid();


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        protected void gvErrorCodeMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvErrorCodeMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserDashboard.aspx");
        }
    }
}