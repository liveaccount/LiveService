namespace NotifyService
{
    using System;

    public class NotificationService : INotificationService
    {      
        public String StartServer()
        {
            return String.Format("Server started");
        }

        public String StopServer()
        {           
            return String.Format("Server stoped");
        }
    }
}
