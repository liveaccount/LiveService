namespace NotifyService
{
    using System;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class NotificationService : INotificationService
    {
        private readonly WebSocketServer wssv;

        public NotificationService()
        {
            wssv = new WebSocketServer(System.Net.IPAddress.Any, 8080);
            wssv.AddWebSocketService<Laputa>("/Laputa");
        }
        
        public String StartServer()
        {
            try
            {
                wssv.Start();
            }
            catch(Exception ex)
            {
                return String.Format("Error: {0}", ex.Message);
            }

            return String.Format("Server started {0}", wssv.Address);
        }

        public String StopServer()
        {
            try
            {
                wssv.Stop();
            }
            catch (Exception ex)
            {
                return String.Format("Error: {0}", ex.Message);
            }
            
            return String.Format("Server stoped");
        }
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
}
