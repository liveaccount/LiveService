namespace NancyApp.Db
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionstring;

        public DbConnectionFactory(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public IDbConnection CreateSqlConnection()
        {
            var connection = new SqlConnection(connectionstring);
            connection.Open();
            return connection;
        }
    }
}
