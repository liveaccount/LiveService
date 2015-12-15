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
                do
                {
                    Console.WriteLine("Enter your name: ");
                    Console.WriteLine(service.GetNotification(Console.ReadLine()));
                }
                while (Console.ReadKey(true).Key != ConsoleKey.A);
            }

            Console.ReadKey();            
        }
    }
}