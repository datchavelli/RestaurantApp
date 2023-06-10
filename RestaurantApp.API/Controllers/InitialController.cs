using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.Logging;
using RestaurantApp.DataAccess;
using RestaurantApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialController : ControllerBase
    {

        private  RestaurantAppContext _context;

        public InitialController(RestaurantAppContext context)
        {
            _context = context;
        }


        // POST api/<InitialController>
        [HttpPost]
        public IActionResult Post()
        {
            // FALE USE-CASE-ovi i Fali return
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Waiter",
                },

                new Role()
                {
                    Name = "Receptionist"
                },

                new Role()
                {
                    Name = "Administrator"
                }
            };


            var masterRole = new List<RoleUseCase>();

            for (int i = 1; i < 30; i++)
            {
                masterRole.Add(new RoleUseCase { Role = roles.ElementAt(2), UseCaseId = i });
            }

            int[] waiterUseCases = { 2,3,7,8,9,10,11,13,15,16,19,21,26,28,30};
            int[] receptionistUseCases = { 2,3,5,6,7,11,13,19,21,25,26,27,28};


            var waiterRoles = new List<RoleUseCase>();
            for (int i = 0; i < waiterUseCases.Length; i++)
            {
                waiterRoles.Add(new RoleUseCase { Role = roles.ElementAt(0), UseCaseId = waiterUseCases[i] });
            }

            var receptionistRoles = new List<RoleUseCase>();
            for (int i = 0; i < receptionistUseCases.Length; i++)
            {
                receptionistRoles.Add(new RoleUseCase { Role = roles.ElementAt(1), UseCaseId = receptionistUseCases[i] });
            }


            var roleUseCase = new List<RoleUseCase>();


            roleUseCase.AddRange(masterRole);
            roleUseCase.AddRange(waiterRoles);
            roleUseCase.AddRange(receptionistRoles);


            var users = new List<User>()
            {
                new User()
                {
                    UserName = "stefke",
                    Password = BCrypt.Net.BCrypt.HashPassword("stef992"),
                    Role = roles.ElementAt(0)
                },
                new User()
                {
                    UserName = "parke",
                    Password = BCrypt.Net.BCrypt.HashPassword("parsic95"),
                    Role = roles.ElementAt(1)
                },
                new User()
                {
                    UserName = "dani",
                    Password = BCrypt.Net.BCrypt.HashPassword("Kalifornija123!"),
                    Role = roles.ElementAt(2)  
                }
            };

            var categories = new List<Category>()
            {
                new Category()
                {
                    CategoryName = "Sa Rostilja"
                },
                new Category()
                {
                    CategoryName = "Kuvano",
                },
                new Category()
                {
                    CategoryName = "Salate"
                }
            };

            var menuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Name = "Cezar Salata",
                    Category = categories.ElementAt(2),
                    Description = "Cezar salata neki opis.",
                    Price = 350
                },
                new MenuItem()
                {
                    Name = "Dimnjena vesalica",
                    Category = categories.ElementAt(0),
                    Description = "Dimnjena vesalica neki opis.",
                    Price = 450
                },
                new MenuItem()
                {
                    Name = "Riblja corba",
                    Category = categories.ElementAt(1),
                    Description = "Riblja corba neki opis.",
                    Price = 500
                },
            };

            var tables = new List<Table>()
            {
                new Table()
                {
                    TableNumber = 1,
                    Capacity = 5,
                    Status = TableStatus.Available
                },
                new Table()
                {
                    TableNumber = 2,
                    Capacity = 7,
                    Status = TableStatus.Available
                },
                new Table()
                {
                    TableNumber = 3,
                    Status = TableStatus.Reserved,
                    Capacity = 4
                }
            };

            var reservations = new List<Reservation>()
            {
                new Reservation()
                {
                    CustomerName = "Branko",
                    ReservationDate = DateTime.UtcNow,
                    StartTime = DateTime.UtcNow.AddDays(3),
                    GuestCount = 4,
                    Receptionist = users.ElementAt(1)
                }
            };


            var orders = new List<Order>()
            {
                new Order()
                {
                    Reservation = reservations.ElementAt(0),
                    Table = tables.ElementAt(2),
                    OrderStatus = OrderStatus.Pending,
                    OrderTime = DateTime.UtcNow,
                    TotalAmount = 700,
                    Waiter = users.ElementAt(0)
                }
            };

            var orderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Order = orders.ElementAt(0),
                    MenuItem = menuItems.ElementAt(0),
                    Quatity = 2,
                    Subtotal = 700
                    
                }
            };


            _context.AddRange(roles);
            _context.AddRange(roleUseCase);
            _context.AddRange(users);
            _context.AddRange(categories);
            _context.AddRange(menuItems);
            _context.AddRange(tables);
            _context.AddRange(reservations);
            _context.AddRange(orders);
            _context.AddRange(orderItems);

            _context.SaveChanges();

            return StatusCode(201);

        }

    }
}
