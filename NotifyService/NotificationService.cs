namespace NotifyService
{
    using System;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Threading;
    using WebSocketSharp;
    using WebSocketSharp.Server;
    
    public class NotificationService : INotificationService
    {
        private readonly WebSocketServer wssv;

        public NotificationService()
        {
            wssv = new WebSocketServer("ws://liveservice.apphb.com");
            wssv.AddWebSocketService<Laputa>("/Laputa");
        }


        public class Laputa : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                var msg = e.Data == "BALUS"
                          ? "I've been balused already..."
                          : "I'm not available now.";

                Send(msg);
            }
        }

        public String StartServer()
        {           
            wssv.Start();

            return String.Format("Server started on {0}", wssv.Address.ToString());
        }

        public String StopServer()
        {
            wssv.Stop();

            return String.Format("Server stoped");
        }
    }
}
