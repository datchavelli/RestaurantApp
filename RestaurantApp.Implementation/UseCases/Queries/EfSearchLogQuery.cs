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
    public class EfSearchLogQuery : EfUseCase, ISearchLogQuery
    {
        public EfSearchLogQuery(RestaurantAppContext context) : base(context)
        {
        }

        public int Id => 24;

        public string Name => "Check Logs";

        public string Description => "Check Logs from Database.";

        public PagedResponse<LogsDto> Execute(SearchLog search)
        {
            IQueryable<LogEntry> query = Context.LogEntries;

            if(!string.IsNullOrEmpty(search.UseCase))
            {
                query = query.Where(x => x.UseCaseName == search.UseCase); 
            }

            if(!string.IsNullOrEmpty(search.Actor))
            {
                query = query.Where(x => x.Actor == search.Actor);
            }

            if(!string.IsNullOrEmpty(search.Data))
            {
                query = query.Where(x => x.UseCaseData == search.Data);
            }

            return query.ToPagedResponse<LogEntry, LogsDto>(search, x => new LogsDto
            {
                Actor = x.Actor,
                UseCaseData = x.UseCaseData,
                UseCaseName = x.UseCaseName
            });

            }
    }
}
