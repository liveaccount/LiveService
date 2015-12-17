namespace NotifyServiceClient
{
    using System;
    using System.Threading;
    using System.ServiceModel;
    using System.ServiceModel.Security;
    using System.ComponentModel;
    using NotifyServiceClient.BroadcastServiceReference;

    class Program
    {
        static void Main(string[] args)
        {
            var callback = new BroadcastCallback();
            var context = new InstanceContext(callback);


            using (var service = new BroadcastServiceClient(context))
            {
                do
                {
                    try
                    {
                        service.ClientCredentials.UserName.UserName = "test";
                        service.ClientCredentials.UserName.Password = "test";
                        service.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;                  
                        
                        service.OpenSession();
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