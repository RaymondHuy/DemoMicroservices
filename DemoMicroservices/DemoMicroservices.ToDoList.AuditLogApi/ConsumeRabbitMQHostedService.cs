﻿using DemoMicroservices.ToDoList.AuditLogApi.Entities;
using DemoMicroservices.ToDoList.AuditLogApi.Infrastructure;
using DemoMicroservices.ToDoList.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoMicroservices.ToDoList.AuditLogApi
{
    public class ConsumeRabbitMQHostedService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;

        public ConsumeRabbitMQHostedService(IServiceProvider services, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();
            _services = services;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            _channel.QueueDeclare("demo.queue.log", false, false, false, null);
            _channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body);

                // handle the received message  
                HandleMessage(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("demo.queue.log", false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {
            // we just print this message   
            _logger.LogInformation($"consumer received {content}");

            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AuditLogDbContext>();

                var @event = JsonConvert.DeserializeObject<Event>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                HandleEvent(@event, dbContext);
            }
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        private void HandleEvent(Event @event, AuditLogDbContext dbContext)
        {
            switch (@event)
            {
                case ToDoCompletedEvent completedEvent:
                    dbContext.ToDoLogs.Add(new ToDoLog()
                    {
                        Description = $"User has completed '{completedEvent.Name}' at {completedEvent.CreatedDate.ToShortDateString()}",
                        CreatedDate = completedEvent.CreatedDate
                    });
                    break;
                case ToDoCreatedEvent createdEvent:
                    dbContext.ToDoLogs.Add(new ToDoLog()
                    {
                        Description = $"User has created '{createdEvent.Name}' at {createdEvent.CreatedDate.ToShortDateString()}",
                        CreatedDate = createdEvent.CreatedDate
                    });
                    break;
                case ToDoUncompletedEvent uncompletedEvent:
                    dbContext.ToDoLogs.Add(new ToDoLog()
                    {
                        Description = $"User has uncompleted '{uncompletedEvent.Name}' at {uncompletedEvent.CreatedDate.ToShortDateString()}",
                        CreatedDate = uncompletedEvent.CreatedDate
                    });
                    break;
            }

            dbContext.SaveChanges();
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
