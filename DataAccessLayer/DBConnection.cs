﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace DataAccessLayer
{
    public static class DBConnection
    {
        private static string ConnString = GetConnectionString("ConnectionString", string.Empty);
        public static string GetConnectionString(string key, string defaultValue)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(defaultValue))
            {
                value = defaultValue;
            }
            try
            {
               value = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                //value = "Data Source=Training;User ID=SCOTT;Password=TIGERlion;Unicode=True"; 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("Error reading webconfig : " + ex.ToString());
            }
            return value;
        }
        public static OracleConnection OpenConnection()
        {
            try
            {
                OracleConnection con = new OracleConnection();
                con.ConnectionString = ConnString;
                //con.ConnectionString = "Data Source=DB19C;User ID=IIRIS_GI_DEVP;Password=iiris_gi_devp;Unicode=True";
                con.Open();
                return con;
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {

            }
        }

        public static void CloseConnection(OracleConnection connection)
        {
            try
            {
                if (connection != null)
                    connection.Close();
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
            }
        }

        public static int ExecuteQuery(string sql)
        {
            OracleConnection connection = null;
            try
            {
                OracleCommand cmd = new OracleCommand();
                connection = OpenConnection();
                cmd.CommandText = sql;
                cmd.Connection = connection;
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {

                throw err;
            }
            finally
            {
                CloseConnection(connection);
            }
        }

        public static DataTable ExecuteDataset(string query)
        {
            OracleCommand cmd;
            DataSet Dset = new DataSet();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd = new OracleCommand(query, connection);
                OracleDataAdapter Da = new OracleDataAdapter(cmd);
                Da.Fill(Dset);
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            if (Dset != null && Dset.Tables[0] != null)
            {
                return Dset.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public static object ExecuteScalar(string query)
        {
            DataSet Dset = new DataSet();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                OracleCommand objcmd = new OracleCommand(query, connection);
                object outcount = objcmd.ExecuteScalar();
                return outcount;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            if (Dset != null && Dset.Tables[0] != null)
            {
                return Dset.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public static int ExecuteQuery(Dictionary<string, object> paramValues, string query)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            int rval;
            try
            {

                connection = OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
                        }
                    }
                }
                cmd.CommandText = query;
                cmd.Connection = connection;
                rval = cmd.ExecuteNonQuery();
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return rval;

        }


        public static DataSet ExecuteQuerySelect(Dictionary<string, object> paramValues, string query)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            DataSet Dset = new DataSet();
            try
            {

                connection = OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
                        }
                    }
                }
                cmd.CommandText = query;
                cmd.Connection = connection;
                OracleDataAdapter Da = new OracleDataAdapter(cmd);
                Da.Fill(Dset);
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return Dset;

        }

        public static int ApprovingBid(string pVbUid)
        {
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection(); 
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection; cmd.CommandText = "DPRC_VEH_AUCTION_BIDDING"; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_VAB_UID", OracleType.VarChar).Value = pVbUid;
                cmd.Parameters.Add("P_STATUS", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int op = Convert.ToInt32(cmd.Parameters["P_STATUS"].Value.ToString());
                return op;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static (int,string) ExecuteProc(long clmUid, string UserId)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd.CommandText = "DPRC_CLAIM_SURVEY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CLM_UID", OracleType.Number).Value = clmUid;
                cmd.Parameters.Add("P_USER_ID", OracleType.VarChar).Value = UserId;
                cmd.Parameters.Add("P_STATUS", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_ERR_MSG", OracleType.VarChar, 112).Direction = ParameterDirection.Output;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                return ((Convert.ToInt32(cmd.Parameters["P_STATUS"].Value)),(Convert.ToString(cmd.Parameters["P_ERR_MSG"].Value)));
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) CloseConnection(connection);
            }
        }

        public static (int,long, string) ExecuteProc(Dictionary<string,object> dict)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd.CommandText = "DPRC_SAVE_SURVEY_DETAILS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_SUR_UID", OracleType.VarChar).Value = dict["SURD_SUR_UID"];
                cmd.Parameters.Add("P_ITEM_CODE", OracleType.VarChar).Value = dict["ITEM_CODE"];
                cmd.Parameters.Add("P_TYPE", OracleType.VarChar).Value = dict["TYPE"];
                cmd.Parameters.Add("P_UNIT_PRICE", OracleType.Number).Value = dict["UNIT_PRICE"];
                cmd.Parameters.Add("P_QUANTITY", OracleType.Number).Value = dict["QUANTITY"];
                cmd.Parameters.Add("P_USER_ID", OracleType.VarChar).Value = dict["SUR_CR_BY"];
                cmd.Parameters.Add("P_FC_AMT", OracleType.Number).Value = dict["FC_AMT"];
                cmd.Parameters.Add("P_LC_AMT", OracleType.Number).Value = dict["LC_AMT"];

                //cmd.Parameters.Add("P_LC_AMT", OracleType.Number).Value = dict["CR_BY"];--created by
                //cmd.Parameters.Add("P_LC_AMT", OracleType.Number).Value = dict["CR_BY"];--created date

                cmd.Parameters.Add("P_SURD_UID", OracleType.Number).Direction = ParameterDirection.InputOutput;
                cmd.Parameters["P_SURD_UID"].Value = dict["SURD_UID"];

                cmd.Parameters.Add("P_STATUS", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_ERR_MSG", OracleType.VarChar, 112).Direction = ParameterDirection.Output;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                return ((Convert.ToInt32(cmd.Parameters["P_STATUS"].Value)), (Convert.ToInt64(cmd.Parameters["P_SURD_UID"].Value)),(Convert.ToString(cmd.Parameters["P_ERR_MSG"].Value)));
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) CloseConnection(connection);
            }
        }

        public static (int, string) ExecuteProcForApproval(long surUid,long clmUid, string UserId)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd.CommandText = "DPRC_APPROVE_SURVEY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_SUR_UID", OracleType.Number).Value = surUid;
                cmd.Parameters.Add("P_CLM_UID", OracleType.Number).Value = clmUid;
                cmd.Parameters.Add("P_USER_ID", OracleType.VarChar).Value = UserId;
                
                cmd.Parameters.Add("P_STATUS", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("P_ERR_MSG", OracleType.VarChar, 112).Direction = ParameterDirection.Output;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                return ((Convert.ToInt32(cmd.Parameters["P_STATUS"].Value)), (Convert.ToString(cmd.Parameters["P_ERR_MSG"].Value)));
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) CloseConnection(connection);
            }
        }


    }
}

