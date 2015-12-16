namespace NotifyService
{
    using System.ServiceModel;

    [ServiceContract] 
    public interface IBroadcastService
    {
        [OperationContract(IsOneWay = true)]
        void RegisterClient(string clientName);

        [OperationContract(IsOneWay = true)]
        void NotifyServer(EventDataType eventData);
    }
}
