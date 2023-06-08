using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities
{
    public class Reservation : Entity
    {
        public string CustomerName { get; set; }
        public int ReceptionistId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int GuestCount { get; set; }
        public ReservationStatus ReservationStatus { get; set; }

        public virtual User Receptionist { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
