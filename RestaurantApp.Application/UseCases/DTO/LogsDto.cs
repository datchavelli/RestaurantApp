using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class LogsDto
    {
        public string Actor { get; set; }
        public string UseCaseName { get; set; }
        public string UseCaseData { get; set; }
    }
}
