using RabbitMQ.Client;
using RestaurantService.Dtos;
using System.Text;
using System.Text.Json;

namespace RestaurantService.RabbitMq;

public class RabbitMqClient : IRabbitMqClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _exchangeName = "trigger";

    public RabbitMqClient(IConfiguration configuration)
    {
        _configuration = configuration;

        _connection = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:Host"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]!)
        }.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
    }

    public void SendRestaurant(RestaurantReadDto restaurantReadDto)
    {
        string message = JsonSerializer.Serialize(restaurantReadDto);
        byte[] body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: "",
            basicProperties: null,
            body: body);
    }
}
