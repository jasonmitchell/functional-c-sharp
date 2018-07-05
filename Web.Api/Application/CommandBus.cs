using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Api.Application
{
    public delegate Task CommandHandler<TCommand>(TCommand command);
    
    public class CommandBus
    {
        private readonly Dictionary<Type, CommandHandler<object>> _handlers =
            new Dictionary<Type, CommandHandler<object>>();
        
        public void RegisterHandler<TCommand>(CommandHandler<TCommand> handler)
        {
            _handlers.Add(typeof(TCommand), c => Log((TCommand)c, handler));
        }

        private Task Log<TCommand>(TCommand command, CommandHandler<TCommand> next)
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