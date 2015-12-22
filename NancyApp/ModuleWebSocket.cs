namespace NancyApp
{
    using System;
    using System.Collections.Concurrent;

    using Nancy.AspNet.WebSockets;

    public class ModuleWebSocket : WebSocketNancyModule
    {
        public ModuleWebSocket(IHandlerBagDictionary handlerBagDictionary)
            : base()
        {
            WebSocket["/websocket"] = _ =>
            {
                var handlerBag = handlerBagDictionary.GetOrAdd((string)Request.Query.name ?? "Unknown");
                return handlerBag.CreateHandler();
            };
        }
    }

    public class WebSocketHandler : IWebSocketHandler
    {
        private IWebSocketClient client;

        private readonly String name;
        private readonly HandlerBag handlers;

        public WebSocketHandler(HandlerBag handlers, String name)
        {
            this.name = name;
            this.handlers = handlers;
        }

        private void SendToAll(byte[] data)
        {
            handlers.ForEachHandler(handler =>
            {
                // if(handler != this)
                handler.client.Send(data);
            });
        }

        private void SendToAll(string message)
        {
            handlers.ForEachHandler(handler =>
            {
                // if(handler != this)
                handler.client.Send(message);
            });
        }

        public void OnOpen(IWebSocketClient client)
        {
            this.client = client;

            SendToAll(string.Format("User {0} connected.", name));
        }

        public void OnData(byte[] data)
        {
            SendToAll(data);
        }

        public void OnMessage(string message)
        {
            SendToAll(string.Format("User {0} says: {1} count {2}", name, message, handlers.Count));
        }

        public void OnClose()
        {
            SendToAll(string.Format("User {0} disconnected.", name));
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
        private readonly String name;

        public HandlerBag(String name)
            : base()
        {
            this.name = name;
        }

        public WebSocketHandler CreateHandler()
        {
            var handler = new WebSocketHandler(this, name);
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