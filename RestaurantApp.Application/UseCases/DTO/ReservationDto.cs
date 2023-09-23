using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ReservationDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int GuestCount { get; set; }
        public string ReservedBy { get; set; }
        public string Status { get; set; }  

        public IEnumerable<TableDto> Table { get; set; }
    }
}
