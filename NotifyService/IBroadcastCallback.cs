namespace NotifyService
{
    using System.ServiceModel;

    public interface IBroadcastCallback
    {
        [OperationContract(IsOneWay = true)]
        void BroadcastToClient(EventDataType eventData);
    }
}
