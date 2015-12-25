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
                var name = (string)Request.Query.name ?? "Unknown";

                var session = sessions.GetOrAdd(name);

                return session.Register(name);
            };
        }
    }
}