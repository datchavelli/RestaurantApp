using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Hold
    }
}
