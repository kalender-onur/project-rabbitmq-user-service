using Application.Interfaces.RabbitMq;
using Domain.Common;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace Application.Services.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {

        private readonly RabbitMqSettings _settings;
        public RabbitMqService(IOptions<RabbitMqSettings> settings)
        {
            _settings = settings.Value;
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_settings.ConnectionString),
                ClientProvidedName = _settings.ClientProvidedName
            };

            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(_settings.ExchangeName, ExchangeType.Direct);
                    channel.QueueDeclare(_settings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.QueueBind(_settings.QueueName, _settings.ExchangeName, _settings.RoutingKey);

                    byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(_settings.ExchangeName, _settings.RoutingKey, null, messageBodyBytes);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
