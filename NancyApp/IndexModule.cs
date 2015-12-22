namespace NancyApp
{
    using Nancy;
    using Nancy.AspNet.WebSockets;
    using System;
    using System.Collections.Generic;

    public class IndexModule : WebSocketNancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ =>
            {
                return View["index"];
            };

            WebSocket["/websocket"] = _ => CreateWebSocketHandler();
        }

        private Handler CreateWebSocketHandler()
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