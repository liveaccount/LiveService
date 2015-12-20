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
                Console.WriteLine("Enter your name: ");
                Console.WriteLine(service.GetNotification(Console.ReadLine()));
            }

            Console.ReadKey();            
        }
    }
}
