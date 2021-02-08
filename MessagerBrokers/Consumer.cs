using System;
using System.Text;
using System.Text.Json;
using Domain.Models.SendEntities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendEmailAplication;
namespace MenssagerBrokers
{
    public class Consumer
    {
        public static void ListenCommunication()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "achebarato",
                Password = "getout1123"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var exchargeName = "SenderNotification";
                channel.ExchangeDeclare(exchange: exchargeName, type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                             exchange: exchargeName,
                                             routingKey: "");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var entity = JsonConvert.DeserializeObject<SenderEntity>(message);
                        PostSms.SendSms(entity);
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (System.Exception)
                    {

                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }


                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: false,
                                     consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}