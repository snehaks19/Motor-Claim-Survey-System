using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MotorClmSurHdrManager
    {
        public DataTable LoadGrid()
        {
            string query = $@"
            SELECT 
                SUR_UID,         
                SUR_CLM_NO, 
                SUR_NO, 
                TO_CHAR(SUR_DATE, 'DD-MM-YYYY') AS SUR_DATE,
               (SELECT C.CM_DESC FROM SNEHA_CODES_MASTER C WHERE C.CM_CODE = S.SUR_LOCATION ) AS SUR_LOCATION,
                SUR_CHASSIS_NO, 
                SUR_REGN_NO, 
                SUR_ENGINE_NO,
                CASE 
                    WHEN SUR_STATUS = 'P' THEN 'PENDING'
                    WHEN SUR_STATUS = 'S' THEN 'SUBMITTED'
                   
                END AS SUR_STATUS,
                CASE 
                    WHEN SUR_APPR_STS = 'A' THEN 'APPROVED'
                    WHEN SUR_APPR_STS = 'R' THEN 'REJECTED'
                    WHEN SUR_APPR_STS = 'N' THEN 'NOT APPROVED'
                    
                END AS SUR_APPR_STS,          
                SUR_CURR, 
                SUR_FC_AMT,
                SUR_LC_AMT       
            FROM 
                MOTOR_CLM_SUR_HDR S
            ORDER BY 
                SUR_UID DESC";

            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public DataTable LoadDetails(int surUid)
        {
            try
            {
                string query = $"SELECT SUR_UID,SUR_NO, SUR_CLM_NO, SUR_DATE, " +
                $" SUR_LOCATION, SUR_CHASSIS_NO, SUR_REGN_NO, SUR_ENGINE_NO, SUR_CURR, " +
                $"SUR_FC_AMT, SUR_LC_AMT,TO_CHAR(SUR_CR_DT,'DD-MM-YYYY') AS SUR_CR_DT FROM MOTOR_CLM_SUR_HDR WHERE SUR_UID = {surUid}";

                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public DataTable returndropdown(string type)
        {
            string query = $"SELECT CM_CODE || '-' || CM_DESC AS TEXT,CM_CODE AS CODE FROM SNEHA_CODES_MASTER WHERE CM_TYPE='{type}' AND CM_ACTIVE_YN='Y'";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public int SaveSurvey(MotorClmSurHdr objMotorClmSurHdr)
        {

            string updateQuery = $"UPDATE MOTOR_CLM_SUR_HDR SET " +
                     $"SUR_DATE = TO_DATE('{objMotorClmSurHdr.SurDate:dd-MM-yyyy}', 'DD-MM-YYYY'), " +
                     $"SUR_LOCATION = '{objMotorClmSurHdr.SurLocation}', " +
                     $"SUR_CURR = '{objMotorClmSurHdr.SurCurr}', " +
                     $"SUR_UP_BY = '{objMotorClmSurHdr.SurCrBy}', " +
                     $"SUR_UP_DT = SYSDATE " +
                     $"WHERE SUR_UID = '{objMotorClmSurHdr.SurUid}'";

            int rows = DBConnection.ExecuteQuery(updateQuery);
            if (rows > 0)
            {
                return rows;
            }
            else
            {
                return 0;
            }
        }

        public DataTable LoadSurveyDetailsGrid(string surUid)
        {
            string query = $@" SELECT
                SURD_UID,
                SURD_SUR_UID,
                (SELECT C.CM_DESC FROM SNEHA_CODES_MASTER C WHERE C.CM_CODE = M.SURD_ITEM_CODE ) AS SURD_ITEM_CODE,
                CASE 
                    WHEN SURD_TYPE = 'R' THEN 'Repair'
                    WHEN SURD_TYPE = 'P' THEN 'Replace'
                END AS SURD_TYPE,
                SURD_UNIT_PRICE,
                SURD_QUANTITY,
                SURD_FC_AMT,
                SURD_LC_AMT ,
                (SELECT SUR_STATUS FROM MOTOR_CLM_SUR_HDR WHERE SUR_UID={surUid} )AS SURD_STATUS
            FROM
                MOTOR_CLM_SUR_DTL M
            WHERE SURD_SUR_UID='{surUid}'
            ORDER BY SURD_UID ";

            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;

        }

        public int Delete(string surdUid, string surUid)
        {
            try
            {
                string deleteQuery = $"DELETE FROM MOTOR_CLM_SUR_DTL WHERE SURD_UID = {surdUid} AND SURD_SUR_UID={surUid}";
                int rows = DBConnection.ExecuteQuery(deleteQuery);

                if (rows >= 1)
                {
                    string updateQuery = $"UPDATE MOTOR_CLM_SUR_HDR SET " +
                         $"SUR_FC_AMT = (SELECT SUM(SURD_FC_AMT) FROM MOTOR_CLM_SUR_DTL WHERE SURD_SUR_UID = {surUid}), " +
                         $"SUR_LC_AMT = (SELECT SUM(SURD_LC_AMT) FROM MOTOR_CLM_SUR_DTL WHERE SURD_SUR_UID = {surUid}) " +
                         $"WHERE SUR_UID = {surUid}";

                    int row = DBConnection.ExecuteQuery(updateQuery);

                    if (row >= 1)
                    {
                        return row;
                    }
                    else
                        return 0;
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

        public int LoadDetailsTable(int surUid)
        {
            string query = $"SELECT COUNT(*) FROM MOTOR_CLM_SUR_DTL WHERE SURD_SUR_UID={surUid}";
            int row = Convert.ToInt32(DBConnection.ExecuteScalar(query));
            if (row > 0)
            {
                return row;
            }
            else
            {
                return 0;
            }
            
        }

        public (int,string) Approve(long surUid ,long clmUid,string userId)
        {
            (int status, string errMsg) = DBConnection.ExecuteProcForApproval(surUid, clmUid, userId);
            return (status, errMsg);
        }

        public int FetchClmUid(long surUid)
        {
            string query = $"SELECT SUR_CLM_UID FROM MOTOR_CLM_SUR_HDR WHERE SUR_UID={surUid}";
            int clmuid = Convert.ToInt32(DBConnection.ExecuteScalar(query));
            return clmuid;
        }

        public int FetchSurveyStatus(int surUid)
        {
            string query = $"SELECT SUR_STATUS FROM MOTOR_CLM_SUR_HDR WHERE SUR_UID={surUid}";
            string status = DBConnection.ExecuteScalar(query).ToString();

            if (status == "S")
                return 1;
            else
                return 0;

        }

        public long getClmUid(string clmNo)
        {
            string query = $"SELECT SUR_CLM_UID FROM MOTOR_CLM_SUR_HDR WHERE SUR_CLM_NO='{clmNo}'";
            long clmUid = Convert.ToInt64(DBConnection.ExecuteScalar(query));
            return clmUid;
        }

        public DataTable SurveyDataReport(int surUid)
        {
            string Query = $"SELECT SUR_NO,TO_CHAR(SUR_DATE, 'DD-MM-YYYY') AS SUR_DATE,(SELECT CM_DESC FROM SNEHA_CODES_MASTER WHERE CM_CODE=SUR_LOCATION) AS SUR_LOCATION,SUR_CURR,SUR_FC_AMT,SUR_LC_AMT FROM MOTOR_CLM_SUR_HDR " +
                $"WHERE SUR_UID ={surUid}";
            DataTable dt = DBConnection.ExecuteDataset(Query);

            return dt;
        }


       
    }
}

