using System;
using System.Reflection;
using Autofac;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using FluentScheduler;
using RabbitMQTest.Services;
using RabbitMQTest.Tasks;
using Serilog;
using IContainer = Autofac.IContainer;

namespace RabbitMQTest
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var builder = new ContainerBuilder();

            builder.Register(context => RabbitHutch.CreateBus("host=inbeta-coreos.cloudapp.net",
                x => x.Register<IEasyNetQLogger>(p => new SerilogLogger())))
                .As<IBus>()
                .SingleInstance();

            builder.RegisterType<SystemClock>()
                .As<IClock>();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(p => p.IsAssignableTo<ITask>())
                .AsSelf()
                .AsImplementedInterfaces();

            builder.RegisterType<AutofacTaskFactory>()
                .SingleInstance();

            builder.RegisterType<TaskRegistry>()
                .SingleInstance();

            IContainer container = builder.Build();

            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                string subscriptionId = "rabbitmqtest_" + Guid.NewGuid().ToString("N");
                
                TaskManager.TaskFactory = scope.Resolve<AutofacTaskFactory>();
                TaskManager.Initialize(scope.Resolve<TaskRegistry>());

                using (var bus = scope.Resolve<IBus>())
                {
                    var subscriber = new AutoSubscriber(bus, subscriptionId);
                    subscriber.SubscribeAsync(Assembly.GetExecutingAssembly());

                    Console.WriteLine("waiting...");
                    Console.ReadLine();
                }

                Console.WriteLine("exiting...");
            }
        }
    }
}