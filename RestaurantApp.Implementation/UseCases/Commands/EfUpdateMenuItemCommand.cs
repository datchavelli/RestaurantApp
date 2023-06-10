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
    public class EfUpdateMenuItemCommand : EfUseCase, IUpdateMenuItemCommand
    {
        private IApplicationActor _actor;
        private UpdateMenuItemValidator _validator;
        public EfUpdateMenuItemCommand(RestaurantAppContext context, IApplicationActor actor, UpdateMenuItemValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 14;

        public string Name => "Update MenuItem";

        public string Description => "Updating Menu item usecase for administrator.";

        public void Execute(UpdateMenuItemDto request)
        {
            _validator.ValidateAndThrow(request);

            var menuItemId = Context.MenuItems.Where(x => x.Id == request.Id).Select(x => x.Id).FirstOrDefault();
            var categoryId = Context.Categories.Where(x => x.Id == request.CategoryId).Select(x => x.Id).FirstOrDefault();

            if (menuItemId < 0)
            {
                throw new Exception("The item doesn't exist");
            }

            if (categoryId < 0)
            {
                throw new Exception("Invalid Category Id");
            }

            var menuItem = Context.MenuItems.FirstOrDefault(x => x.Id == request.Id);

            if(!string.IsNullOrEmpty(request.Name))
            {
                menuItem.Name = request.Name;
            }

            if(!string.IsNullOrEmpty(request.Description))
            {
                menuItem.Description = request.Description;
            }

            if(categoryId > 0)
            {
                menuItem.CategoryId = categoryId;
            }

            if(request.Price > 0)
            {
                menuItem.Price = request.Price;
            }

            menuItem.UpdatedAt = DateTime.UtcNow;
            menuItem.UpdatedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
