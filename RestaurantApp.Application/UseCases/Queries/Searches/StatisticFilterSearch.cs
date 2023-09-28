using RestaurantApp.Application.UseCases.Queries.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Queries.Searches
{
    public class StatisticFilterSearch : PagedSearch
    {
        public string UserName { get; set; }
        public string LastMonth { get; set; }
        public string AllTime { get; set; }
        public string Today { get; set; }
        public string isAdmin { get; set; }
    }
}
