using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Threading;

namespace RabbitMQApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length>0)
            {
                if (args[0] == "consumer")
                {
                    Consumer consumer = new Consumer();
                    Thread consumerThread = new Thread(consumer.ReceiveMessage);
                    consumerThread.Start();

                }
                else
                {
                    Producer producer = new Producer();
                    producer.SendMessage();
                }
            }
        }
    }
}
