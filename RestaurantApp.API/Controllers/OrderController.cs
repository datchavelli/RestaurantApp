using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.UseCaseHandling;
using RestaurantApp.DataAccess;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get([FromBody] OrderSearch search,
                          [FromServices] ISearchOrderQuery query,
                          [FromServices] IQueryHandler handler)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
