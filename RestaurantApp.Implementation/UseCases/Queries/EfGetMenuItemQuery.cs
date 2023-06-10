using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.Exceptions;
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
            var query = Context.MenuItems.Include(x => x.Category).AsQueryable();

            if (search > 0)
            {
                var check = query.Any(x => x.Id == search);

                if (!check)
                {
                    throw new EntityNotFoundException(search, nameof(MenuItemDto));
                }
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
