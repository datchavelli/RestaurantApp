using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<CategoryDto> ChildCategories { get; set; }
        public IEnumerable<MenuItemDto> MenuItems { get; set; } = new List<MenuItemDto>();
    }
}
