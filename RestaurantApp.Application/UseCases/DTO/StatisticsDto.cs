using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class StatisticsDto
    {
        public string User { get; set; }
        public int OrderId { get; set; }
        public string Date { get; set; }
        public int TableNumber { get; set; }
        public decimal Total { get; set; }

    }
}
