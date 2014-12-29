using System;

namespace RabbitMQTest.Messages
{
    public class HeartBeat
    {
        public HeartBeat(DateTimeOffset now)
        {
            Now = now;
            Id = Guid.NewGuid();
            HostName = System.Net.Dns.GetHostName();
        }

        public DateTimeOffset Now { get; private set; }
        public Guid Id { get; private set; }
        public string HostName { get; private set; }
    }
}