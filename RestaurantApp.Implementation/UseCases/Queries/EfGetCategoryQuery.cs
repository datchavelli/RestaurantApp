using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.DataAccess;
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
            var query = Context.Categories.AsQueryable();

            if (search > 0)
            {
                query = query.Where(x => x.Id == search);
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
