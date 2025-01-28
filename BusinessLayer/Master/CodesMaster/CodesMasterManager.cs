using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class CodesMasterManager
    {
        public DataTable LoadCodesMasterGrid()
        {
            try
            {
                string query = "SELECT CM_CODE,CM_TYPE,CM_DESC,CM_PARENT_CODE,CM_VALUE,CM_ACTIVE_YN FROM SNEHA_CODES_MASTER ORDER BY CM_CODE,CM_TYPE";
                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int AddNewCodeMasterValue(CodesMaster objCodesMaster)
        {
            try
            {
                string insertQuery = $"INSERT INTO SNEHA_CODES_MASTER (CM_CODE, CM_TYPE, CM_DESC, CM_PARENT_CODE, CM_VALUE, CM_ACTIVE_YN, CM_CR_BY, CM_CR_DT) " +
                             $"VALUES ('{objCodesMaster.CmCode}', '{objCodesMaster.CmType}', '{objCodesMaster.CmDesc}', '{objCodesMaster.CmParentCode}'," +
                             $" '{objCodesMaster.CmValue}', '{objCodesMaster.CmActiveYn}', '{objCodesMaster.CmCrBy}', TO_DATE('{objCodesMaster.CmCrDt:yyyy-MM-dd}', 'YYYY-MM-DD'))";


                Dictionary<string, object> dict = new Dictionary<string, object>
{
                { "CmCode", objCodesMaster.CmCode.ToString() },
                { "CmType", objCodesMaster.CmType.ToString() },
                { "CmDesc", objCodesMaster.CmDesc },
                { "CmParentCode", objCodesMaster.CmParentCode },
                { "CmValue", objCodesMaster.CmValue },
                { "CmActiveYn", objCodesMaster.CmActiveYn },
                { "CmCrBy", objCodesMaster.CmCrBy },
                { "CmCrDt", objCodesMaster.CmCrDt.ToString("yyyy-MM-dd") }
            };

                int rows = DBConnection.ExecuteQuery(dict, insertQuery);
                return rows;

                ////Dictionary<string, object> dict = new Dictionary<string, object>();
                ////dict["cmCode"] = objCodesMaster.CmCode;
                ////dict["cmType"] = objCodesMaster.CmType;
                ////dict["cmDesc"] = objCodesMaster.CmDesc;
                ////dict["cmValue"] = objCodesMaster.CmValue;
                ////dict["ActiveYN"] = objCodesMaster.CmActiveYn;
                ////dict["cmCrBy"] = objCodesMaster.CmCrBy;
                ////dict["cmCrDt"] = objCodesMaster.CmCrBy;

                //string insertQuery = $"INSERT INTO CODES_MASTER(CM_CODE,CM_TYPE,CM_DESC,CM_PARENT_CODE,CM_VALUE,CM_ACTIVE_YN,CM_CR_BY,CM_CR_DT)" +
                //    $" VALUES('{objCodesMaster.CmCode}','{objCodesMaster.CmType}','{objCodesMaster.CmDesc}','{objCodesMaster.CmParentCode}','{objCodesMaster.CmValue}'," +
                //    $" '{objCodesMaster.CmActiveYn}','{objCodesMaster.CmCrBy}','{objCodesMaster.CmCrDt}')";
                //int rows = DBConnection.ExecuteQuery(insertQuery);
                //return rows;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int CheckDuplicateUser(CodesMaster objCodesMaster)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM SNEHA_CODES_MASTER WHERE CM_CODE='{objCodesMaster.CmCode}' AND CM_TYPE='{objCodesMaster.CmType}'";
                Object rows = DBConnection.ExecuteScalar(query);
                int row = Convert.ToInt32(rows);
                return row;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int UpdateCodeMasterValue(CodesMaster objCodesMaster)
        
        {
            try
            {
                string updateQuery = $"UPDATE SNEHA_CODES_MASTER SET CM_VALUE='{objCodesMaster.CmValue}',CM_PARENT_CODE='{objCodesMaster.CmParentCode}', CM_DESC='{objCodesMaster.CmDesc}',CM_ACTIVE_YN='{objCodesMaster.CmActiveYn}',CM_UP_BY='{objCodesMaster.CmUpBy}',CM_UP_DT=TO_DATE('{objCodesMaster.CmUpDt:yyyy-MM-dd}', 'YYYY-MM-DD') where CM_CODE='{objCodesMaster.CmCode}' AND CM_TYPE='{objCodesMaster.CmType}'";
                int rows = DBConnection.ExecuteQuery(updateQuery);
                return rows;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public DataTable LoadDetails(string cmcode,string cmType)
        {
            try
            {
                string query = $"SELECT CM_CODE,CM_TYPE,CM_DESC,CM_VALUE,CM_PARENT_CODE,CM_ACTIVE_YN FROM SNEHA_CODES_MASTER WHERE CM_CODE='{cmcode}' AND CM_TYPE='{cmType}'";
                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int DeleteCodesMaster(string cmCode,string cmType)
        {
            try
            {
                string deleteQuery = $"DELETE FROM SNEHA_CODES_MASTER WHERE CM_CODE='{cmCode}' AND CM_TYPE='{cmType}'";
                int rows = DBConnection.ExecuteQuery(deleteQuery);
                return rows;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public decimal ReturnVal(string currCode)
        {
            try
            {
                string query = $"SELECT CM_VALUE FROM SNEHA_CODES_MASTER WHERE CM_CODE='{currCode}'";
                object s = DBConnection.ExecuteScalar(query);
                return (decimal)s;
            }
            catch (Exception err)
            {

                throw err;
            }

        }
    }
    
}
