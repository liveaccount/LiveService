namespace NotifyService
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    
    public class NotificationService : INotificationService
    {
        private string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        
        public String GetNotification(String name)
        {
            var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8080));
            serverSocket.Listen(128);
            //serverSocket.BeginAccept(null, 0, OnAccept, null);




            return String.Format("Notification for {0}", name);
        }
    }
}
