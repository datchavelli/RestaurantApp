using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Logging
{
    public interface IUseCaseLogger
    {
        void Add(UseCaseLogEntry entry);
    }

    public class UseCaseLogEntry
    {
        public string Actor { get; set; }
        public int ActorId { get; set; }
        public object Data { get; set; }
        public string UseCaseName { get; set; }
    }
}
