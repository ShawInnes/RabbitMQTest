using System;
using EasyNetQ;
using Serilog;

namespace RabbitMQTest
{
    public class SerilogLogger : IEasyNetQLogger
    {
        public void DebugWrite(string format, params object[] args)
        {
            Log.Debug(format, args);
        }

        public void InfoWrite(string format, params object[] args)
        {
            Log.Information(format, args);
        }

        public void ErrorWrite(string format, params object[] args)
        {
            Log.Error(format, args);
        }

        public void ErrorWrite(Exception exception)
        {
            Log.Error(exception, "");
        }
    }
}