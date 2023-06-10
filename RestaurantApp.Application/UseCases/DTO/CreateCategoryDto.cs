using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
