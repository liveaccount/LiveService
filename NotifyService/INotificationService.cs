namespace NotifyService
{
    using System;    
    using System.ServiceModel;

    [ServiceContract]
    public interface INotificationService
    {
        [OperationContract]
        String StartServer();

        [OperationContract]
        String StopServer();
    }
}
