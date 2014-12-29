using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using FluentScheduler;
using Serilog;

namespace RabbitMQTest.Tasks
{
    public class AutofacTaskFactory : ITaskFactory
    {
        private readonly ILifetimeScope container;

        public AutofacTaskFactory(ILifetimeScope container)
        {
            this.container = container;
        }

        public ITask GetTaskInstance<T>() where T : ITask
        {
            Log.Information("Creating Task {Task}", typeof(T).Name);

            try
            {
                return container.Resolve<T>();
            }
            catch (Autofac.Core.Registration.ComponentNotRegisteredException ex)
            {
                Log.Fatal(ex, "Unable to resolve component {Type}", typeof(T).FullName);
            }

            return null;
        }
    }
}
