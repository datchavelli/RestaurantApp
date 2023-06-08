using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Exceptions
{
    public class UnauthorizedUseCaseExecutionException : Exception
    {
        public UnauthorizedUseCaseExecutionException(string username, string useCaseName)
            : base($"There was an unauthorized access attempt by {username} for {useCaseName} use case.")
        {

        }
    }
}
