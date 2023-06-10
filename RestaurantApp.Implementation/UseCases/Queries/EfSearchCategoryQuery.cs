using Microsoft.EntityFrameworkCore;
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
    public class EfSearchCategoryQuery : EfUseCase, ISearchCategoryQuery
    {
        public EfSearchCategoryQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 19;

        public string Name => "Search Category";

        public string Description => "Search Category UseCase.";

        public PagedResponse<CategoryDto> Execute(SearchCategory search)
        {
            IQueryable<Category> query = Context.Categories.Include(x => x.MenuItems);

            if(!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.CategoryName == search.Name);
            }
            if(search.CategoryId > 0)
            {
                query = query.Where(x => x.Id == search.CategoryId);
            }

            if(search.hasChildren == true)
            {
                query = query.Where(x => x.ChildCategories.Count() > 0);
            }

            return query.ToPagedResponse<Category, CategoryDto>(search, x => new CategoryDto
            {
               Id = x.Id,
               CategoryName = x.CategoryName,
               ChildCategories = x.ChildCategories.Select(cc => new CategoryDto
               {
                   Id = cc.Id,
                   CategoryName = cc.CategoryName
               }),
               MenuItems = x.MenuItems.Select(mi => new MenuItemDto
               {
                   Name = mi.Name,
                   Description = mi.Description,
                   Price = mi.Price,
                   CategoryName = mi.Category.CategoryName,
                   CategoryId = mi.CategoryId,
               })
            });
        }
    }
}
