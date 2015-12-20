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
            wssv = new WebSocketServer("ws://liveservice.apphb.com");
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

            return String.Format("Server started on {0}", wssv.Address.ToString());
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
