using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQApplication
{
    public class Producer
    {
        string username = string.Empty;
        string message = string.Empty;
        byte[] messageByteArray;
        ConnectionFactory factory;
        object lockObject = new object();
        public Producer()
        {
            factory = new ConnectionFactory() { HostName = "localhost" , UserName="guest" , Password="guest" };
        }

        public void SendMessage()
        {
            Console.Write("Specify a username: ");
            username = Console.ReadLine();

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("communicationExchange", ExchangeType.Direct, false, false, null);
                    channel.QueueDeclare("communicationQueue", false, false, false, null);
                    channel.QueueBind("communicationQueue", "communicationExchange", ExchangeType.Direct, null);
                    while (true)
                    {
                        Console.Write("Message: ");
                        message = Console.ReadLine();
                        message = username + ": " + message;
                        messageByteArray = new byte[1024];
                        messageByteArray = ASCIIEncoding.Default.GetBytes(message);
                        channel.BasicPublish(exchange: "communicationExchange", routingKey: ExchangeType.Direct, basicProperties: null, body: messageByteArray);
                    }
                }
            }
        }
    }
}
