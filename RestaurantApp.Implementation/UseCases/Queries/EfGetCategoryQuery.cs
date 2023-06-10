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
    public class EfGetCategoryQuery : EfUseCase, IGetCategoryQuery
    {
        public EfGetCategoryQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "Get Gategory";

        public string Description => "Get One Category";

        public CategoryDto Execute(int search)
        {
            var query = Context.Categories.Include(x => x.ChildCategories)
                                          .Include(x => x.MenuItems).AsQueryable();

            if (search > 0)
            {
                var check = query.Any(x => x.Id == search);

                if (!check)
                {
                    throw new EntityNotFoundException(search,nameof(CategoryDto));
                }
            }
            else
            {
                throw new Exception("Id not valid");
            }

            var result = query.FirstOrDefault();

            CategoryDto category = new CategoryDto()
            {
                Id = result.Id,
                CategoryName = result.CategoryName,
                ChildCategories = result.ChildCategories.Select(cc => new CategoryDto
                {
                    Id=cc.Id,
                    CategoryName=cc.CategoryName,
                    ChildCategories = null,
                    MenuItems = null
                }),
                MenuItems = result.MenuItems.Select(x => new MenuItemDto
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price
                })
            };

            return category;
        }
    }
}
