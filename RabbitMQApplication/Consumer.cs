using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using RabbitMQ.Client.MessagePatterns;

namespace RabbitMQApplication
{
    public class Consumer
    {
        byte[] messageByteArray;
        string message = string.Empty;
        ConnectionFactory factory;
        public Consumer()
        {
            factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        }

        public void ReceiveMessage()
        {
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("communicationQueue", false, false, false, null);
                    Subscription subscription = new Subscription(channel, "communicationQueue",false);

                    while (true)
                    {
                        BasicDeliverEventArgs basicDeliver = subscription.Next();
                        message = ASCIIEncoding.Default.GetString(basicDeliver.Body);
                        Console.WriteLine(message);
                        subscription.Ack(basicDeliver);
                    }
                }
            }
        }
    }
}
