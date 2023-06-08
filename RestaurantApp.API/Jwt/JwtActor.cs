using RestaurantApp.Application;
using System.Collections.Generic;

namespace RestaurantApp.API.Jwt
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<int> AllowedUseCases { get; set; }
    }
}
