using RestaurantApp.Application.Logging;
using RestaurantApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCaseHandling
{
    public class LoggingQueryHandler : IQueryHandler
    {
        private IQueryHandler _next;
        private IApplicationActor _actor;
        private IUseCaseLogger _logger;

        public LoggingQueryHandler(IQueryHandler next, IApplicationActor actor, IUseCaseLogger logger)
        {
            _next = next;
            if (next == null)
            {
                throw new ArgumentException();
            }
            _actor = actor;
            _logger = logger;
        }

        public TResult HandleQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search) where TResult : class
        {
            _logger.Add(new UseCaseLogEntry
            {
                Actor = _actor.Username,
                ActorId = _actor.Id,
                Data = search,
                UseCaseName = query.Name
            });

            return _next.HandleQuery(query, search);
        }
    }
}
