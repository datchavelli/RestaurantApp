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
    public class MenuItemsController : ControllerBase
    {
        private RestaurantAppContext _context;
        private IQueryHandler _queryHandler;
        private ICommandHandler _commandHandler;

        public MenuItemsController(RestaurantAppContext context, IQueryHandler queryHandler, ICommandHandler commandHandler)
        {
            _context = context;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        // GET: api/<MenuItemsController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] SearchMenuItems search,
                                 [FromServices] ISearchMenuItemsQuery query,
                                 [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }


        // POST api/<MenuItemsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateMenuItemDto dto,
                                  [FromServices] ICreateMenuItemCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id, 
                                  [FromServices] IGetMenuItemQuery query,
                                  [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // PUT api/<MenuItemsController>/5
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] UpdateMenuItemDto dto,
                                [FromServices] IUpdateMenuItemCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // DELETE api/<MenuItemsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id,
                                    [FromServices] IDeleteMenuItemCommand command)
        {
            _commandHandler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
