using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.Queries.Searches
{
    public class SearchLog : PagedSearch
    {
        public string Data { get; set; }
        public string Actor { get; set; }
        public string UseCase { get; set; }
    }
}
