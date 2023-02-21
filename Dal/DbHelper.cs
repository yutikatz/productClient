using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{

    public class DBHelper
    {
        #region Members

        protected SqlConnection m_ConnObj = null;
        protected SqlCommand m_CmdObj = null;

        #endregion Members

        #region ConnectionString

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string m_ConnectionString;
        /// <summary>
        /// Gets or set Connection string
        /// </summary>
        public string ConnectionString
        {
            get { return m_ConnectionString; }
            set { m_ConnectionString = value; }
        }

        #endregion

        #region ComandText
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string m_CmdText;
        /// <summary>
        /// Gets or set sql command for execute
        /// </summary>
        public string ComandText
        {
            get { return m_CmdText; }
            set { m_CmdText = value; }
        }

        #endregion

        #region LastError
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string m_LastError;
        /// <summary>
        /// Gets last error which take place in system
        /// this value will be reset with enter to all function
        /// </summary>
        public string LastError
        {
            get { return m_LastError; }
            set { m_LastError = value; }
        }
        #endregion

        #region CommandType
        private CommandType m_CommandType;

        protected CommandType DbCommandType
        {
            get { return m_CommandType; }
            set { m_CommandType = value; }
        }
        #endregion

        #region Constructor

        public DBHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.m_ConnObj = new SqlConnection(connectionString);
            this.m_CmdObj = this.m_ConnObj.CreateCommand();
        }

        #endregion Constructor

        #region private method

        protected void CreateCommand()
        {
            this.m_CmdObj.CommandText = this.m_CmdText;
            this.m_CmdObj.CommandType = this.DbCommandType;
            return;
        }

        /// <summary>
        /// function will be open connection and return true or false
        /// if return value is false,the error can be see in LastError properties
        /// </summary>
        /// <returns></returns>
        protected bool OpenConnection()
        {
            bool status = false;
            try
            {
                if (this.m_ConnObj.State != ConnectionState.Open)
                {
                    this.m_ConnObj.Open();
                }
                status = true;
            }
            catch (Exception ex)
            {
                this.m_LastError = ex.Message;
                status = false;
            }
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="isCloseConnection">if need close connection must be true,otherwise connection stay open </param>
        /// <returns>For UPDATE, INSERT, and DELETE statements, the 
        /// return value is the number of rows affected by the command. 
        /// For all other types of statements, the return value is -1.
        /// If error was occured the error can be see in LastError properties
        /// and return value is -100</returns>
        private bool ExecuteNonQuery(bool isCloseConnection, out int rowsAffected)
        {
            bool status = false;
            rowsAffected = 0;
            this.m_LastError = String.Empty;

            try
            {
                if (this.m_ConnObj.State != ConnectionState.Open)
                {
                    this.OpenConnection();
                }
                CreateCommand();
                rowsAffected = this.m_CmdObj.ExecuteNonQuery();
                status = true;
            }
            catch (Exception ex)
            {
                this.m_LastError = ex.Message;
                status = false;
            }
            finally
            {
                if (isCloseConnection)
                {
                    this.CloseConnection();
                }
            }
            return status;
        }

        private bool ExecuteReader(bool isCloseConnection, ref SqlDataReader reader)
        {
            bool status = false;
            reader = null;
            this.m_LastError = String.Empty;
            try
            {
                if (this.m_ConnObj.State != ConnectionState.Open)
                {
                    this.OpenConnection();
                }
                CreateCommand();
                reader = this.m_CmdObj.ExecuteReader(isCloseConnection ? CommandBehavior.CloseConnection : CommandBehavior.Default);
                status = true;
            }
            catch (Exception ex)
            {
                this.m_LastError = ex.Message;
                status = false;
            }
            finally
            {
                if (isCloseConnection)
                {
                    this.CloseConnection();
                }
            }
            return status;
        }

        #endregion private method

        #region public method

        /// <summary>
        /// Remove invalid characters for Db from string
        /// </summary>
        /// <param name="sSourceStr"></param>
        /// <returns></returns>
        public string DBFilterString(string sSourceStr)
        {
            if (sSourceStr != null && sSourceStr != "")
            {
                return sSourceStr.Replace("\"", "''").Replace("'", "''");
            }
            return "";
        }

        /// <summary>
        /// Close Connection to DB
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                this.m_ConnObj.Close();
            }
            catch
            {
                return;
            }
        }

        public bool ExecuteNonQuery_Sp(string SpName, bool isCloseConnection, out int rowsAffected)
        {

            this.m_CommandType = CommandType.StoredProcedure;
            this.ComandText = SpName;
            return ExecuteNonQuery(isCloseConnection, out rowsAffected);
        }

        public bool ExecuteNonQuery_Sql(string nameColumn, string statement, bool isCloseConnection, out int rowsAffected)
        {
            this.m_CommandType = CommandType.Text;
            this.ComandText = statement;
            return ExecuteNonQuery(isCloseConnection, out rowsAffected);
        }

        public bool ExecuteReader_Sp(string SpName, bool isCloseConnection, ref SqlDataReader reader)
        {
            this.m_CommandType = CommandType.StoredProcedure;
            this.ComandText = SpName;
            return ExecuteReader(isCloseConnection, ref reader);
        }

        public bool ExecuteReader_Sql(string statement, bool isCloseConnection, ref SqlDataReader reader)
        {
            this.m_CommandType = CommandType.Text;
            this.ComandText = statement;
            return ExecuteReader(isCloseConnection, ref reader);
        }

        public bool ExecuteScalar(bool isCloseConnection, out object retObj)
        {
            bool status = false;
            retObj = null;
            this.m_LastError = String.Empty;
            try
            {
                if (this.m_ConnObj.State != ConnectionState.Open)
                {
                    this.OpenConnection();
                }
                CreateCommand();
                retObj = this.m_CmdObj.ExecuteScalar();
                status = true;
            }
            catch (Exception ex)
            {
                this.m_LastError = ex.Message;
                status = false;
            }
            finally
            {
                if (isCloseConnection)
                {
                    this.CloseConnection();
                }
            }
            return status;
        }

        public bool ExecuteScalar_Sp(string SpName, bool isCloseConnection, out object retObj)
        {
            this.m_CommandType = CommandType.StoredProcedure;
            this.ComandText = SpName;
            return ExecuteScalar(isCloseConnection, out retObj);
        }

        public bool ExecuteScalar_Sql(string statement, bool isCloseConnection, out object retObj)
        {
            this.m_CommandType = CommandType.Text;
            this.ComandText = statement;
            return ExecuteScalar(isCloseConnection, out retObj);
        }

        public static SqlParameter CreateParameter(ParameterDirection direction, string ParamName, SqlDbType ParamType)
        {
            SqlParameter p = null;
            try
            {
                SqlParameter param = new SqlParameter();
                p = param;
            }
            catch (Exception ex)
            {
                return null;
            }
            return p;
        }

        public static SqlParameter CreateParameter(ParameterDirection direction, string ParamName, object ParamValue, SqlDbType ParamType)
        {
            SqlParameter p = null;
            if (ParamValue != null && ParamValue != DBNull.Value)
            {
                int ParamSize = ParamValue.ToString().Length;
                p = CreateParameter(direction, ParamName, ParamSize, ParamValue, ParamType);

            }
            return p;
        }

        public static SqlParameter CreateParameter(ParameterDirection direction, string ParamName, int ParamSize, object ParamValue, SqlDbType ParamType)
        {
            try
            {

                SqlParameter param = new SqlParameter();
                param.Direction = direction;
                param.ParameterName = ParamName;
                param.SqlDbType = ParamType;
                //if(ParamValue != null )
                //{
                param.Value = ParamValue;
                param.Size = ParamSize;
                //}
                //if(ParamValue == null || ParamValue == DBNull.Value
                return param;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void AddParam(SqlParameter param)
        {
            try
            {
                this.m_CmdObj.Parameters.Add(param);
            }
            catch (Exception ex)
            {

            }
        }

        public void ClearParameters()
        {
            this.m_CmdObj.Parameters.Clear();
        }

        public object GetParam(string ParamName)
        {
            return m_CmdObj.Parameters[ParamName].Value;
        }

        #endregion public method
    }
}
