namespace NancyApp.Modules
{
    using System;
    using Sessions;
    using System.Configuration;

    using Nancy.AspNet.WebSockets;

    using Db;

    public class ModuleWebSocket : WebSocketNancyModule
    {
        public ModuleWebSocket(ISessions sessions)
            : base()
        {
            var repository = new ConnectionRepository(new DbConnectionFactory(ConfigurationManager.ConnectionStrings["Test"].ConnectionString));
            
            WebSocket["/ws"] = _ =>
            {
                var name = (String)Request.Query.name;
                var code = (String)Request.Query.code;
                var address = Request.UserHostAddress;

                if (Convert.ToInt32(name) == 22 && Convert.ToInt32(code) == 222)
                {
                    repository.SetConnection(name, code, address, true);

                    var session = sessions.GetOrAdd(name);

                    return session.Register(name, code);
                }
				else
					repository.SetConnection(name, code, address, false);

				return null;
            };
        }
    }
}