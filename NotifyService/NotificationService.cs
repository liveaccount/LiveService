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


        public String GetNotification(String info)
        {
            //ThreadPool.QueueUserWorkItem((ignore) =>
            //{
                var wssv = new WebSocketServer("ws://localhost:8021");
                wssv.AddWebSocketService<Laputa>("/Laputa");
                wssv.Start();

                info = "started";

                //Thread.Sleep(120000);

                //wssv.Stop();
            //});

            //Thread.Sleep(1500);

            return String.Format("Notification for {0}", info);
        }


        private string GetPorts()
        {
            var info = String.Empty;

            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            foreach (TcpConnectionInformation tcpi in ipGlobalProperties.GetActiveTcpConnections())
            {
                info += tcpi.LocalEndPoint.Port + Environment.NewLine;
            }

            return info;
        }
    }
}
