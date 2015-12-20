namespace NotifyServiceClient
{
    using System;

    using NotifyServiceReference;

    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new NotificationServiceClient())
            {
                Console.WriteLine(service.StartServer());
                Console.ReadKey();
                Console.WriteLine(service.StopServer());
            }                    
        }
    }
}
