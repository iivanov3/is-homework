using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsApp.Data;
using TicketsApp.Models.Identity;
using TicketsApp.Web.Models.Domain;
using TicketsApp.Web.Models.DTO;

namespace TicketsApp.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            /*
                select UserId, OrderedOn, TicketId, OrderId, Quantity, Title, Date, Duration, Genre, Rating, Price
                from dbo.Orders
                join dbo.AspNetUsers on dbo.Orders.UserId = dbo.AspNetUsers.Id
                join dbo.TicketsInOrders on dbo.TicketsInOrders.OrderId = dbo.Orders.Id
                join dbo.Tickets on dbo.Tickets.Id = dbo.TicketsInOrders.TicketId;
                where UserId = userId
            */
            // I should try to find a way to implement the same functionality but with Linq
            var allOrders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
            var allTicketsInOrders = await _context.TicketsInOrders.ToListAsync();
            var allTickets = await _context.Tickets.ToListAsync();
            var allUsers = await _context.Users.ToListAsync();
            
            var userOrders = from u in allUsers
                             join o in allOrders on u.Id equals userId
                             join to in allTicketsInOrders on o.Id equals to.OrderId
                             join t in allTickets on to.TicketId equals t.Id
                             select new
                             {
                                 FirstName = u.FirstName, // to check if it works correctly, not needed otherwise
                                 LastName = u.LastName,// well, at least the First Name might be needed
                                 UserName = u.UserName,
                                 OrderedOn = o.OrderedOn,
                                 Quantity = to.Quantity,
                                 Title = t.Title,
                                 Date = t.Date,
                                 Duration = t.Duration,
                                 Genre = t.Genre,
                                 Rating = t.Rating,
                                 Price = t.Price
                             };
            
            CompletedOrderDto model = new CompletedOrderDto();
            model.CompletedOrders = new List<CompletedOrder>();
            foreach (var o in userOrders)
            {
                CompletedOrder order = new CompletedOrder
                {
                    Title = o.Title,
                    OrderedOn = o.OrderedOn,
                    Quantity = o.Quantity,
                    PlayedOn = o.Date,
                    Duration = o.Duration,
                    Price = o.Price,
                    Rating = o.Rating,
                    Genre = o.Genre
                };
                model.CompletedOrders.Add(order);
            }

            return View(model);
        }
    }
}
