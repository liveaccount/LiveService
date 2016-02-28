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

        public void SetConnection(String name, String code, String address, Boolean status)
        {
            using (var connection = factory.CreateSqlConnection())
            {
                connection.Query(@"INSERT Connections(Name, Code, Address, Status)
                                   VALUES (@Name, @Code, @Address, @Status)",
                                 new Connection
                                 {
                                     Name = name,
                                     Code = code,
                                     Address = address,
									 Status = status
								 });
            }
        }
    }

    internal class Connection
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public String Address { get; set; }
		public Boolean Status { get; set; }
	}
}