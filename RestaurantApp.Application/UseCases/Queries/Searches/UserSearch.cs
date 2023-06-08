using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.Queries.Searches
{
    public class UserSearch : PagedSearch
    {
        public string Keyword { get; set; }
    }
}
