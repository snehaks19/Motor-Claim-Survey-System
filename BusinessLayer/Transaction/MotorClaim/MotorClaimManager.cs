using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class MotorClaimManager
    {
        public DataTable returnDropDown()
        {
            string query = $"SELECT POL_NO FROM MOTOR_POLICY WHERE POL_APPR_STATUS='A' ORDER BY POL_NO DESC";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }
        public DataTable returndropdown(string type)
        {
            string query = $"SELECT CM_CODE || '-' || CM_DESC AS TEXT,CM_CODE AS CODE FROM SNEHA_CODES_MASTER WHERE CM_TYPE='{type}' AND CM_ACTIVE_YN='Y'";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public DataTable returndropdown(string vehmodel, string vehmake)
        {
            string query = $"SELECT CM_CODE || '-' || CM_DESC AS TEXT,CM_CODE AS CODE FROM SNEHA_CODES_MASTER WHERE CM_TYPE='{vehmodel}' AND CM_PARENT_CODE='{vehmake}' AND CM_ACTIVE_YN='Y'";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public DataTable LoadPolicyDetails(string polNo)
        {
            try
            {
                string query = $"SELECT POL_FM_DT,POL_TO_DT,POL_ASSR_NAME,POL_ASSR_MOBILE,POL_VEH_MAKE,POL_VEH_MODEL" +
                        $",POL_VEH_CHASSIS_NO,POL_VEH_ENGINE_NO,POL_VEH_REGN_NO,POL_VEH_VALUE FROM MOTOR_POLICY WHERE POL_NO='{polNo}'";
                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int AddClaim(MotorClaim objMotorClaim)
        {
            try
            {
                string query = "SELECT seq_clm_uid.NEXTVAL FROM dual";
                int clmUid = Convert.ToInt32(DBConnection.ExecuteScalar(query));

                string claimNumber = $"C/{DateTime.Now.Year}/{clmUid.ToString().PadLeft(5, '0')}";

                Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "CLM_UID",  clmUid},
                { "CLM_NO", claimNumber },
                { "CLM_POL_NO", objMotorClaim.ClmPolNo },
                { "CLM_POL_FM_DT", objMotorClaim.ClmPolFmDt},
                { "CLM_POL_TO_DT", objMotorClaim.ClmPolToDt },
                { "CLM_POL_ASSR_NAME", objMotorClaim.ClmPolAssrName },
                { "CLM_POL_ASSR_MOB", objMotorClaim.ClmPolAssrMob},
                { "CLM_LOSS_DT", objMotorClaim.ClmLossDt },
                { "CLM_INTM_DT", objMotorClaim.ClmIntmDt },
                { "CLM_REG_DT", objMotorClaim.ClmRegDt },
                { "CLM_POL_REP_NO", objMotorClaim.ClmPolRepNo },
                { "CLM_POL_REP_DTL", objMotorClaim.ClmPolRepDtl },
                { "CLM_LOSS_DESC", objMotorClaim.ClmLossDesc},
                { "CLM_VEH_MAKE", objMotorClaim.ClmVehMake},
                { "CLM_VEH_MODEL", objMotorClaim.ClmVehModel},
                { "CLM_VEH_CHASSIS_NO", objMotorClaim.ClmVehChassisNo},
                { "CLM_VEG_ENGINE_NO", objMotorClaim.ClmVehEngineNo},
                { "CLM_VEH_REGN_NO", objMotorClaim.ClmVehRegnNo },
                { "CLM_VEH_VALUE", objMotorClaim.ClmVehValue },
                { "CLM_SUR_CR_YN", "N" },
                { "CLM_SUR_APPR_YN", "N" },
                { "CLM_APPR_STATUS", "N" },
                { "CLM_CR_BY", objMotorClaim.ClmCrBy },
                { "CLM_CR_DT", objMotorClaim.ClmCrDt}
            };

                string insertQuery = @"
            INSERT INTO MOTOR_CLAIM (CLM_UID, CLM_NO, CLM_POL_NO, CLM_POL_FM_DT, CLM_POL_TO_DT, 
                CLM_POL_ASSR_NAME, CLM_POL_ASSR_MOB, CLM_LOSS_DT, CLM_INTM_DT, CLM_REG_DT, 
                CLM_POL_REP_NO, CLM_POL_REP_DTL, CLM_LOSS_DESC, CLM_VEH_MAKE, CLM_VEH_MODEL, 
                CLM_VEH_CHASSIS_NO, CLM_VEG_ENGINE_NO, CLM_VEH_REGN_NO, CLM_VEH_VALUE, 
                CLM_SUR_CR_YN, CLM_SUR_APPR_YN, CLM_APPR_STATUS,CLM_CR_BY,CLM_CR_DT) 
            VALUES (:CLM_UID, :CLM_NO, :CLM_POL_NO, :CLM_POL_FM_DT, :CLM_POL_TO_DT, 
                :CLM_POL_ASSR_NAME, :CLM_POL_ASSR_MOB, :CLM_LOSS_DT, :CLM_INTM_DT, :CLM_REG_DT, 
                :CLM_POL_REP_NO, :CLM_POL_REP_DTL, :CLM_LOSS_DESC, :CLM_VEH_MAKE, :CLM_VEH_MODEL,
                :CLM_VEH_CHASSIS_NO, :CLM_VEG_ENGINE_NO, :CLM_VEH_REGN_NO, :CLM_VEH_VALUE,
                :CLM_SUR_CR_YN, :CLM_SUR_APPR_YN, :CLM_APPR_STATUS,:CLM_CR_BY,:CLM_CR_DT)";


                int rows = DBConnection.ExecuteQuery(dict, insertQuery);
                if (rows > 0)
                {
                    return clmUid;
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

        public DataTable LoadDetails(int claimUid)
        {
            try
            {
                string selectQuery = $"SELECT CLM_NO, CLM_POL_NO, CLM_POL_FM_DT, CLM_POL_TO_DT," +
                    $"CLM_POL_ASSR_NAME, CLM_POL_ASSR_MOB, CLM_LOSS_DT, CLM_INTM_DT," +
                    $"CLM_REG_DT, CLM_POL_REP_NO, CLM_POL_REP_DTL, CLM_LOSS_DESC," +
                    $"CLM_VEH_MAKE, CLM_VEH_MODEL, CLM_VEH_CHASSIS_NO, " +
                    $"CLM_VEG_ENGINE_NO, CLM_VEH_REGN_NO, CLM_VEH_VALUE,CLM_SUR_CR_YN,CLM_SUR_NO FROM MOTOR_CLAIM WHERE CLM_UID = {claimUid}";
                DataTable dt = DBConnection.ExecuteDataset(selectQuery);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }
            
        }

        public int UpdateClaim(MotorClaim objMotorClaim)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "CLM_UID",  objMotorClaim.ClmUid},
                { "CLM_NO", objMotorClaim.ClmNo },
                { "CLM_POL_NO", objMotorClaim.ClmPolNo },
                { "CLM_POL_FM_DT", objMotorClaim.ClmPolFmDt},
                { "CLM_POL_TO_DT", objMotorClaim.ClmPolToDt },
                { "CLM_POL_ASSR_NAME", objMotorClaim.ClmPolAssrName },
                { "CLM_POL_ASSR_MOB", objMotorClaim.ClmPolAssrMob},
                { "CLM_LOSS_DT", objMotorClaim.ClmLossDt },
                { "CLM_INTM_DT", objMotorClaim.ClmIntmDt },
                { "CLM_REG_DT", objMotorClaim.ClmRegDt },
                { "CLM_POL_REP_NO", objMotorClaim.ClmPolRepNo },
                { "CLM_POL_REP_DTL", objMotorClaim.ClmPolRepDtl },
                { "CLM_LOSS_DESC", objMotorClaim.ClmLossDesc},
                { "CLM_VEH_MAKE", objMotorClaim.ClmVehMake},
                { "CLM_VEH_MODEL", objMotorClaim.ClmVehModel},
                { "CLM_VEH_CHASSIS_NO", objMotorClaim.ClmVehChassisNo},
                { "CLM_VEG_ENGINE_NO", objMotorClaim.ClmVehEngineNo},
                { "CLM_VEH_REGN_NO", objMotorClaim.ClmVehRegnNo },
                { "CLM_VEH_VALUE", objMotorClaim.ClmVehValue },
                { "CLM_SUR_CR_YN", "N" },
                { "CLM_SUR_APPR_YN", "N" },
                { "CLM_APPR_STATUS", "N" },
                { "CLM_UP_BY", objMotorClaim.ClmUpBy },
                { "CLM_UP_DT", objMotorClaim.ClmUpDt }
            };

                string updateQuery = @"
            UPDATE MOTOR_CLAIM
            SET 
                CLM_NO = :CLM_NO,
                CLM_POL_NO = :CLM_POL_NO,
                CLM_POL_FM_DT = :CLM_POL_FM_DT,
                CLM_POL_TO_DT = :CLM_POL_TO_DT,
                CLM_POL_ASSR_NAME = :CLM_POL_ASSR_NAME,
                CLM_POL_ASSR_MOB = :CLM_POL_ASSR_MOB,
                CLM_LOSS_DT = :CLM_LOSS_DT,
                CLM_INTM_DT = :CLM_INTM_DT,
                CLM_REG_DT = :CLM_REG_DT,
                CLM_POL_REP_NO = :CLM_POL_REP_NO,
                CLM_POL_REP_DTL = :CLM_POL_REP_DTL,
                CLM_LOSS_DESC = :CLM_LOSS_DESC,
                CLM_VEH_MAKE = :CLM_VEH_MAKE,
                CLM_VEH_MODEL = :CLM_VEH_MODEL,
                CLM_VEH_CHASSIS_NO = :CLM_VEH_CHASSIS_NO,
                CLM_VEG_ENGINE_NO = :CLM_VEG_ENGINE_NO,
                CLM_VEH_REGN_NO = :CLM_VEH_REGN_NO,
                CLM_VEH_VALUE = :CLM_VEH_VALUE,
                CLM_SUR_CR_YN = :CLM_SUR_CR_YN,
                CLM_SUR_APPR_YN = :CLM_SUR_APPR_YN,
                CLM_APPR_STATUS = :CLM_APPR_STATUS,
                CLM_UP_BY= :CLM_UP_BY,
                CLM_UP_DT = :CLM_UP_DT
            WHERE CLM_UID = :CLM_UID";

                int rows = DBConnection.ExecuteQuery(dict, updateQuery);
                return rows;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public DataTable LoadClaimListingGrid()
        {
            string query = $" SELECT CLM_UID,CLM_NO, CLM_POL_NO,TO_CHAR(CLM_POL_FM_DT, 'DD-MM-YYYY') AS CLM_POL_FM_DT, " +
                $"TO_CHAR(CLM_POL_TO_DT, 'DD-MM-YYYY') AS CLM_POL_TO_DT, CLM_POL_ASSR_NAME,  CLM_POL_ASSR_MOB, " +
                $"TO_CHAR(CLM_LOSS_DT, 'DD-MM-YYYY') AS CLM_LOSS_DT, TO_CHAR(CLM_INTM_DT, 'DD-MM-YYYY') AS CLM_INTM_DT, " +
                $"TO_CHAR(CLM_REG_DT, 'DD-MM-YYYY') AS CLM_REG_DT,CLM_POL_REP_NO,CLM_POL_REP_DTL,CLM_LOSS_DESC," +
                $"(SELECT C.CM_DESC FROM SNEHA_CODES_MASTER C WHERE C.CM_CODE = CLM_VEH_MAKE) AS CLM_VEH_MAKE, " +
                $"(SELECT C.CM_DESC FROM SNEHA_CODES_MASTER C WHERE C.CM_CODE = CLM_VEH_MODEL) AS CLM_VEH_MODEL," +
                $"CLM_VEH_CHASSIS_NO,  CLM_VEG_ENGINE_NO,  CLM_VEH_REGN_NO, CLM_VEH_VALUE ,CLM_SUR_CR_YN," +
                $"CASE " +
                $"WHEN CLM_APPR_STATUS = 'A' THEN 'Approved' " +
                $"WHEN CLM_APPR_STATUS = 'R' THEN 'Rejected' " +
                $"END AS CLM_APPR_STATUS " +
                $" FROM  MOTOR_CLAIM ORDER BY CLM_UID DESC";

            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        
        }

        public (int,string) SurveyRequest(long clmUid ,string userId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict["CLMUID"] = clmUid;
            dict["USERID"] = userId;
           
            (int status,string errMsg) = DBConnection.ExecuteProc(clmUid, userId);
           
            return (status,errMsg);
        }

        public long getSurUid(string number)
        {
            string query = $"SELECT SUR_UID FROM MOTOR_CLM_SUR_HDR WHERE SUR_NO='{number}'";
            long surUid = Convert.ToInt64(DBConnection.ExecuteScalar(query));
            return surUid;
        }

        public string FindSurveyStatus(string clmNo)
        {
            string query = $"SELECT SUR_STATUS FROM  MOTOR_CLM_SUR_HDR WHERE SUR_CLM_NO='{clmNo}'";
            object status = DBConnection.ExecuteScalar(query);
            if (status!=null)
            {
                if (status.ToString() == "S")
                {
                    return status.ToString();
                }
                else
                {
                    return status.ToString();
                }
                
            }
            else
            {
                return "0";
            }
        }

        public string FindClaimStatus(string clmNo)
        {
            string query = $"SELECT CLM_APPR_STATUS FROM MOTOR_CLAIM WHERE CLM_NO ='{clmNo}'";
            object status = DBConnection.ExecuteScalar(query);
            if (status != null)
            {
                if (status.ToString() == "A" || (status.ToString() == "R"))
                {
                    return status.ToString();
                }
                else
                {
                    return status.ToString();
                }

            }
            else
            {
                return "0";
            }
        }

        public int ClaimApproved(long clmUid)
        {
            string query = $"UPDATE MOTOR_CLAIM SET CLM_APPR_STATUS='A' WHERE CLM_UID={clmUid}";
            int rows = DBConnection.ExecuteQuery(query);
            return rows;
        }

        public int ClaimRejected(long clmUid)
        {
            string query = $"UPDATE MOTOR_CLAIM SET CLM_APPR_STATUS='R' WHERE CLM_UID={clmUid}";
            int rows = DBConnection.ExecuteQuery(query);
            return rows;
        }

        public DataTable ClaimDataReport(int uid)
        {
            string selectquery = $"SELECT CLM_NO AS CLM_NO,TO_CHAR(CLM_LOSS_DT, 'DD-MM-YYYY') AS CLM_LOSS_DT,TO_CHAR(CLM_INTM_DT, 'DD-MM-YYYY') AS CLM_INTM_DT, TO_CHAR(CLM_REG_DT, 'DD-MM-YYYY') AS CLM_REG_DT, CLM_LOSS_DESC FROM MOTOR_CLAIM WHERE CLM_UID = '{uid}'";
            DataTable dt = new DataTable();
            dt = DBConnection.ExecuteDataset(selectquery);
            return dt;
        }

        public int CheckDuplicate(string reptNo)
        {
            string query = $"SELECT COUNT(*) FROM MOTOR_CLAIM WHERE CLM_POL_REP_NO='{reptNo}'";
            int res = Convert.ToInt32(DBConnection.ExecuteScalar(query));
            return res;
        }
        public string GetReportNo(string clmUid)
        {
            string query = $"SELECT CLM_POL_REP_NO FROM MOTOR_CLAIM WHERE CLM_UID='{clmUid}'";
            string reptNo = DBConnection.ExecuteScalar(query).ToString();
            return reptNo;
        }
    }
}

