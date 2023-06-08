using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class PagedResponse<T> 
        where T : class
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        //po strani 3
        //imamo 9 zapisa -> 3
        //imamo 11 zapisa -> 4
        public int PagesCount => (int)Math.Ceiling((float)TotalCount / ItemsPerPage);

        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    }
}
