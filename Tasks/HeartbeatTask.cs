using EasyNetQ;
using FluentScheduler;
using RabbitMQTest.Messages;
using RabbitMQTest.Services;
using Serilog;

namespace RabbitMQTest.Tasks
{
    public class HeartbeatTask : ITask
    {
        private readonly IBus bus;
        private readonly IClock clock;

        public HeartbeatTask(IBus bus, IClock clock)
        {
            this.bus = bus;
            this.clock = clock;
        }

        public void Execute()
        {
            HeartBeat message = new HeartBeat(clock.Now);
            Log.Information("Heartbeat Task ({now})", message.Now);
            bus.Publish(message);
        }
    }
}