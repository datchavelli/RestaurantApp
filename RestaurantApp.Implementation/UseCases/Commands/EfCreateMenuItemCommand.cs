using FluentValidation;
using RestaurantApp.Application;
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
    public class EfCreateMenuItemCommand : EfUseCase, ICreateMenuItemCommand
    {
        private IApplicationActor _actor;
        private CreateMenuItemValidator _validator;
        public EfCreateMenuItemCommand(RestaurantAppContext context, IApplicationActor actor, CreateMenuItemValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 12;

        public string Name => "Create new Product";

        public string Description => "Creating a new MenuItem for the Waiters to be able to use for the guests.";

        public void Execute(CreateMenuItemDto request)
        {
            _validator.ValidateAndThrow(request);

            var categoryExists = Context.Categories.FirstOrDefault(c => c.Id == request.CategoryId).Id;

            if(categoryExists == null)
            {
                throw new Exception("Category doesn't exitst");
            }

            if(request.Price <= 0)
            {
                throw new Exception("Price needs to be a valid number");
            }

           

            MenuItem menuItem = new MenuItem
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = categoryExists,
                Price = request.Price,
            };

            Context.MenuItems.Add(menuItem);
            Context.SaveChanges();
        }
    }
}
