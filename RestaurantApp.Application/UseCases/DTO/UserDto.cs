using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class UserDto
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
