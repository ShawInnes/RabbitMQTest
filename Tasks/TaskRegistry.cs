using FluentScheduler;
using Serilog;

namespace RabbitMQTest.Tasks
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Schedule<HeartbeatTask>()
                .ToRunNow()
                .AndEvery(30)
                .Seconds();
        }
    }
}