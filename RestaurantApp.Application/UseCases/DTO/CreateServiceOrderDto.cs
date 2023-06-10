using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class CreateServiceOrderDto
    {
        public List<int> MenuItems { get; set; }
        public List<int> MenuQuantity { get; set; }
        public int? TableNumber { get; set; }

    }
}
