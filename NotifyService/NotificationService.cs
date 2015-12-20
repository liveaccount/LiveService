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
            var wssv = new WebSocketServer("ws://localhost:8021");
            wssv.AddWebSocketService<Laputa>("/Laputa");
            wssv.Start();

            info = "started";

            return String.Format("Notification for {0}", wssv.Address.ToString());
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
