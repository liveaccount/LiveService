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

                if (Convert.ToInt32(name) == 22 && Convert.ToInt32(code) == 22)
                {
                    repository.SetConnection(name, code);

                    var session = sessions.GetOrAdd(name);

                    return session.Register(name, code);
                }

                return null;
            };
        }
    }
}