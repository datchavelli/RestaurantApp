using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Queries
{
    public class EfGetMenuItemQuery : EfUseCase, IGetMenuItemQuery
    {
        public EfGetMenuItemQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 13;

        public string Name => "Get One menu Item";

        public string Description => "Get details of one Menu Item.";

        public MenuItemOneDto Execute(int search)
        {
            var query = Context.MenuItems.AsQueryable();

            if (search > 0)
            {
                query = query.Where(x => x.Id == search);
            }
            else
            {
                throw new Exception("Id not valid");
            }

            var result = query.FirstOrDefault();

            MenuItemOneDto menuItem = new MenuItemOneDto()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CategoryId = result.CategoryId,
                CategoryName = result.Category.CategoryName,
                Price = result.Price
            };

            return menuItem;
        }
    }
}
