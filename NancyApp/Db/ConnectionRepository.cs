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

        public void SetConnectionInfo(string name, string code, string address, bool status)
        {
            using (var connection = factory.CreateSqlConnection())
            {
                connection.Query(@"INSERT Connections(Name, Code, Address, Status)
                                   VALUES (@Name, @Code, @Address, @Status)",
                                 new ConnectionInfo
                                 {
                                     Name = name,
                                     Code = code,
                                     Address = address,
									 Status = status
								 });
            }
        }
    }

    internal class ConnectionInfo
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
	}
}