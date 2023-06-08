using FluentValidation;
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
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        public EfRegisterUserCommand(RestaurantAppContext context, RegisterUserValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "User registration";

        public string Description => "";

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);

            Role defaultRole = Context.Roles.FirstOrDefault(x => x.IsDefault);

            if (defaultRole == null)
            {
                throw new InvalidOperationException("Default role doesn't exist.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Role = defaultRole,
                UserName = request.Username,
                Password = passwordHash
            };

            Context.Users.Add(user);

            //Context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            Context.SaveChanges();
        }
    }
}
