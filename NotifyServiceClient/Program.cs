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
                    try
                    {
                        Console.WriteLine("Enter your name: ");
                        Console.WriteLine(service.GetNotification(Console.ReadLine()));
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
}