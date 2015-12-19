namespace NotifyServiceClient
{
    using System;
    using System.Threading;
    using System.ServiceModel;
    
    using NotifyServiceClient.BroadcastServiceReference;
    
    using Alchemy;
    using Alchemy.Classes;

    using WebSocket4Net;

    class Program
    {
        static void Main(string[] args)
        {
            //"ws://echo.websocket.org:80/echo"

            //"ws://websockettest-1.apphb.com/"

            //"ws://liveservice.apphb.com/"

            var client = new WebSocket("ws://liveservice.apphb.com/");
            client.Opened += OnConnected;
            client.Closed += OnDisconnect;
            client.MessageReceived += OnReceive;

            client.Open();

            //var callback = new BroadcastCallback();
            //var context = new InstanceContext(callback);

            //using (var service = new BroadcastServiceClient(context))
            //{
            //    do
            //    {
            //        try
            //        {
            //            service.ClientCredentials.UserName.UserName = "test";
            //            service.ClientCredentials.UserName.Password = "test";

            //            service.OpenSession();
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //            Console.WriteLine(ex.StackTrace);
            //        }
            //    }
            //    while (Console.ReadKey(true).Key != ConsoleKey.A);
            //}

            Console.ReadKey();
        }

        private static void OnDisconnect(object sender, EventArgs context)
        {
            Console.WriteLine("{0}: Disconnected", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        private static void OnConnect(object sender, EventArgs context)
        {
            Console.WriteLine("{0}: Connecting...", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        private static void OnConnected(object sender, EventArgs context)
        {
            Console.WriteLine("{0}: Connected", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            var client = sender as WebSocket;

            client.Send("Hey!");
        }

        private static void OnReceive(object sender, MessageReceivedEventArgs context)
        {
            Console.WriteLine("{0}: Message Received:\n{1}\n", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), context.Message);
        }
    }



    public class BroadcastCallback : IBroadcastServiceCallback
    {
        //private EventHandler handler;
        //private SynchronizationContext context;

        //public void SetHandler(EventHandler handler)
        //{
        //    this.handler = handler;
        //    context = AsyncOperationManager.SynchronizationContext;
        //}

        //public void BroadcastToClient(NotifyService.EventDataType eventData)
        //{
        //    //context.Post(new SendOrPostCallback(OnBroadcast), eventData);

        //    Console.WriteLine("> Received callback at {0}", DateTime.Now);
        //}

        public void OnCallback()
        {
            Console.WriteLine("> Received callback at {0}", DateTime.Now);
        }

        //private void OnBroadcast(object eventData)
        //{
        //    handler.Invoke(eventData, null);
        //}

        //public void HandleBroadcast(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var eventData = (NotifyService.EventDataType)sender;
        //        var message = string.Format("{0} (from {1})", eventData.EventMessage, eventData.ClientName);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}