using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.Queries.Searches
{
    public class SearchCategory : PagedSearch
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public bool? hasChildren { get; set; }

    }
}
