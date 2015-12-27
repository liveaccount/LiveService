namespace NancyApp.Sessions
{
    using System;
    using System.Collections.Generic;

    using Nancy.AspNet.WebSockets;

    public class Session
    {
        private readonly string name;
        private readonly object guard = new object();
        private readonly IList<WebSocketHandler> handlers;

        public Session(string name)
        {
            this.name = name;

            handlers = new List<WebSocketHandler>();
        }

        public IWebSocketHandler Register(string user)
        {
            var handler = new WebSocketHandler(this, user);

            lock (guard)
            {
                handlers.Add(handler);
            }

            return handler;
        }

        internal void Deregister(WebSocketHandler handler)
        {
            lock (guard)
            {
                handlers.Remove(handler);
            }
        }

        internal void ForEachHandler(Action<WebSocketHandler> action)
        {
            lock (guard)
            {
                foreach (var handler in handlers)
                {
                    action(handler);
                }
            }
        }

        public class WebSocketHandler : IWebSocketHandler
        {
            private IWebSocketClient client;

            private readonly String user;
            private readonly Session session;

            public WebSocketHandler(Session session, String user)
            {
                this.user = user;
                this.session = session;
            }

            private void SendToAll(byte[] data)
            {
                session.ForEachHandler(handler =>
                {
                    //if (handler != this)
                    {
                        handler.client.Send(data);
                    }
                });
            }

            private void SendToAll(string message)
            {
                session.ForEachHandler(handler =>
                {
                    //if (handler != this)
                    {
                        handler.client.Send(message);
                    }
                });
            }

            public void OnOpen(IWebSocketClient client)
            {
                this.client = client;

                SendToAll(string.Format("User {0} connected. Count {1}", user, session.handlers.Count));
            }

            public void OnData(byte[] data)
            {
                SendToAll(data);
            }

            public void OnMessage(string message)
            {
                SendToAll(string.Format("User {0} says: {1}", user, message));
            }

            public void OnClose()
            {
                session.Deregister(this);

                SendToAll(string.Format("User {0} disconnected. Count {1}", user, session.handlers.Count));
            }

            public void OnError()
            {
                SendToAll("Error");
            }

            #region Public Override Methods

            public override string ToString()
            {
                return user;
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

            #endregion Public Override Methods
        }
    }
}