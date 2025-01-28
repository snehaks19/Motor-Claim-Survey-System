using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem
{
    public partial class UserMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                string type = Session["UserType"].ToString();
                if (type == "U")
                {
                    ltPolicyLink.Text = "<li><a class='dropdown-item' href='/Transaction/PolicyListing.aspx'>Policy</a></li>";
                    ltClaimLink.Text = "<li><a class='dropdown-item' href='/Transaction/Claim/ClaimListing.aspx'>Claim</a></li>";
                    ltSurveyLink.Text = "";
                }
                else if (type == "S")
                {
                    ltPolicyLink.Text = "";
                    ltClaimLink.Text = "";
                    ltSurveyLink.Text = "<li><a class='dropdown-item' href='/Transaction/Survey/SurveyListing.aspx'>Survey</a></li>";
                    masterddPanel.Visible = false;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}