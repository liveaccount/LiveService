namespace NancyApp
{
    using System.Collections.Concurrent;

    using Nancy.AspNet.WebSockets;
    using System;

    public class ModuleWebSocket : WebSocketNancyModule
    {
        public ModuleWebSocket()
        {
            WebSocket["/websocket"] = _ =>
            {
                var bag = new HandlerBag();
                return bag.CreateHandler();
            };
        }
    }

    public class WebSocketHandler : IWebSocketHandler
    {
        private IWebSocketClient client;

        private readonly HandlerBag handlers;

        public WebSocketHandler(HandlerBag handlers)
        {
            this.handlers = handlers;
        }

        private void SendToAll(byte[] data)
        {
            handlers.ForEachHandler(handler =>
            {
                handler.client.Send(data);
            });
        }

        private void SendToAll(string message)
        {
            handlers.ForEachHandler(handler =>
            {
                handler.client.Send(message);
            });
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

        public override int GetHashCode()
        {
            return client.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as WebSocketHandler;

            return other != null ? client.Equals(other.client) : false;
        }
    }

    public class HandlerBag : ConcurrentBag<WebSocketHandler>
    {
        public WebSocketHandler CreateHandler()
        {
            var handler = new WebSocketHandler(this);
            this.Add(handler);
            return handler;
        }

        public void ForEachHandler(Action<WebSocketHandler> action)
        {
            foreach (var handler in this)
            {
                action(handler);
            }
        }
    }
}