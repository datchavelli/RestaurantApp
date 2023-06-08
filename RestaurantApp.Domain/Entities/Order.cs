using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities
{
 
    public class Order : Entity
    {
        public int TableId { get; set; }
        public int? ReservationId { get; set; }
        public int? WaiterId { get; set; }
        public DateTime OrderTime { get; set; }
        public int TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public virtual Reservation Reservation { get; set; }
        public virtual Table Table { get; set; }
        public virtual User Waiter { get; set; }
    }
}
