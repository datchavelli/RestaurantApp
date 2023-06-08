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
    public class OrderController : ControllerBase
    {
        private RestaurantAppContext _context;
        private IQueryHandler _queryHandler;
        private ICommandHandler _commandHandler;

        public OrderController(RestaurantAppContext context, IQueryHandler queryHandler, ICommandHandler commandHandler)
        {
            _context = context;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        // GET: api/<OrderController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromBody] OrderSearch search,
                          [FromServices] ISearchOrderQuery query,
                          [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        

        // POST api/<OrderController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateOrderDto dto,
                                  [FromServices] ICreateOrderCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
