using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.Queries.Searches
{
    public class SearchMenuItems : PagedSearch
    {
        public string MenuItemName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
