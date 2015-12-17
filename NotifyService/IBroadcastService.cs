namespace NotifyService
{
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(IBroadcastCallback))] 
    public interface IBroadcastService
    {
        [OperationContract(IsOneWay = true)]
        void RegisterClient(string clientName);

        [OperationContract(IsOneWay = true)]
        void NotifyServer(EventDataType eventData);
    }
}
