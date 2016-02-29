namespace NancyApp.Sessions
{
    using System;
    using System.Collections.Generic;

    using Nancy.AspNet.WebSockets;

    public class Session
    {
        private readonly String name;
        private readonly Object guard = new Object();
        private readonly IList<WebSocketHandler> handlers;

        public Session()
        {
            handlers = new List<WebSocketHandler>();
        }

        public Session(String name)
            : this()
        {
            this.name = name;            
        }

        public IWebSocketHandler Register(String user, String code)
        {
            var handler = new WebSocketHandler(this, user, code);

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
            private readonly String code;
            private readonly Session session;

            public WebSocketHandler(Session session, String user, String code)
            {
                this.user = user;
                this.code = code;
                this.session = session;
            }

            private void SendToAll(Byte[] data)
            {
                //session.ForEachHandler(handler =>
                //{
                //    if (handler != this)
                //    {
                //        handler.client.Send(data);
                //    }                    
                //});

                client.Send(data);
            }

            private void SendToAll(String message)
            {
				session.ForEachHandler(handler =>
				{
					//if (handler != this)
					{
						handler.client.Send(message);
					}
				});
				
				//client.Send(message);
            }

            public void OnOpen(IWebSocketClient client)
            {
                this.client = client;

                SendToAll(String.Format("User {0} connected. Count {1}", user, session.handlers.Count));
            }

            public void OnData(Byte[] data)
            {
                SendToAll(data);
            }

            public void OnMessage(String message)
            {
                SendToAll(String.Format("User {0} says: {1}", user, message));
            }

            public void OnClose()
            {
                session.Deregister(this);

                SendToAll(String.Format("User {0} disconnected. Count {1}", user, session.handlers.Count));
            }

            public void OnError()
            {
                SendToAll("Error");
            }

            #region Public Override Methods

            public override String ToString()
            {
                return user;
            }

            public override Int32 GetHashCode()
            {
                return client.GetHashCode();
            }

            public override Boolean Equals(object obj)
            {
                var other = obj as WebSocketHandler;

                return other != null ? client.Equals(other.client) : false;
            }

            #endregion Public Override Methods
        }
    }
}