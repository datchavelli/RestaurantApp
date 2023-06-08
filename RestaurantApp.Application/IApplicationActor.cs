using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application
{
    public interface IApplicationActor
    {
        int Id { get;}
        string Username { get;}
        IEnumerable<int> AllowedUseCases { get;}
    }
}
