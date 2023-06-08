using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities
{
    public class Category : Entity
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual IEnumerable<Category> ChildCategories { get; set; } = new HashSet<Category>();
        public virtual IEnumerable<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
    }
}
