using RestaurantApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases
{
    public class EfUseCase
    {
        protected RestaurantAppContext Context { get; }

        protected EfUseCase(RestaurantAppContext context)
        {
            Context = context;
        }
    }
}
