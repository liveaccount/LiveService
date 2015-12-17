namespace NotifyService
{
    using System;
    using System.Linq;
    using System.Timers;
    using System.ServiceModel;
    using System.Collections.Generic;

    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BroadcastService : IBroadcastService
    {
        public static Timer timer;
        public static IBroadcastCallback callback;

        private static Dictionary<string, IBroadcastCallback> clients = new Dictionary<string, IBroadcastCallback>();
        private static object locker = new object();

        //public void RegisterClient(string name)
        //{
        //    if (name != null && name != "")
        //    {
        //        try
        //        {
        //            callback = OperationContext.Current.GetCallbackChannel<IBroadcastCallback>();
        //            //lock (locker)
        //            //{
        //            //    //remove the old client
        //            //    if (clients.Keys.Contains(name))
        //            //        clients.Remove(name);
        //            //    clients.Add(name, callback);
        //            //}

        //            callback.OnCallback();
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }
        //}

        //public void NotifyServer(EventDataType eventData)
        //{
        //    lock (locker)
        //    {
        //        var inactiveClients = new List<string>();
        //        foreach (var client in clients)
        //        {
        //            if (client.Key != eventData.ClientName)
        //            {
        //                try
        //                {
        //                    client.Value.BroadcastToClient(eventData);
        //                }
        //                catch (Exception ex)
        //                {
        //                    inactiveClients.Add(client.Key);
        //                }
        //            }
        //        }

        //        if (inactiveClients.Count > 0)
        //        {
        //            foreach (var client in inactiveClients)
        //            {
        //                clients.Remove(client);
        //            }
        //        }
        //    }
        //} 

        public void OpenSession()
        {
            Console.WriteLine("> Session opened at {0}", DateTime.Now);
            callback = OperationContext.Current.GetCallbackChannel<IBroadcastCallback>();

            timer = new Timer(1000);
            timer.Elapsed += OnTimerElapsed;
            timer.Enabled = true;
        }

        void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            callback.OnCallback();
        }
    } 
}
