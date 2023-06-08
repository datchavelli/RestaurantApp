using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.Queries.Searches
{
    public class OrderSearch : PagedSearch
    {
        public string Waiter { get; set; }
        public string Reservation { get; set; }
        public string OrderTime { get; set; }
        public string OrderStatus { get; set; }
        public int? TableNumber { get; set; }

    }
}
