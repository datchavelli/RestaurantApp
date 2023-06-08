using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class CreateOrderDto
    {
        public string OrderStatus { get; set; }
        public int TableNumber { get; set; }

    }
}
