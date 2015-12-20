namespace NotifyService
{
    using SuperSocket.SocketBase;
    using SuperSocket.SocketBase.Config;
    using SuperSocket.SocketEngine;
    using SuperWebSocket;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    
    public class NotificationService : INotificationService
    {

        public String GetNotification(String name)
        {
            var wsServer = new WebSocketServer();

            wsServer.Setup(new RootConfig(), new ServerConfig
            {
                Name = "SuperWebSocket",
                Ip = "Any",
                Port = 8181,
                Mode = SocketMode.Tcp,
            }, new SocketServerFactory());

            wsServer.Start();


            return String.Format("Notification for {0}", name);
        }
    }
}
