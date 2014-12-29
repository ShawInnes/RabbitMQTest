using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using RabbitMQTest.Messages;
using Serilog;

namespace RabbitMQTest.Consumers
{
    public class HeartBeatConsumer : IConsumeAsync<HeartBeat>
    {
        public async Task Consume(HeartBeat message)
        {
            Log.Information("Received Message {@msg}", message);
        }
    }
}