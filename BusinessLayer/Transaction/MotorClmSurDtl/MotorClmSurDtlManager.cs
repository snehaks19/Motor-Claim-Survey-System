using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MotorClmSurDtlManager
    {
        public DataTable returnDropDown(string type)
        {
            string query = $"SELECT CM_CODE || '-' || CM_DESC AS TEXT,CM_CODE AS CODE FROM SNEHA_CODES_MASTER WHERE CM_TYPE='{type}' AND CM_ACTIVE_YN='Y'";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public (int,long, string) SaveDetails(MotorClmSurDtl objMotorClmSurDtl)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict["SURD_UID"] = objMotorClmSurDtl.SurdUid > 0 ? objMotorClmSurDtl.SurdUid : 0;
            dict["SURD_SUR_UID"] = objMotorClmSurDtl.SurdSurUid; 
            dict["ITEM_CODE"] = objMotorClmSurDtl.SurdItemCode;
            dict["TYPE"] = objMotorClmSurDtl.SurdType;
            dict["UNIT_PRICE"] = objMotorClmSurDtl.SurdUnitPrice;
            dict["QUANTITY"] = objMotorClmSurDtl.SurdQuantity;
            dict["FC_AMT"] = objMotorClmSurDtl.SurdFcAmt;
            dict["LC_AMT"] = objMotorClmSurDtl.SurdLcAmt;
            dict["SUR_CR_BY"] = objMotorClmSurDtl.SurdCrBy;
            dict["SUR_CR_BY"] = objMotorClmSurDtl.SurdCrBy;
            

            (int status,long uid, string errMsg) = DBConnection.ExecuteProc(dict);

            return (status,uid, errMsg);
        }

        public DataTable LoadDetails(string surdUid, string surUid)
        {
            try
            {
                string selectQuery = $@"SELECT
                                            SURD_UID,
                                            SURD_SUR_UID,
                                            SURD_ITEM_CODE,
                                            SURD_TYPE,
                                            SURD_UNIT_PRICE,
                                            SURD_QUANTITY,
                                            SURD_FC_AMT,
                                            SURD_LC_AMT
                                            
                                        FROM
                                            MOTOR_CLM_SUR_DTL
                                        WHERE
                                            SURD_SUR_UID = '{surUid}' and SURD_UID = '{surdUid}'";
                DataTable dt = DBConnection.ExecuteDataset(selectQuery);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }

        }

        public long getSurdUid(long number)
        {
            string query = $"SELECT SURD_UID FROM MOTOR_CLM_SUR_DTL WHERE SURD_SUR_UID={number}";
            long surUid = Convert.ToInt64(DBConnection.ExecuteScalar(query));
            return surUid;
        }

        public DataTable SurveyDetailsDataReport(int surUid)
        {
            string Query = $"SELECT (SELECT CM_DESC FROM SNEHA_CODES_MASTER WHERE CM_CODE=SURD_ITEM_CODE) AS SURD_ITEM_CODE,(SELECT CM_DESC FROM SNEHA_CODES_MASTER WHERE CM_CODE=SURD_TYPE) AS SURD_TYPE,SURD_UNIT_PRICE,SURD_FC_AMT,SURD_LC_AMT FROM MOTOR_CLM_SUR_DTL " +
                $"WHERE SURD_SUR_UID ={surUid}";
            DataTable dt = DBConnection.ExecuteDataset(Query);

            return dt;
        }
        public int CheckDuplicate(string item, long surUid)
        {
            string query = $"SELECT COUNT(*) FROM MOTOR_CLM_SUR_DTL WHERE SURD_ITEM_CODE='{item}' AND SURD_SUR_UID={surUid}";
            object rows = DBConnection.ExecuteScalar(query);
            return Convert.ToInt32(rows);
        }


    }
}
