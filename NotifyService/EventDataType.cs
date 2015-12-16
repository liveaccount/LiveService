namespace NotifyService
{
    using System.Runtime.Serialization;

    [DataContract()]
    public class EventDataType
    {
        [DataMember]
        public string ClientName { get; set; }

        [DataMember]
        public string EventMessage { get; set; }
    } 
}
