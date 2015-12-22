namespace NancyApp
{
    using System.Collections.Concurrent;

    using Nancy.AspNet.WebSockets;
    using System;

    public class ModuleWebSocket : WebSocketNancyModule
    {
        public ModuleWebSocket(IHandlerBagDictionary handlerBagDictionary)
            : base("/websocket")
        {
            WebSocket["/{path}"] = _ =>
            {
                var handlerBag = handlerBagDictionary.GetOrAdd((String)_.path);
                return new HandlerBag().CreateHandler();//handlerBag.CreateHandler();
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

    public interface IHandlerBagDictionary
    {
        HandlerBag GetOrAdd(String id);
    }

    public class HandlerBagDictionary : IHandlerBagDictionary
    {
        private readonly ConcurrentDictionary<String, HandlerBag> dictionary;

        public HandlerBagDictionary()
        {
            dictionary = new ConcurrentDictionary<string, HandlerBag>();
        }

        public HandlerBag GetOrAdd(String id)
        {
            return dictionary.GetOrAdd(id, new HandlerBag());
        }
    }
}