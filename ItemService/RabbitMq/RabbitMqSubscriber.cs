using ItemService.Event;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ItemService.RabbitMq;

public class RabbitMqSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public RabbitMqSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;

        _connection = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMq:Host"],
            Port = int.Parse(_configuration["RabbitMq:Port"]!)
        }.CreateConnection();

        _channel  = _connection.CreateModel();
        _channel.ExchangeDeclare(
            exchange: "trigger", 
            type: ExchangeType.Fanout);

        _queueName = _channel.QueueDeclare().QueueName;

        _channel.QueueBind(
            queue: _queueName, 
            exchange: "trigger", 
            routingKey: "");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        EventingBasicConsumer consumer = new(_channel);

        consumer.Received += (ModuleHandle, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            _eventProcessor.Process(message);
        };

        _channel.BasicConsume(
            queue: _queueName, 
            autoAck: true, 
            consumer: consumer);

        return Task.CompletedTask;
    }
}
