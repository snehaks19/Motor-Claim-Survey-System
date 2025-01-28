using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ErrorCodeMasterManager
    {
        public DataTable LoadGrid()
        {
            try
            {
                string query = "SELECT ERR_CODE, ERR_TYPE, ERR_DESC FROM SNEHA_ERROR_CODE_MASTER ORDER BY ERR_CODE,ERR_TYPE";
                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int Delete(string errCode, string errType)
        {
            try
            {
                string query = $"DELETE FROM SNEHA_ERROR_CODE_MASTER WHERE ERR_CODE='{errCode}' AND ERR_TYPE='{errType}'";
                int rows = DBConnection.ExecuteQuery(query);
                return rows;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int AddNewErrorCodeMasterValue(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                string insertQuery = $"INSERT INTO SNEHA_ERROR_CODE_MASTER (ERR_CODE, ERR_TYPE, ERR_DESC, ERR_CR_BY, ERR_CR_DT)" +
                        $" VALUES (:ErrCode,:ErrType,:ErrDesc,:ErrCrBy,:ErrCrDt)";


                Dictionary<string, object> dict = new Dictionary<string, object>
{
                { "ErrCode", objErrorCodeMaster.ErrCode.ToUpper().Trim() },
                { "ErrType", objErrorCodeMaster.ErrType.ToUpper() },
                { "ErrDesc", objErrorCodeMaster.ErrDesc },
                { "ErrCrBy", objErrorCodeMaster.ErrCrBy },
                { "ErrCrDt", objErrorCodeMaster.ErrCrDt }
            };

                int rows = DBConnection.ExecuteQuery(dict, insertQuery);
                return rows;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public DataTable LoadDetails(string errCode, string errType)
        {
            try
            {
                string query = $"SELECT ERR_CODE,ERR_TYPE,ERR_DESC FROM SNEHA_ERROR_CODE_MASTER WHERE ERR_CODE='{errCode}' AND ERR_TYPE='{errType}'";
                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int UpdateErrorCodeMasterValue(ErrorCodeMaster objErrorCodeMaster)

        {
            try
            {
                string updateQuery = $"UPDATE SNEHA_ERROR_CODE_MASTER SET ERR_DESC='{objErrorCodeMaster.ErrDesc}',ERR_UP_BY='{objErrorCodeMaster.ErrUpBy}',ERR_UP_DT=TO_DATE('{objErrorCodeMaster.ErrUpDt:dd-MM-yyyy}', 'DD-MM-YYYY') where ERR_CODE='{objErrorCodeMaster.ErrCode}' AND ERR_TYPE='{objErrorCodeMaster.ErrType}'";
                int rows = DBConnection.ExecuteQuery(updateQuery);
                return rows;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public int CheckDuplicate(ErrorCodeMaster objErrorCodeMaster)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM SNEHA_ERROR_CODE_MASTER WHERE ERR_CODE='{objErrorCodeMaster.ErrCode}'";
                Object rows = DBConnection.ExecuteScalar(query);
                int row = Convert.ToInt32(rows);
                return row;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

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

        public DataTable returndropdown(string type, string vehmake)
        {
            try
            {
                string query = $"SELECT CM_CODE || '-' || CM_DESC AS TEXT,CM_CODE AS CODE FROM CODES_MASTER WHERE CM_TYPE='{type}' AND CM_PARENT_CODE='{vehmake}' AND CM_ACTIVE_YN='Y'";
                DataTable dt = DBConnection.ExecuteDataset(query);
                return dt;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public string GetErrorMsg(string code)
        {
            try
            {
                string query = $"SELECT ERR_DESC FROM SNEHA_ERROR_CODE_MASTER WHERE ERR_CODE='{code}'";
                string desc = DBConnection.ExecuteScalar(query).ToString();
                return desc;
            }
            catch (Exception err)
            {

                throw err;
            }

        }

        public string GetMessage(string str)
        {
            try
            {
                string query = $"SELECT ERR_DESC FROM SNEHA_ERROR_CODE_MASTER WHERE ERR_CODE='{str}'";
                string result = DBConnection.ExecuteScalar(query).ToString().Trim();
                return result;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

    }
}
