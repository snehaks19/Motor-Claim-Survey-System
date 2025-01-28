using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem
{
    public partial class Login : System.Web.UI.Page
    {
        ErrorCodeMasterManager objErrorCodeMasterManager = new ErrorCodeMasterManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserMaster ObjUserMaster = new UserMaster();
                UserMasterManager ObjUserMasterManager = new UserMasterManager();

                ObjUserMaster.UserId = txtUserId.Text.Trim();
                ObjUserMaster.UserPassword = txtPassword.Text.Trim();

                string userType = ObjUserMasterManager.UserExist(ObjUserMaster);

                if (userType == "U")
                {
                    Session["ID"] = txtUserId.Text;
                    Session["UserType"] = "U";
                    Response.Redirect("UserDashboard.aspx");
                }

                else if (userType == "S")
                {
                    Session["ID"] = txtUserId.Text;
                    Session["UserType"] = "S";
                    Response.Redirect("UserDashboard.aspx");
                }

                else
                {
                    string message = objErrorCodeMasterManager.GetMessage("1001");

                    string script = $@"
                        swal({{
                            title: '{message}',
                            text: 'Invalid credentials, please try again.',
                            icon: 'error',
                            buttons: {{
                                confirm: {{
                                    text: 'Try Again',
                                    value: true
                                }}
                            }}
                        }});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlert", script, true);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}