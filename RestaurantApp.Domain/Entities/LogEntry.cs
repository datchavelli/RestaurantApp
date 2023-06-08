using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities
{
    public class LogEntry : Entity
    {
        public string Actor { get; set; }
        public int ActorId { get; set; }
        public string UseCaseName { get; set; }
        public string UseCaseData { get; set; }
    }
}
