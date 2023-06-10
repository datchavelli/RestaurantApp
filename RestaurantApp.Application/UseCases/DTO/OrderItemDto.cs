using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public string MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        

    }
}
