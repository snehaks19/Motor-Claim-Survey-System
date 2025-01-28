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
    public partial class UserMasterListing : System.Web.UI.Page
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
                DataTable dt = objUserMasterManager.LoadGrid();
                gvUserMaster.DataSource = dt;
                gvUserMaster.DataBind();
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

                string userId = ((Label)row.FindControl("lblUserID")).Text;
                //string userType = ((Label)row.FindControl("lblErrType")).Text;

                Response.Redirect($"UserMasterEdit.aspx?userId={userId}");
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

                string userId = ((Label)row.FindControl("lblUserId")).Text;
                // string pErrType = ((Label)row.FindControl("lblErrType")).Text;

                int rows = objUserMasterManager.Delete(userId);

                if (rows > 0)
                {
                    string message = objErrorCodeMasterManager.GetErrorMsg("3000");
                    string script = $"swal('{message}', '', 'success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", "setTimeout(function(){ window.location.href='UserMasterListing.aspx'; }, 1000);", true);
                    BindGrid();


                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserDashboard.aspx");
        }

        protected void AddNewBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserMasterEdit.aspx");
        }

        protected void gvUserMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUserMaster.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
