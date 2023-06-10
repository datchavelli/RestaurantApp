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
    public class EfUpdateTableCommand : EfUseCase, IUpdateTableCommand
    {
        private IApplicationActor _actor;
        private UpdateTableValidator _validator;
        public EfUpdateTableCommand(RestaurantAppContext context, IApplicationActor actor, UpdateTableValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 18;

        public string Name => "Update Table";

        public string Description => "Update table command. Only for Admins.";

        public void Execute(UpdateTableDto request)
        {
            _validator.ValidateAndThrow(request);

            var table = Context.Tables.Find(request.Id);

            if(table == null)
            {
                throw new Exception("Table Id is not valid");
            }

            if(request.TableNumber != null)
            {
                table.TableNumber = request.TableNumber.Value;
            }

            if(request.Capacity != null)
            {
                table.Capacity = request.Capacity.Value;
            }

            table.UpdatedAt = DateTime.UtcNow;
            table.UpdatedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
