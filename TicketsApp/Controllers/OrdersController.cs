using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsApp.Data;
using TicketsApp.Models.Domain;
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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrders = await _context.Orders.Where(o => o.UserId.Equals(userId))
                                                  .Include(t => t.TicketsInOrder)
                                                  .ToListAsync();
            await _context.Tickets.ToListAsync(); // it magically works thanks to this line
            /*var test = await _context.Users.Where(u => u.Id.Equals(userId))
                                         .Include(t => t.UserOrders)//.Include(t => t.UserOrders.TicketsInOrders)
                                         .ToListAsync();
            */
            OrderDto model = new OrderDto
            {
                Orders = userOrders
            };
            return View(model);
        }

        public async Task<IActionResult> Details(Guid? orderId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrders = await _context.Orders.Where(o => o.UserId.Equals(userId) && o.Id == orderId)
                                                  .Include(t => t.TicketsInOrder)
                                                  .SingleOrDefaultAsync();// .ToListAsync();
            await _context.Tickets.ToListAsync(); // the same trick again

            return View(userOrders);
        }
    }
}
