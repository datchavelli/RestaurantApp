using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class CreateReservationDto
    {
        public string CustomerName { get; set; }
        public string ReceptionistUsername { get; set; }
        public string ReservationStart { get; set; }
        public string ReservationStatus { get; set; }
        public int GuestCount { get; set; }

    }
}
