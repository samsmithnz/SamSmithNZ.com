using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;

namespace SSNZ.FooFighters.Data
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GenericDataAccess<T>
    {
        private string _connectionStringName;

        public GenericDataAccess()
        {
            this._connectionStringName = "DefaultConnection";
        }

        /// <summary>
        /// Retrieves just one item from the database
        /// </summary>
        /// <typeparam name="T">Generic type of object to return</typeparam>
        /// <param name="storedProcedureName">Stored Procedure name string</param>
        /// <param name="parameters">Dapper DynamicParameters object</param>
        /// <returns>Returns object as defined by T</returns>
        public async Task<T> GetItemAsync(string storedProcedureName, DynamicParameters parameters = null)
        {
            IEnumerable<T> items = await this.GetListAsync(storedProcedureName, parameters);
            return items.SingleOrDefault();
        }

        /// <summary>
        /// Retrieves a list of items from the database
        /// </summary>
        /// <typeparam name="T">Generic type of object to return</typeparam>
        /// <param name="storedProcedureName">Stored Procedure name string</param>
        /// <param name="parameters">Dapper DynamicParameters object</param>
        /// <returns>Returns object as defined by T</returns>
        public async Task<List<T>> GetListAsync(string storedProcedureName, DynamicParameters parameters = null)
        {
            IEnumerable<T> items;
            try
            {
                await this.CreateConnectionAsync();
                items = await this.MySQLConnection.QueryAsync<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                this.CloseConnection();
#if DEBUG
                System.Diagnostics.Debug.WriteLine(CreateSQLString(storedProcedureName, parameters));
#endif
            }
            return items.ToList<T>();
        }

        /// <summary>
        /// Posts data to the database. If you need to return anything aside from a successful boolean call, use the other base functions
        /// </summary>
        /// <param name="storedProcedureName">Stored Procedure name string</param>
        /// <param name="parameters">Dapper DynamicParameters object</param>
        /// <returns>Boolean (true) if successful. </returns>
        public async Task<bool> PostItemAsync(string storedProcedureName, DynamicParameters parameters = null, int? timeoutOverride = null)
        {
            try
            {
                await this.CreateConnectionAsync();
                await this.MySQLConnection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: timeoutOverride);
            }
            finally
            {
                this.CloseConnection();
#if DEBUG
                System.Diagnostics.Debug.WriteLine(CreateSQLString(storedProcedureName, parameters));
#endif
            }
            return true;
        }

        /// <summary>
        /// Retrieves just one scalar item from the database
        /// </summary>
        /// <typeparam name="T">Generic type of object to return</typeparam>
        /// <param name="storedProcedureName">Stored Procedure name string</param>
        /// <param name="parameters">Dapper DynamicParameters object</param>
        /// <returns>Returns object as defined by T</returns>
        public async Task<N> GetScalarAsync<N>(string storedProcedureName, DynamicParameters parameters = null)
        {
            IEnumerable<N> items;
            try
            {
                await this.CreateConnectionAsync();
                items = await this.MySQLConnection.QueryAsync<N>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                this.CloseConnection();
#if DEBUG
                System.Diagnostics.Debug.WriteLine(CreateSQLString(storedProcedureName, parameters));
#endif
            }
            if (items.ToList().Count > 0)
            {
                return ConvertScalarValue<N>(items.ToList()[0].ToString());
            }
            else
            {
                return default(N);
            }
        }

        #region " Private functions and properties"

        private SqlConnection MySQLConnection { get; set; }

        /// <summary>
        /// To create a new connection to the database. Uses the connection string in the calling applications (web/app).config file
        /// </summary>
        /// <returns>A (hopefully) open SQLConnection object</returns>
        private async Task<SqlConnection> CreateConnectionAsync()
        {
            if (ConfigurationManager.ConnectionStrings.Count == 0)
            {
                throw new Exception("No connection string found");
            }
            else if (ConfigurationManager.ConnectionStrings[this._connectionStringName] == null)
            {
                throw new Exception(this._connectionStringName + " connection string not found");
            }
            string connectionString = ConfigurationManager.ConnectionStrings[this._connectionStringName].ToString();
            //string[] connectionStringItems = connectionString.Split(';');
            //string encryptedUid = Array.Find(connectionStringItems, p => p.StartsWith("uid", StringComparison.Ordinal)).Substring(4);
            //string decryptedUid = Encryption.GetDecryptedString(encryptedUid);
            //string encryptedPassword = Array.Find(connectionStringItems, p => p.StartsWith("password", StringComparison.Ordinal)).Substring(9);
            //string decryptedPassword = Encryption.GetDecryptedString(encryptedPassword);
            //connectionString = connectionString.Replace(encryptedUid, decryptedUid).Replace(encryptedPassword, decryptedPassword);
            MySQLConnection = new SqlConnection(connectionString);
            await MySQLConnection.OpenAsync();
            return MySQLConnection;
        }

        /// <summary>
        /// Close the SQL connection to the database
        /// </summary>
        /// <returns>True if successful</returns>
        private bool CloseConnection()
        {
            if (MySQLConnection != null && MySQLConnection.State == ConnectionState.Open)
            {
                MySQLConnection.Close(); //There is no async for close
            }
            return true;
        }

        //A private helper function for the Scalar function to handle Guids
        //Note that N is used instead of T, as T is defined at the class level, and this generic type needs to be defined at the method level
        private static N ConvertScalarValue<N>(string value)
        {
            if (typeof(N).ToString() == "System.Guid")
            {
                //convert a guid to string
                return (N)TypeDescriptor.GetConverter(typeof(N)).ConvertFromInvariantString(value);
            }
            else
            {
                return (N)Convert.ChangeType(value, typeof(N));
            }
        }

        /// <summary>
        /// A function to help debug SQL
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string CreateSQLString(string storedProcedureName, DynamicParameters parameters = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("exec ");
            sb.Append(storedProcedureName);
            sb.Append(" ");
            if (parameters != null)
            {
                int i = 0;
                int count = parameters.ParameterNames.ToList<string>().Count;
                foreach (var name in parameters.ParameterNames)
                {
                    i++;
                    var value = parameters.Get<dynamic>(name);
                    //sb.AppendFormat("{0}={1}\n", name, pValue.ToString());
                    sb.Append("@");
                    sb.Append(name);
                    sb.Append("=");
                    if (value == null)
                    {
                        sb.Append("null");
                    }
                    else
                    {
                        if (value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.Guid")
                        {
                            sb.Append("'");
                        }
                        if (value.GetType().ToString() == "System.Boolean")
                        {
                            if (value == true)
                            {
                                sb.Append("1");
                            }
                            else
                            {
                                sb.Append("0");
                            }
                        }
                        else
                        {
                            sb.Append(value.ToString());
                        }
                        if (value.GetType().ToString() == "System.String" || value.GetType().ToString() == "System.Guid")
                        {
                            sb.Append("'");
                        }
                    }
                    if (count > i)
                    {
                        sb.Append(", ");
                    }
                }
            }
            return sb.ToString();
        }

        #endregion

    }
}
