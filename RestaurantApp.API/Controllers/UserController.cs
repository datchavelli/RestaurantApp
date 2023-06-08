using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.UseCaseHandling;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.Application.UseCases.Queries;
using RestaurantApp.Application.UseCases.Queries.Searches;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IQueryHandler _queryHandler;
        private ICommandHandler _commandHandler;

        public UserController(IQueryHandler queryHandler, ICommandHandler commandHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search,
                                 [FromServices] IGetUsersQuery query)
        {
            return Ok(_queryHandler.HandleQuery(query, search));
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

    }

}

