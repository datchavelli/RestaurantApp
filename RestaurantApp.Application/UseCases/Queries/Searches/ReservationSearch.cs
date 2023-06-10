using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.Queries.Searches
{
    public class ReservationSearch : PagedSearch
    {
        public int? Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime? ReservationTime { get; set; }
        public int? GuestCount { get; set; }
        public string ReservedBy { get; set; }
        public string Status { get; set; }
    }
}
