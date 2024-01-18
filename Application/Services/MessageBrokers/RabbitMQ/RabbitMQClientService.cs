using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MessageBrokers.RabbitMQ;

public class RabbitMQClientService : IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;
    private readonly MessageBrokerOptions _brokerOptions;
    private readonly IConfiguration _configuration;

    public RabbitMQClientService(ConnectionFactory connectionFactory, IConfiguration configuration)
    {
        _connectionFactory = connectionFactory;
        _configuration = configuration;
        _brokerOptions = _configuration.GetSection("MessageBrokers:RabbitMQ:MessageBrokerOptions").Get<MessageBrokerOptions>();
    }

    public IModel Connect()
    {
        _connection = _connectionFactory.CreateConnection();

        if (_channel is { IsOpen: true})
        {
            return _channel;
        }

        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(
            exchange:_brokerOptions.ExchangeName,
            type:"direct",
            durable:true,
            autoDelete:false
            );
        _channel.QueueDeclare(
            queue:_brokerOptions.QueueName,
            durable:true,
            exclusive:false,
            autoDelete:false
            );
        _channel.QueueBind(
            queue:_brokerOptions.QueueName,
            exchange:_brokerOptions.ExchangeName,
            routingKey:_brokerOptions.RoutingKey
            );
        return _channel;
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();

        _connection?.Close();
        _connection?.Dispose();
    }
}
