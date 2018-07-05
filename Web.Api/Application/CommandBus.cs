using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Api.Application
{
    public class CommandBus
    {
        private readonly Dictionary<Type, Func<object, Task>> _handlers =
            new Dictionary<Type, Func<object, Task>>();
        
        public void RegisterHandler<TCommand>(Func<TCommand, Task> handler)
        {
            _handlers.Add(typeof(TCommand), c => Log((TCommand)c, handler));
        }

        private Task Log<TCommand>(TCommand command, Func<TCommand, Task> next)
        {
            Console.WriteLine("Really sophisticated logging");
            return next(command);
        }
        
        public Task Send<TCommand>(TCommand command)
        {
            var handler = _handlers[typeof(TCommand)];
            return handler(command);
        }
    }
}