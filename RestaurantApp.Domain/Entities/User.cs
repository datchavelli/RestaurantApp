using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Reservation> CreatedReservations { get; set; } = new HashSet<Reservation>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
