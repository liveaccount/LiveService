namespace NancyApp
{
    using Nancy.AspNet.WebSockets;

    public class ModuleWebSocket : WebSocketNancyModule
    {
        public ModuleWebSocket()
        {
            WebSocket["/websocket"] = _ => CreateWebSocketHandler();
        }

        private IWebSocketHandler CreateWebSocketHandler()
        {
            return new Handler();
        }
    }

    public class Handler : IWebSocketHandler
    {
        private IWebSocketClient _client;

        private void SendToAll(string message)
        {
            _client.Send(message);
        }

        private void SendToAll(byte[] message)
        {
            _client.Send(message);
        }

        public void OnOpen(IWebSocketClient client)
        {
            _client = client;
            SendToAll(string.Format("User connected to drawing board"));
        }

        public void OnMessage(string message)
        {
            SendToAll(string.Format("User says {0}", message));
        }

        public void OnData(byte[] message)
        {
            SendToAll(message);
        }

        public void OnClose()
        {
            SendToAll(string.Format("User disconnected from drawing board"));
        }

        public void OnError()
        {
            SendToAll("Error");
        }
    }
}