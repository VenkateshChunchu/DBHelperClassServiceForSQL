using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace Checking
{
    public static class DBHelperClass
    {
        private static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["newsArticleCon"].ConnectionString;
            }
        }

        #region Public Methods
        public static DataTable GetResultSetWithProcedure(string PROC_NAME, params object[] parameters)
        {
            var parameterList = new List<SqlParameter>();
            string query = "EXEC " + PROC_NAME;
            bool flag = true;
            try
            {
                if (parameters.Length % 2 != 0)
                    throw new ArgumentException("Wrong number of parameters sent to procedure. Expected an even number.");
                for (int i = 0; i < parameters.Length; i += 2)
                {
                    var newParameter = new SqlParameter(parameters[i] as string, parameters[i + 1]);
                    parameterList.Add(newParameter);
                    query += (flag ? " " : ", ") + ((string)parameters[i]);
                    flag = false;
                }
                return GetResultSetAsDataTableFromDB(query, parameterList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetResultedTableWithQuery(string query, params object[] parameters)
        {
            return GetResultSetAsDataTableFromDB(query, GetTheSqlParameters(parameters));
        }
        public static int ExecuteNonQuery(string query, params object[] parameters)
        {
            return NonQuery(query, GetTheSqlParameters(parameters));
        }
        public static object ExecuteScalar(string query, params object[] parameters)
        {
            return Scalar(query, GetTheSqlParameters(parameters));
        }
        public static DataSet ExecuteDataSet(string sqlQuery, params object[] parameters)
        {
            return GetResultSetAsDataSetFromDB(sqlQuery, GetTheSqlParameters(parameters));
        }
        public static SqlDataReader ExecuteDataReader(string sqlQuery, params object[] parameters)
        {
            return ExecuteDataReader(sqlQuery, GetTheSqlParameters(parameters));
        }
        #endregion

        #region Private Methods
        private static List<SqlParameter> GetTheSqlParameters(object[] parameters)
        {
            var parameterList = new List<SqlParameter>();
            try
            {
                if (parameters.Length % 2 != 0)
                    throw new ArgumentException("Wrong number of parameters sent to procedure. Expected an even number.");
                for (int i = 0; i < parameters.Length; i += 2)
                {
                    var newParameter = new SqlParameter(parameters[i] as string, parameters[i + 1]);
                    parameterList.Add(newParameter);
                }
                return parameterList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static DataSet GetResultSetAsDataSetFromDB(String sqlQuery, List<SqlParameter> parameters)
        {
            var ds = new DataSet();
            var cmd = GetTheCommandToExecute(sqlQuery, parameters);
            var sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }
        private static DataTable GetResultSetAsDataTableFromDB(String sqlQuery, List<SqlParameter> parameters)
        {
            var dt = new DataTable();
            var cmd = GetTheCommandToExecute(sqlQuery, parameters);
            try
            {
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static int NonQuery(string sqlQuery, List<SqlParameter> parameters)
        {
            var cmd = GetTheCommandToExecute(sqlQuery, parameters);
            return cmd.ExecuteNonQuery();
        }
        private static object Scalar(string sqlQuery, List<SqlParameter> parameters)
        {
            var cmd = GetTheCommandToExecute(sqlQuery, parameters);
            return cmd.ExecuteScalar();
        }
        private static SqlDataReader ExecDataReader(string sqlQuery,  List<SqlParameter> parameters)
        {
            var cmd = GetTheCommandToExecute(sqlQuery, parameters);
            return cmd.ExecuteReader();
        }
        private static SqlCommand GetTheCommandToExecute(string sqlQuery, List<SqlParameter> parameters)
        {
            try
            {
                var cmd = new SqlCommand(sqlQuery, new SqlConnection(ConnectionString));
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());
                return cmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
#region Refer this
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;

//namespace Checking
//{
//    public class DBHelperClass
//    {
//        private static string ConnectionString
//        {
//            get
//            {
//                return System.Configuration.ConfigurationManager.ConnectionStrings["newsArticleCon"].ConnectionString;
//            }
//        }
//        public DataTable GetResultSetFromDB(string sqlQuery)
//        {
//            var dt = new DataTable();
//            using(var da= new SqlDataAdapter(sqlQuery,ConnectionString))
//            {
//                da.Fill(dt);
//            }
//            return dt;

//        }
//        public void ExecuteSqlQuery(string sqlQuery)
//        {
//            using(var cmd=new SqlCommand(sqlQuery,new SqlConnection(ConnectionString)))
//            {
//                cmd.Connection.Open();
//                cmd.ExecuteNonQuery();
//                cmd.Connection.Close();
//            }
//        }
//    }
//}
#endregion