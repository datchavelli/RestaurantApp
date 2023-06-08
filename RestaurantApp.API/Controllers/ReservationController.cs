using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.UseCaseHandling;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.Application.UseCases.Queries.Searches;
using RestaurantApp.DataAccess;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private RestaurantAppContext _context;
        private IQueryHandler _queryHandler;
        private ICommandHandler _commandHandler;

        public ReservationController(RestaurantAppContext context, IQueryHandler queryHandler, ICommandHandler commandHandler)
        {
            _context = context;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        // GET: api/<ReservationController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromBody] ReservationSearch search,
                                 [FromServices] ISearchReservationsQuery query,
                                 [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }


        // POST api/<ReservationController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateReservationDto dto,
                                  [FromServices] ICreateReservationCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<ReservationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
