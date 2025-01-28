using BusinessLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorSurveySystem.Report
{
    public partial class Surveyreport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MotorClaimManager objMotorClaimManager = new MotorClaimManager();
                MotorPolicyManager objMotorPolicyManager = new MotorPolicyManager();
                MotorClmSurHdrManager objMotorClmSurHdrManager = new MotorClmSurHdrManager();
                MotorClmSurDtlManager objMotorClmSurDtlManager = new MotorClmSurDtlManager();
                MotorClmSurDtlHistManager objMotorClmSurDtlHistManager = new MotorClmSurDtlHistManager();

                int clmUid = Convert.ToInt32(Request.QueryString["clmUid"]);
                int polUid = Convert.ToInt32(Request.QueryString["polUid"]);
                int surUid = Convert.ToInt32(Request.QueryString["surUid"]);
                
                DataTable dt1 = objMotorClaimManager.ClaimDataReport(clmUid);
                DataTable dt2 = objMotorPolicyManager.PolicyDataReport(polUid);
                DataTable dt3 = objMotorClmSurHdrManager.SurveyDataReport(surUid);
                DataTable dt4 = objMotorClmSurDtlManager.SurveyDetailsDataReport(surUid);
                
                if (dt1.Rows.Count > 0 && dt2.Rows.Count>0 && dt3.Rows.Count > 0 && dt4.Rows.Count > 0)
                {
                    DataSet1 objdataSet1 = new DataSet1();
                    objdataSet1.Tables["MOTOR_CLAIM"].Merge(dt1);
                    objdataSet1.Tables["MOTOR_POLICY"].Merge(dt2);
                    objdataSet1.Tables["MOTOR_CLM_SUR_HDR"].Merge(dt3);
                    objdataSet1.Tables["MOTOR_CLM_SUR_DTL"].Merge(dt4);
                    string reportPath = Server.MapPath("~") + "Report\\Report.rpt";
                    ReportDocument report = new ReportDocument();
                    report.Load(reportPath);
                    report.SetDataSource(objdataSet1);

                    report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "SurveyReport");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", "showErrorMessage" +
                            "(''," +
                            "'CLAIM IS NOT APPROVED');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //string uid = Session["ID"].ToString();
            //if (string.IsNullOrEmpty(uid))
            //{
            //    Response.Write("User Id is missing.");
            //    return;
            //}

            //try
            //{
            //    UserMasterManager objUserMasterManager = new UserMasterManager();
            //    DataTable dt = objUserMasterManager.User_Details(uid);
            //    if (dt.Rows.Count > 0)
            //    {

            //        string reportPath = Server.MapPath("~") + "Report\\Report.rpt";

            //        ReportDocument report = new ReportDocument();
            //        report.Load(reportPath);
            //        report.SetDataSource(dt);
            //        report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "PolicyDocument");
            //    }
            //    else
            //    {
            //        Response.Write("No data found for the given User ID.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex);
            //    Response.Write("An error occurred while generating the report.");
            //}
        }
    }
}