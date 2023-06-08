using RestaurantApp.Application;
using System.Collections.Generic;

namespace RestaurantApp.API.Jwt
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;

        public string Username => "guest";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1, 2};
    }
}
