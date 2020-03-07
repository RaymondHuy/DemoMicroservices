using DemoMicroservices.ToDoList.Domain.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DemoMicroservices.ToDoList.Domain
{
    public class RabbitMQEventBus : IEventBus
    {
        public void Publish(Event @event)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                RequestedConnectionTimeout = 3000,
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
                channel.QueueDeclare("demo.queue.log", false, false, false, null);
                channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
                channel.BasicQos(0, 1, false);

                string message = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "demo.exchange",
                    routingKey: "demo.queue.abc",
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
