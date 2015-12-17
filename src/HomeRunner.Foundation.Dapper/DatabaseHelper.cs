
using System.Configuration;
using System.Data.Common;

namespace HomeRunner.Foundation.Dapper
{
    public class DatabaseHelper
    {
        /// <summary>
        /// Create a <see cref="DbConnection"/> for a provider name and connection string taken from the application configuration. 
        /// </summary>
        /// <returns>Returns a DbConnection on success.</returns>
        public static DbConnection CreateDbConnection()
        {
            return DatabaseHelper.CreateDbConnection(ConfigurationManager.AppSettings["dapper.provider"], ConfigurationManager.AppSettings["dapper.connectionstring"]);
        }

        // Given a provider name and connection string, create the DbProviderFactory and DbConnection. 
        // Returns a DbConnection on success. 
        /// <summary>
        /// Create a <see cref="DbConnection"/> for the given provider name and connection string. 
        /// </summary>
        /// <returns>Returns a DbConnection on success.</returns>
        public static DbConnection CreateDbConnection(string providerName, string connectionString)
        {
            // Assume failure.
            DbConnection connection = null;

            // Create the DbProviderFactory and DbConnection. 
            if (connectionString != null)
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                connection = factory.CreateConnection();
                if (connection != null) connection.ConnectionString = connectionString;
            }

            return connection;
        }
    }
}