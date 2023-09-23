using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(int id, string entityType)
           : base($"Entity of type {entityType} with an id of {id} already exists.")
        {

        }
    }
}
