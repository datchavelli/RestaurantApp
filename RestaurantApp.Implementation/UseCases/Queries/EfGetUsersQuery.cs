using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.Application.UseCases.Queries.Searches;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using RestaurantApp.Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Queries
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        public EfGetUsersQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 2;

        public string Name => "Get Users";

        public string Description => "Get users using Entity Framework Core.";

        public PagedResponse<UserDto> Execute(UserSearch search)
        {
            IQueryable<User> query = Context.Users;

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(search.Keyword));
            }

            return query.ToPagedResponse<User, UserDto>(search, x => new UserDto
            {
                Id = x.Id,
                RoleId = x.RoleId,
                RoleName = x.Role.Name,
                Username = x.UserName
            }
            );
        }

    }
}
