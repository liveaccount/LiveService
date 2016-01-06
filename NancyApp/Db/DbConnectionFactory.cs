namespace NancyApp.Db
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly String connectionString;

        public DbConnectionFactory(String connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateSqlConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
