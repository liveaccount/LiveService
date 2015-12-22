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
            return new WebSocketHandler();
        }
    }

    public class WebSocketHandler : IWebSocketHandler
    {
        private IWebSocketClient client;

        private void SendToAll(byte[] data)
        {
            client.Send(data);
        }

        private void SendToAll(string message)
        {
            client.Send(message);
        }
        
        public void OnOpen(IWebSocketClient client)
        {
            this.client = client;

            SendToAll(string.Format("User connected."));
        }

        public void OnData(byte[] data)
        {
            SendToAll(data);
        }

        public void OnMessage(string message)
        {
            SendToAll(string.Format("User says: {0}", message));
        }

        public void OnClose()
        {
            SendToAll(string.Format("User disconnected."));
        }

        public void OnError()
        {
            SendToAll("Error");
        }
    }
}