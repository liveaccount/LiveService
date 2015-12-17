namespace NotifyService
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BroadcastService : IBroadcastService
    {
        private static Dictionary<string, IBroadcastCallback> clients;
        private static object locker = new object();

        public BroadcastService()
        {
            clients = new Dictionary<string, IBroadcastCallback>();
        }

        public void RegisterClient(string name)
        {
            if (name != null && name != "")
            {
                try
                {
                    var callback = OperationContext.Current.GetCallbackChannel<IBroadcastCallback>();
                    lock (locker)
                    {
                        //remove the old client
                        if (clients.Keys.Contains(name))
                            clients.Remove(name);
                        clients.Add(name, callback);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void NotifyServer(EventDataType eventData)
        {
            lock (locker)
            {
                var inactiveClients = new List<string>();
                foreach (var client in clients)
                {
                    if (client.Key != eventData.ClientName)
                    {
                        try
                        {
                            client.Value.BroadcastToClient(eventData);
                        }
                        catch (Exception ex)
                        {
                            inactiveClients.Add(client.Key);
                        }
                    }
                }

                if (inactiveClients.Count > 0)
                {
                    foreach (var client in inactiveClients)
                    {
                        clients.Remove(client);
                    }
                }
            }
        } 
    } 
}
