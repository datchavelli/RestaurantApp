using System;
using System.Collections.Generic;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class TableDto
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }

        public IEnumerable<OrderDto> Orders { get; set; } = new List<OrderDto>();
    }

}
