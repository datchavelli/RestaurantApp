using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class UpdateReservationDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? GuestCount { get; set; }
        public string ReservationStatus { get; set; }
    }
}
