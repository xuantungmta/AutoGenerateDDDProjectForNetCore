﻿using System.Data;
using System.Data.SqlClient;

namespace AutoGenerateDDDProjectForNetCore.services
{
    public class DatabaseService : SQLServiceBase
    {
        public static string GetMasterConnetionString(string serverName)
        {
            return GetConnetionString(serverName, "master");
        }
        public static string GetMasterConnetionString(string serverName, string username, string password)
        {
            return GetConnetionString(serverName, "master", username, password);
        }

        public DatabaseService(string connetionString) : base(connetionString)
        {
        }

        public string[] GetAllDatabase()
        {
            string query = "SELECT * FROM sys.databases";
            SqlCommand command = new SqlCommand(query);

            DataTable data = ExecuteReaderData(command);
            if (data != null)
            {
                List<string> databases = new List<string>();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    databases.Add(data.Rows[i]["name"].ToString().ToLower());
                }
                return databases.ToArray();
            }
            else return Array.Empty<string>();
        }
    }
}