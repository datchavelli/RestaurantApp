using RestaurantApp.Application;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Implementation.UseCases.Commands
{
    public class EfDeleteCategoryCommand : EfUseCase, IDeleteCategoryCommand
    {
        private IApplicationActor _actor;
        public EfDeleteCategoryCommand(RestaurantAppContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 22;

        public string Name => "Delete Category";

        public string Description => "Delete Category UseCase";

        public void Execute(int request)
        {
            var category = Context.Categories.Find(request);

            if (category == null)
            {
                throw new Exception("Not found");
            }

            if(!category.IsActive)
            {
                throw new Exception("This category is already deleted.");
            }

            var categoryChild = Context.Categories.FirstOrDefault(x => x.ChildCategories.Count() > 0);

            if(categoryChild != null)
            {
                throw new Exception("Unable to delete. This category has Child Categories.");
            }

            /*category.IsActive = false;
            category.DeletedAt = DateTime.UtcNow;
            category.DeletedBy = _actor.Username;*/

            Context.Remove(category);

            Context.SaveChanges();
        }
    }
}
