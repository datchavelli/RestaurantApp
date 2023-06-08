using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities
{
    
    public class OrderItem : Entity
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quatity { get; set; }
        public int Subtotal { get; set; }

        public virtual MenuItem MenuItem { get; set; }
        public virtual Order Order { get; set; }
    }
}
