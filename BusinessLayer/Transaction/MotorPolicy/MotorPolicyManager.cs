using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MotorPolicyManager
    {
        public DataTable returndropdown(string type)
        {
            try
            {
                string query = $"SELECT CM_CODE || '-' || CM_DESC AS TEXT,CM_CODE AS CODE FROM SNEHA_CODES_MASTER WHERE CM_TYPE='{type}' AND CM_ACTIVE_YN='Y'";
                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public DataTable returndropdown(string vehmodel,string vehmake)
        {
            string query = $"SELECT CM_CODE || '-' || CM_DESC AS TEXT,CM_CODE AS CODE FROM SNEHA_CODES_MASTER WHERE CM_TYPE='{vehmodel}' AND CM_PARENT_CODE='{vehmake}' AND CM_ACTIVE_YN='Y'";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public int AddPolicy(MotorPolicy objMotorPolicy)
        {

            try
            {
                string query = "SELECT seq_pol_uid.NEXTVAL FROM dual";

                int polUid = Convert.ToInt32(DBConnection.ExecuteScalar(query));
                objMotorPolicy.PolUid = polUid;

                string policyNumber = $"P/{DateTime.Now.Year}/{polUid.ToString().PadLeft(5, '0')}";
                objMotorPolicy.PolNo = policyNumber;

                Dictionary<string, object> dict = new Dictionary<string, object>
                {
                    { "POL_UID", objMotorPolicy.PolUid },
                    { "POL_NO", objMotorPolicy.PolNo },
                    { "POL_ISS_DT", objMotorPolicy.PolIssDt },
                    { "POL_FM_DT", objMotorPolicy.PolFmDt },
                    { "POL_TO_DT", objMotorPolicy.PolToDt },
                    { "POL_ASSR_NAME", objMotorPolicy.PolAssrName },
                    { "POL_ASSR_MOBILE", objMotorPolicy.PolAssrMobile },
                    { "POL_CURR_CODE", objMotorPolicy.PolCurrCode },
                    { "POL_GROSS_FC_PREM", objMotorPolicy.PolGrossFcPrem },
                    { "POL_GROSS_LC_PREM", objMotorPolicy.PolGrossLcPrem },
                    { "POL_VEH_MAKE", objMotorPolicy.PolVehMake },
                    { "POL_VEH_MODEL", objMotorPolicy.PolVehModel },
                    { "POL_VEH_CHASSIS_NO", objMotorPolicy.PolVehChassisNo },
                    { "POL_VEH_ENGINE_NO", objMotorPolicy.PolVehEngineNo },
                    { "POL_VEH_REGN_NO", objMotorPolicy.PolVehRegnNo },
                    { "POL_VEH_VALUE", objMotorPolicy.PolVehValue },
                    { "POL_APPR_STATUS", objMotorPolicy.PolApprStatus },
                    { "POL_CR_BY", objMotorPolicy.PolCrBy },
                    { "POL_CR_DT", objMotorPolicy.PolCrDt }

                };

                string insertQuery = @"INSERT INTO MOTOR_POLICY (
                    POL_UID, POL_NO, POL_ISS_DT, POL_FM_DT, POL_TO_DT, POL_ASSR_NAME, 
                    POL_ASSR_MOBILE, POL_CURR_CODE, POL_GROSS_FC_PREM, POL_GROSS_LC_PREM, 
                    POL_VEH_MAKE, POL_VEH_MODEL, POL_VEH_CHASSIS_NO, POL_VEH_ENGINE_NO, 
                    POL_VEH_REGN_NO,POL_VEH_VALUE, POL_APPR_STATUS, POL_CR_BY, POL_CR_DT
                ) VALUES (
                    :POL_UID, :POL_NO, :POL_ISS_DT, :POL_FM_DT, :POL_TO_DT, :POL_ASSR_NAME, 
                    :POL_ASSR_MOBILE, :POL_CURR_CODE, :POL_GROSS_FC_PREM, :POL_GROSS_LC_PREM, 
                    :POL_VEH_MAKE, :POL_VEH_MODEL, :POL_VEH_CHASSIS_NO, :POL_VEH_ENGINE_NO, 
                    :POL_VEH_REGN_NO,:POL_VEH_VALUE, :POL_APPR_STATUS, :POL_CR_BY, :POL_CR_DT
                )";

                int rows = DBConnection.ExecuteQuery(dict, insertQuery);
                if (rows > 0)
                {
                    return polUid;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception err)
            {

                throw err;
            }

        }

        public DataTable LoadDetails(int polUid)
        {
            try
            {
                string query = $"SELECT POL_NO, POL_ISS_DT,POL_FM_DT, POL_TO_DT, POL_ASSR_NAME, POL_ASSR_MOBILE, POL_CURR_CODE," +
                        $"POL_GROSS_FC_PREM, POL_GROSS_LC_PREM, POL_VEH_MAKE, POL_VEH_MODEL,POL_VEH_CHASSIS_NO, POL_VEH_ENGINE_NO," +
                        $" POL_VEH_REGN_NO, POL_VEH_VALUE,POL_APPR_STATUS FROM MOTOR_POLICY WHERE POL_UID={polUid}";
                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }

        }

        public DataTable LoadPolicyListingGrid()
        {

            string query = $@"
    SELECT 
        POL_UID,
        POL_NO, 
        TO_CHAR(POL_ISS_DT, 'DD-MM-YYYY') AS POL_ISS_DT, 
        TO_CHAR(POL_FM_DT, 'DD-MM-YYYY') AS POL_FM_DT, 
        TO_CHAR(POL_TO_DT, 'DD-MM-YYYY') AS POL_TO_DT, 
        POL_ASSR_NAME, 
        POL_ASSR_MOBILE, 
        POL_CURR_CODE, 
        POL_GROSS_FC_PREM,  
        POL_GROSS_LC_PREM, 
        (SELECT C.CM_DESC FROM SNEHA_CODES_MASTER C WHERE C.CM_CODE = P.POL_VEH_MAKE ) AS POL_VEH_MAKE, 
        (SELECT C.CM_DESC FROM SNEHA_CODES_MASTER C WHERE C.CM_CODE = P.POL_VEH_MODEL ) AS POL_VEH_MODEL, 
        POL_VEH_CHASSIS_NO, 
        POL_VEH_ENGINE_NO, 
        POL_VEH_REGN_NO, 
        POL_VEH_VALUE, 
        CASE 
            WHEN POL_APPR_STATUS = 'A' THEN 'Approved' 
            WHEN POL_APPR_STATUS = 'N' THEN 'Not Approved' 
            ELSE 'Unknown' 
        END AS POL_APPR_STATUS
         
        FROM 
            MOTOR_POLICY P 
        ORDER BY 
            POL_UID DESC";


            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public int DeletePolicy(string polNo)
        {
            string deleteQuery = $"DELETE FROM MOTOR_POLICY WHERE POL_NO= '{polNo}'";
            int rows = DBConnection.ExecuteQuery(deleteQuery);
            return rows;
        }

        public int UpdatePolicy(MotorPolicy objMotorPolicy)

        {

            string updateQuery = $@"UPDATE MOTOR_POLICY SET 
            Pol_Iss_Dt = TO_DATE('{objMotorPolicy.PolIssDt.ToString("dd-MM-yyyy")}', 'dd-MM-yyyy'),
            Pol_Fm_Dt = TO_DATE('{objMotorPolicy.PolFmDt.ToString("dd-MM-yyyy")}', 'dd-MM-yyyy'),
            Pol_To_Dt = TO_DATE('{objMotorPolicy.PolToDt.ToString("dd-MM-yyyy")}', 'dd-MM-yyyy'),
            Pol_Assr_Name = '{objMotorPolicy.PolAssrName}',
            Pol_Assr_Mobile = '{objMotorPolicy.PolAssrMobile}',
            Pol_Curr_Code = '{objMotorPolicy.PolCurrCode}',
            Pol_Gross_Fc_Prem = {objMotorPolicy.PolGrossFcPrem},
            Pol_Gross_Lc_Prem = {objMotorPolicy.PolGrossLcPrem},
            Pol_Veh_Make = '{objMotorPolicy.PolVehMake}',
            Pol_Veh_Model = '{objMotorPolicy.PolVehModel}',
            Pol_Veh_Chassis_No = '{objMotorPolicy.PolVehChassisNo}',
            Pol_Veh_Engine_No = '{objMotorPolicy.PolVehEngineNo}',
            Pol_Veh_Regn_No = '{objMotorPolicy.PolVehRegnNo}',
            Pol_Veh_Value = {objMotorPolicy.PolVehValue},
            Pol_Up_By = '{objMotorPolicy.PolUpBy}',
            Pol_Up_Dt = SYSDATE
            WHERE Pol_No = '{objMotorPolicy.PolNo}'";


            int rows = DBConnection.ExecuteQuery(updateQuery);
            return rows;
        }

        public int ApprovePolicy(MotorPolicy objMotorPolicy)
        {
            string updateQuery = $"UPDATE MOTOR_POLICY SET POL_APPR_STATUS='A',POL_APPR_BY='{objMotorPolicy.PolApprBy}'," +
                $"POL_APPR_DT=SYSDATE WHERE POL_UID={objMotorPolicy.PolUid}";
            int rows = DBConnection.ExecuteQuery(updateQuery);
            return rows;
        }
        public int fetchUid(string polNo)
        {
            try
            {
                string query = $"SELECT POL_UID FROM MOTOR_POLICY WHERE POL_NO='{polNo}'";
                object value = DBConnection.ExecuteScalar(query);
                int val = Convert.ToInt32(value);
                return val;
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        public DataTable PolicyDataReport(int polUid)
        {
            string Query = $"SELECT POL_NO,POL_VEH_REGN_NO,TO_CHAR(POL_ISS_DT, 'DD-MM-YYYY') AS POL_ISS_DT,TO_CHAR(POL_FM_DT, 'DD-MM-YYYY') AS POL_FM_DT," +
                $"TO_CHAR(POL_TO_DT, 'DD-MM-YYYY') AS POL_TO_DT,POL_ASSR_NAME,POL_ASSR_MOBILE,(SELECT CM_DESC FROM SNEHA_CODES_MASTER WHERE CM_CODE=POL_VEH_MAKE) AS POL_VEH_MAKE,(SELECT CM_DESC FROM SNEHA_CODES_MASTER WHERE CM_CODE=POL_VEH_MODEL) AS POL_VEH_MODEL,POL_VEH_CHASSIS_NO,POL_VEH_ENGINE_NO,POL_VEH_VALUE FROM MOTOR_POLICY WHERE POL_UID={polUid}";
            DataTable dt = DBConnection.ExecuteDataset(Query);

            return dt;
        }


    }
}
