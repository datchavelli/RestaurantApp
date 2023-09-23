using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.UseCases;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.Application.UseCases.Queries.Searches;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Queries
{
    public class EfSearchMenuItemsQuery : EfUseCase, ISearchMenuItemsQuery
    {

        public EfSearchMenuItemsQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 11;

        public string Name => "Menu Search";

        public string Description => "Search menus usecase.";

        

        public PagedResponse<MenuItemDto> Execute(SearchMenuItems search)
        {
            IQueryable<MenuItem> query = Context.MenuItems.Include(x => x.Category);

            if(!string.IsNullOrEmpty(search.MenuItemName))
            {
                query = query.Where(x => x.Name == search.MenuItemName);
            }

            if(!string.IsNullOrEmpty(search.Category))
            {
                query = query.Where(x => x.Category.CategoryName == search.Category);
            }

            if(search.Price > 0)
            {
                query = query.Where(x => x.Price == search.Price);
            }

            return query.ToPagedResponse<MenuItem, MenuItemDto>(search, x => new MenuItemDto
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description
            }
            );

        }
    }
}
