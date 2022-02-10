﻿using System.Data;
using System.Data.SqlClient;

namespace AutoGenerateDDDProjectForNetCore.services
{
    public class SQLServiceBase
    {
        public string? Message { get; private set; }
        private string ConnectionString;

        protected SqlConnection Connection;

        public SQLServiceBase(string connetionString)
        {
            this.ConnectionString = connetionString;
            Connection = new SqlConnection(this.ConnectionString);
        }
        
        public static string GetConnetionString(string serverName, string databaseName)
        {
            return $"Data Source={serverName};Initial Catalog={databaseName};Integrated Security=SSPI;MultipleActiveResultSets=True;Persist Security Info=False;";
        }
        public static string GetConnetionString(string serverName, string databaseName, string username, string password)
        {
            return $"Data Source={serverName};Initial Catalog={databaseName};Integrated Security=SSPI;User Id={username};Password={password};MultipleActiveResultSets=True;Persist Security Info=False;";
        }       

        protected void OpenConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
        }

        protected DataTable ExecuteReaderData(SqlCommand command)
        {
            try
            {
                Connection.Open();

                command.Connection = Connection;
                SqlDataReader reader = command.ExecuteReader();

                DataTable data = new DataTable();
                data.Load(reader);

                return data;
            }
            catch (Exception ex) { Message = ex.Message; return null; }
            finally { Connection.Close(); }
        }

        protected void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
                Connection.Close();
        }

        public bool CheckConnention()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Connection.ConnectionString);
            string connstr = new SqlConnectionStringBuilder
            {
                DataSource = builder.DataSource,
                IntegratedSecurity = false,
                ApplicationIntent = ApplicationIntent.ReadOnly,
                MultipleActiveResultSets = true,
                InitialCatalog = builder.InitialCatalog,
                ConnectTimeout = 1,
                UserID = builder.UserID,
                Password = builder.Password,
            }.ConnectionString;

            using (var l_oConnection = new SqlConnection(connstr))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (SqlException ex)
                {
                    Message = ex.Message;
                    return false;
                }
            }
        }
    }
}