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

        public void SetConnection(String name, String code)
        {
            using (var connection = factory.CreateSqlConnection())
            {
                connection.Query("INSERT Connections(Name, Code) VALUES (@Name, @Code)", new Connection { Name = name, Code= code });
            }
        }
    }

    internal class Connection
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
    }
}