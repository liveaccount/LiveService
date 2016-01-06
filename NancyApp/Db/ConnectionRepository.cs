namespace NancyApp.Db
{
    using System;
    using System.Collections.Generic;

    using Dapper;

    public class ConnectionRepository
    {
        private readonly IDbConnectionFactory factory;

        public ConnectionRepository(IDbConnectionFactory factory)
        {
            this.factory = factory;
        }

        public void SetConnection(String name)
        {
            using (var connection = factory.CreateSqlConnection())
            {
                connection.Query("insert Connections(Name) values (@Name)", new Connection { Name = name });
            }
        }
    }

    internal class Connection
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}