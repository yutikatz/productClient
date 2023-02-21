using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal
{
    public class Dal
    {
        private SqlConnection dbCon;
        private static Dal _dal;
        private string sqlConnectionString;

        #region Properties

        public static Dal Instance
        {
            get
            {
                if (_dal == null)
                {
                    _dal = new Dal();
                }
                return _dal;
            }
        }
        #region event

        private delegate object FillObjectDelegate(SqlDataReader reader);

        #endregion event

        #endregion Properties

        #region Private method
        private static object RunReadStoredProcedure(string sqlConnectionString, string storedProcedureName, FillObjectDelegate fillObjectDelegate)
        {
            return RunReadStoredProcedure(sqlConnectionString, storedProcedureName, fillObjectDelegate, new List<SqlParameter>());
        }

        private static object RunReadStoredProcedure(string sqlConnectionString, string storedProcedureName, FillObjectDelegate fillObjectDelegate, List<SqlParameter> parameters)
        {
            int resultId;
            string resultDesc;
            return RunReadStoredProcedure(sqlConnectionString, storedProcedureName, fillObjectDelegate, parameters, out resultId, out resultDesc);
        }
        private static object RunReadStoredProcedure(string sqlConnectionString, string storedProcedureName, FillObjectDelegate fillObjectDelegate, List<SqlParameter> parameters, out int resultId, out string resultDesc)
        {
            //מפעילה פרוצדורה שמקבלת פרמטרים ומחזירה את אוביקט השליפה
            resultId = -1;
            resultDesc = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = storedProcedureName;
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        object ret = fillObjectDelegate(reader);
                        reader.Close();

                        object result;
                        if (command.Parameters.Contains("@ResultCode"))
                        {
                            result = command.Parameters["@ResultCode"].Value;
                            if (result != null)
                            {
                                if (result is int)
                                {
                                    resultId = (int)result;
                                }
                            }
                            result = command.Parameters["@ResultDesc"].Value;
                            if (result != null)
                            {
                                resultDesc = result.ToString();
                            }
                        }

                        return ret;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private static string ExecNonQueryStoredProcedure(string storedProcedureName, string sqlConnectionString, List<SqlParameter> parameters)
        {
            //Output מפעילה פרוצדורה שמקבלת פרמטרים ומחזירה פרמטר  
            DBHelper dataBaseHelper = null;
            bool executeResult = true;
            string resultCode = String.Empty;
            string resultDesc = String.Empty;
            int rowsAffected = 0;
            try
            {
                dataBaseHelper = new DBHelper(sqlConnectionString);
                dataBaseHelper.ClearParameters();

                foreach (var parameter in parameters)
                {
                    dataBaseHelper.AddParam(parameter);
                }

                executeResult = dataBaseHelper.ExecuteNonQuery_Sp(storedProcedureName, true, out rowsAffected);



                if (!executeResult)
                {
                    Console.WriteLine(storedProcedureName, dataBaseHelper.LastError);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(storedProcedureName, ex);
            }
            finally
            {
                if (dataBaseHelper != null)
                {
                    dataBaseHelper.CloseConnection();
                }
            }
            // return executeResult;
            return resultCode;
        }






        #endregion Private method

        private Dal()
        {

            sqlConnectionString = "Data Source=.;Initial Catalog=Products;Integrated Security=True";
        }
        public List<Product> convertToProducts(SqlDataReader reader)
        {
            List<Product> result = new List<Product>();

            while (reader.Read())
            {
                int code = (int)GetDataInt(reader, "code");
                string name = GetDataVarchar(reader, "name");
                string description = GetDataVarchar(reader, "description");
                string router = GetDataVarchar(reader, "router");
                string startDate = GetDataVarchar(reader, "startDate");
                result.Add(new Product() { Code = code, Name = name, Description = description, Startdate = startDate,Router= router });
            }

            return result;
        }
        public List<Product> GetProducts(int sortNumber)
        {
            object result = new List<Product>();
            try
            {
                string spGetProducts = "GetAllProducts";
                if (sortNumber == 1)
                {
                    spGetProducts = "SortByName";
                }
                if (sortNumber == 2)
                {
                    spGetProducts = "SortByDescription";
                }
                result = RunReadStoredProcedure(sqlConnectionString, spGetProducts, convertToProducts);


            }
            catch (Exception ex)
            {
            }
            return result as List<Product>;
        }

        public Boolean InsertProduct(string name, string description)
        {

            try
            {
                string spAddProduct = "AddProduct";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(GetSqlParameter("@name", name,

                    SqlDbType.NVarChar));
                parameters.Add(GetSqlParameter("@description", description, SqlDbType.NVarChar));

                ExecNonQueryStoredProcedure(spAddProduct, sqlConnectionString, parameters);


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public Boolean DeleteProduct(int code)
        {
            try
            {
                string spDeleteProduct = "DeleteProduct";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(GetSqlParameter("@code", code, SqlDbType.Int));

                ExecNonQueryStoredProcedure(spDeleteProduct, sqlConnectionString, parameters);


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public Boolean EditProduct(int code, string name, string description)
        {

            try
            {
                string spEditProduct = "EditProduct";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(GetSqlParameter("@code", code, SqlDbType.Int));
                parameters.Add(GetSqlParameter("@name", name, SqlDbType.NVarChar));
                parameters.Add(GetSqlParameter("@description", description, SqlDbType.NVarChar));

                ExecNonQueryStoredProcedure(spEditProduct, sqlConnectionString, parameters);


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public Boolean InsertIMG(int code, string router)
        {

            try
            {
                string spAddIMG = "AddIMG";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(GetSqlParameter("@code", code, SqlDbType.Int));
                parameters.Add(GetSqlParameter("@router", router, SqlDbType.NVarChar));

                ExecNonQueryStoredProcedure(spAddIMG, sqlConnectionString, parameters);


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public Boolean EditIMG(int code, string router)
        {

            try
            {
                string spEditeIMG = "EditIMG";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(GetSqlParameter("@code", code, SqlDbType.Int));
                parameters.Add(GetSqlParameter("@router", router, SqlDbType.NVarChar));
                ExecNonQueryStoredProcedure(spEditeIMG, sqlConnectionString, parameters);


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public Boolean DeleteIMG(int code)
        {
            try
            {
                string spDeleteIMG = "DeleteIMG";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(GetSqlParameter("@code", code, SqlDbType.Int));
                ExecNonQueryStoredProcedure(spDeleteIMG, sqlConnectionString, parameters);


            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private static string GetDataVarchar(SqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
            {
                return null;
            }
            return reader[column].ToString();
        }
        private static int? GetDataInt(SqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
            {
                return null;
            }
            return (int)reader[column];
        }
        private static DateTime? GetDataDateTime(SqlDataReader reader, string column)
        {
            if (reader[column] == DBNull.Value)
            {
                return null;
            }
            return (DateTime)reader[column];
        }
        private static SqlParameter GetSqlParameter(string parameter, Object value, SqlDbType type)
        {

            return new SqlParameter() { ParameterName = parameter, Value = value, SqlDbType = type };
        }
    }
}
