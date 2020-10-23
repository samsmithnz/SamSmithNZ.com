using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SamSmithNZ.Service.DataAccess.Base
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class BaseDataAccess<T>
    {
        public string ConnectionString;

        public void SetupConnectionString(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionStrings:DefaultConnectionString"];
        }

        public async Task<List<T>> GetList(string query, DynamicParameters parameters = null, int timeOut = 15) //15 seconds is the default timeout
        {
            if (ConnectionString == null)
            {
                throw new Exception("ConnectionString not set");
            }

            IEnumerable<T> results;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                await connection.OpenAsync();
                results = await connection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure, commandTimeout: timeOut);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            return results.ToList<T>();
        }

        public async Task<T> GetItem(string query, DynamicParameters parameters = null)
        {
            if (ConnectionString == null)
            {
                throw new Exception("ConnectionString not set");
            }

            T result;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                await connection.OpenAsync();
                IEnumerable<T> results = await connection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
                result = results.FirstOrDefault<T>();
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            return result;
        }

        public async Task<R> GetScalarItem<R>(string query, DynamicParameters parameters = null)
        {
            if (ConnectionString == null)
            {
                throw new Exception("ConnectionString not set");
            }

            R result;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                await connection.OpenAsync();
                IEnumerable<R> results = await connection.QueryAsync<R>(query, parameters, commandType: CommandType.StoredProcedure);
                result = results.FirstOrDefault<R>();
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            return result;
        }

        public async Task<bool> SaveItem(string query, DynamicParameters parameters = null, int timeOut = 15) //15 seconds is the default timeout
        {
            if (ConnectionString == null)
            {
                throw new Exception("ConnectionString not set");
            }

            bool result;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                await connection.OpenAsync();
                await connection.ExecuteScalarAsync<bool>(query, parameters, commandType: CommandType.StoredProcedure, commandTimeout: timeOut);
                result = true;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
            return result;
        }
    }
}

