using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.UseCaseHandling;
using RestaurantApp.Application.UseCases.Commands;
using RestaurantApp.Application.UseCases.DTO;
using RestaurantApp.DataAccess;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceOrderController : ControllerBase
    {
        private RestaurantAppContext _context;
        private ICommandHandler _commandHandler;

        public ServiceOrderController(RestaurantAppContext context, ICommandHandler commandHandler)
        {
            _context = context;
            _commandHandler = commandHandler;
        }

        // POST api/<ServiceOrderController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateServiceOrderDto dto,
                                  [FromServices] IServiceOrderCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete([FromBody] RemoveOrderItemDto dto,
                                    [FromServices] IRemoveOrderCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(204);
        }

    }
}
