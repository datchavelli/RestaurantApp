using FluentValidation;
using RestaurantApp.Application;
using RestaurantApp.Application.Exceptions;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Commands
{
    public class EfCreateCategoryCommand : EfUseCase, ICreateCategoryCommand
    {
        private IApplicationActor _actor;
        private CreateCategoryValidator _validator;
        public EfCreateCategoryCommand(RestaurantAppContext context, IApplicationActor actor, CreateCategoryValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 20;

        public string Name => "Create Category";

        public string Description => "Add new Category with possible parrent.";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var categoryExists = Context.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == request.CategoryName.ToLower());
            var parentExists = Context.Categories.FirstOrDefault(c => c.Id == request.ParentCategoryId);

            if (categoryExists != null)
            {
                throw new EntityAlreadyExistsException(categoryExists.Id,"Category");
            }

            Category category = new Category
            {
                CategoryName = request.CategoryName
            };

            if (parentExists == null)
            {
                category = new Category
                {
                    CategoryName = request.CategoryName,
                    ParentCategoryId = request.ParentCategoryId
                };
            }

            

            Context.Categories.Add(category);
            Context.SaveChanges();
        }
    }
}
