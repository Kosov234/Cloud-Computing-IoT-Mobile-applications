using Shared;
using System;


namespace Broadcaster
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostName = "localhost";
            var rabbitMQManager = new RabbitMQManager(hostName);
            while(true)
            {
                Console.WriteLine("Enter your name and surname");
                string fullName = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(fullName))
                {
                    Console.WriteLine("You have to enter your name and surname!");
                    continue;
                }

                if (fullName == "q")
                    return;

                Console.WriteLine("[Start]");
                try
                {
                    rabbitMQManager.SendMessage(QueueNames.NAME_SURNAME, fullName);
                    Console.WriteLine("[Done]");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There's an error: {ex.Message}");
                    Console.ReadKey();
                    return;
                }
            }
        }
    }
}
