namespace NancyApp.Modules
{
    using Nancy.AspNet.WebSockets;

    using Db;
    using Sessions;
    using System.Configuration;

    public class ModuleWebSocket : WebSocketNancyModule
    {
        public ModuleWebSocket(ISessions sessions)
            : base()
        {
            var repository = new ConnectionRepository(new DbConnectionFactory(ConfigurationManager.ConnectionStrings["Test"].ConnectionString));
            
            WebSocket["/ws"] = _ =>
            {
                var name = (string)Request.Query.name;
                if (name != null && name == "test")
                {
                    var code = (string)Request.Query.code;
                    
                    repository.SetConnection(name, code);

                    var session = sessions.GetOrAdd(name);

                    return session.Register(name);
                }
                return null;
            };
        }
    }
}