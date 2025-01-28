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
    public partial class SurveyListing : System.Web.UI.Page
    {
        MotorClmSurHdr objMotorClmSurHdr = new MotorClmSurHdr();
        MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();

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
                DataTable dt = objMotorClmSurHdrManager.LoadGrid();
                gvSurveyListing.DataSource = dt;
                gvSurveyListing.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void btnEditSurvey_Click(object sender, EventArgs e)
        {

        }

        protected void gvSurveyListing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSurveyListing.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnEditSurvey_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEdit = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

                string SurUid = ((Label)row.FindControl("lblSurveyUid")).Text;

                Response.Redirect($"Survey.aspx?surUid={SurUid}");
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }
}
