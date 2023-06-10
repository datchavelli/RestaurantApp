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
    public class EfCreateTableCommand : EfUseCase, ICreateTableCommand
    {

        private IApplicationActor _actor;
        private CreateTableValidator _validator;

        public EfCreateTableCommand(
            RestaurantAppContext context, 
            IApplicationActor actor, 
            CreateTableValidator validator) : base(context)
        {
            _actor = actor;
            _validator = validator;
        }

        public int Id => 4;

        public string Name => "Create Table";

        public string Description => "Creating table UseCase for Admins.";

        public void Execute(CreateTableDto request)
        {
            _validator.ValidateAndThrow(request);

            var tableCheck = Context.Tables.FirstOrDefault(x => x.TableNumber == request.TableNumber);

            if(tableCheck == null)
            {
                throw new Exception("Table with that number already exists");
            }

            Table table = new Table()
            {
                TableNumber = request.TableNumber,
                Capacity = request.Capacity
            };

            Context.Add(table);
            Context.SaveChanges();
        }
    }
}
