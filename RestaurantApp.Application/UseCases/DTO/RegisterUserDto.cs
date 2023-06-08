using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class RegisterUserDto
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
