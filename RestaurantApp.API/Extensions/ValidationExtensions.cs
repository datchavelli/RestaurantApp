using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.API.DTO;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApp.API.Extensions
{
    public static class ValidationExtensions
    {

        public static UnprocessableEntityObjectResult ToUnprocessableEntity(this ValidationResult result)
        {
            var errors = result.Errors.Select(x => new ClientErrorDto
            {
                Error = x.ErrorMessage,
                Property = x.PropertyName
            });

            return new UnprocessableEntityObjectResult(errors);
        }
    }
}
