namespace NotifyServiceClient
{
    using System;
    using System.Threading;
    using System.ComponentModel;

    using NotifyService;
    using System.ServiceModel;

    class Program
    {
        static void Main(string[] args)
        {
            var callback = new BroadcastCallback();

            //callback.SetHandler(HandleBroadcast);

            var context = new InstanceContext(callback);


            using (var service = new BroadcastServiceReference.BroadcastServiceClient(new InstanceContext(new BroadcastCallback())))
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Enter your name: ");
                        var name = Console.ReadLine();
                        service.RegisterClient(name);
                        Console.WriteLine("OK");
                        service.NotifyServer(
                                    new EventDataType()
                                    {
                                        ClientName = name,
                                        EventMessage = "xxx"
                                    });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                while (Console.ReadKey(true).Key != ConsoleKey.A);
            }

            Console.ReadKey();
        }
    }

    public class BroadcastCallback : IBroadcastCallback
    {
        private EventHandler handler;
        private SynchronizationContext context;

        public void SetHandler(EventHandler handler)
        {
            this.handler = handler;
            context = AsyncOperationManager.SynchronizationContext;
        }

        public void BroadcastToClient(EventDataType eventData)
        {
            context.Post(new SendOrPostCallback(OnBroadcast), eventData);
        }

        private void OnBroadcast(object eventData)
        {
            handler.Invoke(eventData, null);
        }

        public void HandleBroadcast(object sender, EventArgs e)
        {
            try
            {
                var eventData = (EventDataType)sender;
                var message = string.Format("{0} (from {1})", eventData.EventMessage, eventData.ClientName);
            }
            catch (Exception ex)
            {
            }
        }
    }
}