using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Waiter { get; set; }
        public string TakenAt { get; set; } 
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public int TableNumber { get; set; }
        public int? ReservationId { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
