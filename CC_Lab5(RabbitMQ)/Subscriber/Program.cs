using Shared;
using System;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "localhost";
            string queue = QueueNames.NAME_SURNAME;

            var rabbitMQManager = new RabbitMQManager(host);

            using(var connection = rabbitMQManager.Factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                rabbitMQManager.SubscribeQueue(channel, queue, (message) =>
                {
                    Console.WriteLine($">>> Received message: '{message}' <<<");
                });

                Console.ReadKey();
            }
        }
    }
}
