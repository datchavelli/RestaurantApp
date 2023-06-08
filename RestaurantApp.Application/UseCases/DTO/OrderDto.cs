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
        public float TotalAmount { get; set; }
        public string Status { get; set; }

        public ReservationDto Reservation { get; set; }
    }
}
