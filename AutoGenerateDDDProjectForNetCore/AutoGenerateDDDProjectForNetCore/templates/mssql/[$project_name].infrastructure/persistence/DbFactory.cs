using [$project_name].domain.responsitory;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace [$project_name].infrastructure.persistence
{
    public class DbFactory : IDisposable, IDbFactory
    {
        private IDbConnection? _connection;

        public void Dispose()
        {
            if (_connection != null)
                _connection?.Dispose();
        }

        public IDbConnection Init()
        {
            if (_connection == null)
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

                var root = builder.Build();
                string _connectionString = root.GetConnectionString("DefaultConnection");
                _connection = new SqlConnection(_connectionString);
                try
                {
                    if (_connection.State == System.Data.ConnectionState.Closed) _connection.Open();
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            Console.WriteLine("Cannot connect to server. Contact administrator");
                            break;

                        case 1045:
                            Console.WriteLine("Invalid username/password, please try again");
                            break;
                    }
                }
            }
            return _connection;
        }
    }
}