namespace NotifyService
{
    using System;

    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class NotificationService : INotificationService
    {
        private readonly HttpServer ws;

        public NotificationService()
        {
            //ws = new HttpServer();
            //ws.AddWebSocketService<Laputa>("/Laputa");
        }
        
        public String StartServer()
        {
            try
            {
                //ws.Start();
            }
            catch(Exception ex)
            {
                return String.Format("Error: {0}", ex.Message);
            }

            return String.Format("Server started");
        }

        public String StopServer()
        {
            try
            {
                //ws.Stop();
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
