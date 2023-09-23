using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class CreateReservationDto
    {
        public int TableNumber { get; set; }
        public string CustomerName { get; set; }
        public string ReservationStart { get; set; }
        public int GuestCount { get; set; }

    }
}
