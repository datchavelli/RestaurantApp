using RestaurantApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCaseHandling
{
    public class TimeTrackingQueryHandler : IQueryHandler
    {
        private IQueryHandler _next;

        public TimeTrackingQueryHandler(IQueryHandler next)
        {
            _next = next;
        }

        public TResult HandleQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
            where TResult : class
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = _next.HandleQuery(query, search);

            stopwatch.Stop();

            Console.WriteLine($"Usecase: {query.Name}, Time: {stopwatch.ElapsedMilliseconds} ms");

            return result;
        }
    }
}
