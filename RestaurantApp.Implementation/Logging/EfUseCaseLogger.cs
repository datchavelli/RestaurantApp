using Newtonsoft.Json;
using RestaurantApp.Application.Logging;
using RestaurantApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.Logging
{
    public class EfUseCaseLogger : IUseCaseLogger
    {
        private readonly RestaurantAppContext _context;

        public EfUseCaseLogger(RestaurantAppContext context)
        {
            _context = context;
        }

        public void Add(UseCaseLogEntry entry)
        {
            _context.LogEntries.Add(new Domain.Entities.LogEntry
            {
                Actor = entry.Actor,
                ActorId = entry.ActorId,
                UseCaseData = JsonConvert.SerializeObject(entry.Data),
                UseCaseName = entry.UseCaseName,
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();
        }
    }
}
