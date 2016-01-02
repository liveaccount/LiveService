namespace NancyApp.Modules
{
    using Nancy.AspNet.WebSockets;

    using Sessions;

    public class ModuleWebSocket : WebSocketNancyModule
    {
        public ModuleWebSocket(ISessions sessions)
            : base()
        {
            WebSocket["/websocket"] = _ =>
            {
                var name = (string)Request.Query.name;
                if (name != null && name == "TEST")
                {
                    var session = sessions.GetOrAdd(name);

                    return session.Register(name);
                }
                return null;
            };
        }
    }
}