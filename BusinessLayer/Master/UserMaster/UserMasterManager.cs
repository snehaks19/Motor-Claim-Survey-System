using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserMasterManager
    {
        public string UserExist(UserMaster ObjUserMaster)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();           

            dict["Userid"] = ObjUserMaster.UserId;
            dict["Password"] = ObjUserMaster.UserPassword;

            string query = "SELECT USER_TYPE FROM USER_MASTER WHERE USER_ID =:Userid AND USER_PASSWORD = :Password AND USER_ACTIVE_YN='Y'";

            DataSet ds = DBConnection.ExecuteQuerySelect(dict, query);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string userType = ds.Tables[0].Rows[0]["USER_TYPE"].ToString();
                return userType;
            }
            else
            {
                return null;
            }
        }

        public DataTable LoadGrid()
        {
            string query = "SELECT USER_ID, USER_NAME, USER_PASSWORD," +
                "CASE WHEN USER_TYPE = 'S' THEN 'Surveyor'" +
                " WHEN USER_TYPE = 'U' THEN 'User' " +
                "END AS USER_TYPE, USER_ACTIVE_YN FROM USER_MASTER ORDER BY USER_ID";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public DataTable returndropdown(string type)
        {
            string query = $"SELECT CM_CODE || '-' || CM_DESC AS TEXT,CM_CODE AS CODE FROM SNEHA_CODES_MASTER WHERE CM_TYPE='{type}' AND CM_ACTIVE_YN='Y'";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public int AddNewUserMasterValue(UserMaster ObjUserMaster)
        {
            string insertQuery = $"INSERT INTO USER_MASTER (USER_ID, USER_TYPE,USER_NAME,USER_PASSWORD, USER_CR_BY, USER_CR_DT,USER_ACTIVE_YN)" +
                $" VALUES (:UserId,:UserType,:UserName,:UserPassword,:UserCrBy,:UserCrDt,:UserActiveYn)";


            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "UserId", ObjUserMaster.UserId },
                { "UserType", ObjUserMaster.UserType },
                { "UserName", ObjUserMaster.UserName },
                { "UserPassword", ObjUserMaster.UserPassword },
                { "UserCrBy", ObjUserMaster.UserCrBy },
                { "UserCrDt", ObjUserMaster.UserCrDt },
                { "UserActiveYn", ObjUserMaster.UserActiveYn }
            };


            int rows = DBConnection.ExecuteQuery(dict, insertQuery);
            return rows;
        }

        public DataTable LoadDetails(string UserId)
        {
            string query = $"SELECT USER_ID, USER_TYPE, USER_NAME,USER_PASSWORD,USER_ACTIVE_YN FROM USER_MASTER WHERE USER_ID='{UserId}'";
            DataTable dt = DBConnection.ExecuteDataset(query);
            return dt;
        }

        public int UpdateUserMasterValue(UserMaster objUserMaster)

        {
            string updateQuery = $"UPDATE USER_MASTER SET USER_NAME='{objUserMaster.UserName}',USER_UP_BY='{objUserMaster.UserUpBy}',USER_ACTIVE_YN='{objUserMaster.UserActiveYn}',USER_UP_DT=TO_DATE('{objUserMaster.UserUpDt:dd-MM-yyyy}', 'DD-MM-YYYY') where USER_ID='{objUserMaster.UserId}' AND USER_TYPE='{objUserMaster.UserType}'";
            int rows = DBConnection.ExecuteQuery(updateQuery);
            return rows;
        }

        public int Delete(string userId)
        {
            string query = $"DELETE FROM USER_MASTER WHERE USER_ID='{userId}'";
            int rows = DBConnection.ExecuteQuery(query);
            return rows;
        }

        public int CheckDuplicateUser(UserMaster objUserMaster)
        {
            string query = $"SELECT COUNT(*) FROM USER_MASTER WHERE USER_ID='{objUserMaster.UserId}'";
            Object rows = DBConnection.ExecuteScalar(query);
            int row = Convert.ToInt32(rows);
            return row;
        }

        
    }
}

