namespace NotifyServiceClient
{
    using System;
    using NotifyServiceClient.BroadcastServiceReference;
    
    
    using WebSocket4Net;
    using SuperSocket.ClientEngine;

    class Program
    {
        static WebSocket websocket;

        static void Main(string[] args)
        {
            websocket = new WebSocket("ws://liveservice.apphb.com/NotifyService.svc", "", null, null, "UserAgent", "http://liveservice.apphb.com/", WebSocketVersion.DraftHybi10);

            
            websocket.Error += websocket_Error;
            websocket.Opened += websocket_Opened;
            //websocket.Closed += new EventHandler(websocket_Closed);
            websocket.MessageReceived += websocket_MessageReceived;
            websocket.Open();


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
            //            service.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

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

        private static void websocket_Opened(object sender, EventArgs e)
        {
            websocket.Send("Hello World!");
        }

        private static void websocket_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
        }

        private static void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            websocket.Send(e.Message);
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