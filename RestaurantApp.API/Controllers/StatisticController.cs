using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.Queries.Searches;
using RestaurantApp.Application.UseCaseHandling;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticController : ControllerBase
    {

        // GET api/<StatisticController>
        [HttpGet]
        public IActionResult Get([FromQuery] StatisticFilterSearch search,
                                 [FromServices] IGetStatisticsQuery query,
                                 [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }


    }
}
