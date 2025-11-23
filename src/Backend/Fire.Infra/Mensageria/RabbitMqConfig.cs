using RabbitMQ.Client;

namespace Fire.Infra.Mensageria
{
    public class RabbitMqConfig
    {
        public async Task GetConnection()
        {
            var factory = new ConnectionFactory { HostName = "localhost"};
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
        }
    }
}
