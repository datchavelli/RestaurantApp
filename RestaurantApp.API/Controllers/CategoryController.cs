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
    public class CategoryController : ControllerBase
    {
        private RestaurantAppContext _context;
        private IQueryHandler _queryHandler;
        private ICommandHandler _commandHandler;

        public CategoryController(RestaurantAppContext context, IQueryHandler queryHandler, ICommandHandler commandHandler)
        {
            _context = context;
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] SearchCategory search,
                               [FromServices] ISearchCategoryQuery query,
                               [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id,
                          [FromServices] IGetCategoryQuery query,
                          [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateCategoryDto dto,
                                  [FromServices] ICreateCategoryCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<CategoryController>/5
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] UpdateCategoryDto dto,
                                 [FromServices] IUpdateCategoryCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id,
                           [FromServices] IDeleteCategoryCommand command)
        {
            _commandHandler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
