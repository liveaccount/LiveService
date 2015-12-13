namespace NotifyService
{
    using System;
    
    public class NotificationService : INotificationService
    {
        public String GetNotification(String name)
        {
            return String.Format("Notification for {0}", name);
        }
    }
}
