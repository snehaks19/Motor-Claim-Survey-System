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
    public partial class ClaimListing : System.Web.UI.Page
    {
        MotorClaim objMotorClaim = new MotorClaim();
        MotorClaimManager objMotorClaimManager = new MotorClaimManager();

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
                    BindGridForClaimListing();
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void BindGridForClaimListing()
        {
            DataTable dt = objMotorClaimManager.LoadClaimListingGrid();
            gvClaimListing.DataSource = dt;
            gvClaimListing.DataBind();
        }

        protected void AddNewBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Claim.aspx");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserDashboard.aspx");
        }

        protected void gvClaimListing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaimListing.PageIndex = e.NewPageIndex;
            BindGridForClaimListing();
        }

        protected void btnEditClaim_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

                string clmuid = ((Label)row.FindControl("lblClmUid")).Text;

                Response.Redirect($"Claim.aspx?clmUid={clmuid}");
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void btnViewClaim_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

                string clmuid = ((Label)row.FindControl("lblClmUid")).Text;

                Response.Redirect($"Claim.aspx?clmUid={clmuid}");
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}