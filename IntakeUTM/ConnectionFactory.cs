using System.Configuration;
using System.Data.SqlClient;

namespace IntakeUTM
{
    public static class ConnectionFactory
    {
        /// <summary>
        /// Get a connection.
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ToString();
            return new SqlConnection(connectionString);
        }
    }
}