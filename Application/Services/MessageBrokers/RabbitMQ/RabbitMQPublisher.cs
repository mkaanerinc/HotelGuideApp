using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.MessageBrokers.RabbitMQ;

public class RabbitMQPublisher : IMessageBrokerHelper
{
    private readonly RabbitMQClientService _rabbitmqClientService;
    private readonly IConfiguration _configuration;
    private readonly MessageBrokerOptions _brokerOptions;

    public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService, IConfiguration configuration)
    {
        _rabbitmqClientService = rabbitMQClientService;
        _configuration = configuration;
        _brokerOptions = _configuration.GetSection("MessageBrokers:RabbitMQ:MessageBrokerOptions").Get<MessageBrokerOptions>();
    }

    public void Publish(string location)
    {
        var channel = _rabbitmqClientService.Connect();

        var bodyString = JsonSerializer.Serialize(location);
        var bodyByte = Encoding.UTF8.GetBytes(bodyString);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchange: _brokerOptions.ExchangeName, body: bodyByte,
            routingKey: _brokerOptions.RoutingKey, basicProperties: properties);
    }
}
