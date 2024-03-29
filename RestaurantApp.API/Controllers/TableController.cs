﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.Logging;
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
    public class TableController : ControllerBase
    {
        private RestaurantAppContext _context;
        private ICommandHandler _commandHandler;

        public TableController(RestaurantAppContext context, ICommandHandler commandHandler)
        {
            _context = context;
            _commandHandler = commandHandler;
        }


        // GET: api/<TableController>
        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] TableSearch search,
                                 [FromServices] ISearchTablesQuery query,
                                 [FromServices] IQueryHandler handler)
        {

            return Ok(handler.HandleQuery(query, search));
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id,
                                  [FromServices] IGetTableQuery query,
                                  [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        
       [HttpPost]
       [Authorize]
       public IActionResult Post([FromBody] CreateTableDto dto,
                        [FromServices] ICreateTableCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] UpdateTableDto dto,
                                [FromServices] IUpdateTableCommand command)
        {
            _commandHandler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPatch]
        [Authorize]
        public IActionResult Patch([FromBody] CloseTableDto request, [FromServices] ICloseTableCommand command)
        {
            _commandHandler.HandleCommand(command, request);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id,
                                    [FromServices] IDeleteTableCommand command)
        {
            _commandHandler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
