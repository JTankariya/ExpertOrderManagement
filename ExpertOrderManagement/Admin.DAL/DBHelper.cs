using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DAL
{
    public class DBHelper
    {
        public static string cs = Convert.ToString(ConfigurationManager.AppSettings["DBPath"]);

        public static int ExecuteNonQuery(string Query, Dictionary<string, object> parameters = null, bool IsStoredProcedure = false)
        {
            int Result = 0;
            using (SqlConnection conn = new SqlConnection(cs))
            {

                SqlCommand cmd = new SqlCommand(Query, conn);

                try
                {
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in parameters)
                            cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }
                    if (IsStoredProcedure)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    Result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            return Result;
        }

        public static int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            //if we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                ////pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                //SqlParameter[] commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName, false);

                ////assign the provided values to these parameters based on parameter order
                //AssignParameterValues(commandParameters, parameterValues);

                //call the overload that takes an array of SqlParameters
                return ExecuteNonQuery(spName, (SqlParameter[])parameterValues);
            }
            //otherwise we can just call the SP without params
            else
            {
                return ExecuteNonQuery(spName);
            }
        }

        public static DataTable GetDataTable(string Query, Dictionary<string, object> parameters = null, bool IsStoredProcedure = false, int TableIndex = 0)
        {
            //string cs = @"server=204.11.58.166;user id=shahsrag_dhruvin;password=dms@1001_;database=shahsrag_expertmobile";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DataSet ds = new DataSet();
                try
                {
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in parameters)
                            cmd.Parameters.Add(new SqlParameter(kvp.Key, (kvp.Value == null ? DBNull.Value : kvp.Value)));
                    }
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    if (IsStoredProcedure)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    adp.Fill(ds);
                    if (TableIndex == 0)
                    {
                        return ds.Tables[0];
                    }
                    else
                    {
                        return ds.Tables[TableIndex];
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            try
            {
                var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName.ToLower())
                    .ToList();

                var properties = typeof(T).GetProperties();

                return dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();

                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name.ToLower()))
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : row[pro.Name], null);
                    }

                    return objT;
                }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static IEnumerable<T> ConvertToEnumerable<T>(DataTable dt)
        {
            try
            {
                var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName.ToLower())
                    .ToList();

                var properties = typeof(T).GetProperties();

                return dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();

                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name.ToLower()))
                        {
                            if (row[pro.Name].ToString().ToUpper() == "TRUE")
                            {
                                pro.SetValue(objT, true, null);
                            }
                            else if (row[pro.Name].ToString().ToUpper() == "FALSE")
                            {
                                pro.SetValue(objT, false, null);
                            }
                            else
                            {
                                pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : row[pro.Name], null);
                            }
                        }

                    }

                    return objT;
                }).AsEnumerable();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static string CheckEscape(string variable)
        {
            return (!string.IsNullOrEmpty(variable) ? variable.Replace("'", "''") : variable);
        }
    }
}
