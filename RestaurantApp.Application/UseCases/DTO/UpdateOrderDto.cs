using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public decimal? TotalAmount { get; set; }
        public string OrderStatus { get; set; }

        public int? MenuItem { get; set; }

    }
}
