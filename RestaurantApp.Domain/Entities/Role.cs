using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<RoleUseCase> RoleUseCases { get; set; }
    }
}
