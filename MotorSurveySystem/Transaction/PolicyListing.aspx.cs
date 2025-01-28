using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem.Transaction
{
    public partial class PolicyListing : System.Web.UI.Page
    {
        MotorPolicyManager objMotorPolicyManager = new MotorPolicyManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindGridForPolicyListing();           
            }
        }

        private void BindGridForPolicyListing()
        {
            DataTable dt = objMotorPolicyManager.LoadPolicyListingGrid();
            gvPolicyListing.DataSource = dt;
            gvPolicyListing.DataBind();
        }

        protected void AddNewBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Policy.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            MotorPolicyManager objMotorPolicyManager = new MotorPolicyManager();

            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btnEdit.NamingContainer;
            string poluid = ((Label)row.FindControl("lblPoluid")).Text;        

            int rows = objMotorPolicyManager.DeletePolicy(poluid);

            if (rows > 0)
            {
                BindGridForPolicyListing();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

            string poluid = ((Label)row.FindControl("lblPoluid")).Text;
            
            Response.Redirect($"Policy.aspx?polUid={poluid}");
            
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserDashboard.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btnEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

            string poluid = ((Label)row.FindControl("lblPoluid")).Text;

            Response.Redirect($"Policy.aspx?polUid={poluid}");
        }

        protected void gvPolicyListing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPolicyListing.PageIndex = e.NewPageIndex;
            BindGridForPolicyListing();
        }
    }
    }
