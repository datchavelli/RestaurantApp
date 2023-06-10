using FluentValidation;
using RestaurantApp.Application;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.DataAccess;
using RestaurantApp.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Commands
{
    public class EfUpdateCategoryCommand : EfUseCase, IUpdateCategoryCommand
    {
        private IApplicationActor _actor;
        public EfUpdateCategoryCommand(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 23;

        public string Name => "Update Category";

        public string Description => "Update Category UseCase. For Admins.";

        public void Execute(UpdateCategoryDto request)
        {

            var category = Context.Categories.Find(request.Id);
            var parentId = Context.Categories.Find(request.ParentId);

            if (category == null)
            {
                throw new Exception("Category Id is not valid");
            }

            if (request.ParentId != null)
            {
                if(parentId != null)
                {
                    category.ParentCategoryId = request.ParentId.Value;
                }
                else
                {
                    throw new Exception("Parent Id is not valid");
                }
            }

            if(request.CategoryName != null)
            {
                category.CategoryName = request.CategoryName;
            }


            category.UpdatedAt = DateTime.UtcNow;
            category.UpdatedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
