namespace NotifyService
{
    using System;
    
    public class NotificationService : INotificationService
    {
        string INotificationService.GetNotification(string name)
        {
            return String.Format("Notification for {0}", name);
        }
    }
}
