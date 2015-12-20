namespace NotifyService
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using WebSocketSharp;
    using WebSocketSharp.Server;
    
    public class NotificationService : INotificationService
    {

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


        public String GetNotification(String name)
        {
            ThreadPool.QueueUserWorkItem((ignore) =>
            {
                var wssv = new WebSocketServer("ws://liveservice.apphb.com");
                wssv.AddWebSocketService<Laputa>("/Laputa");
                wssv.Start();

                Thread.Sleep(120000);

                wssv.Stop();
            });


            return String.Format("Notification for {0}", name);
        }
    }
}
