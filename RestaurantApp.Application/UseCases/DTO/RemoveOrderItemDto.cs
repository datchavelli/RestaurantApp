using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class RemoveOrderItemDto
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
    }
}
