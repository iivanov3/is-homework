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

            var userOrders = await _context.Orders.Where(t => t.UserId.Equals(userId))
                                                  .Include(t => t.TicketsInOrder)
                                                  .Include("TicketsInOrder.Ticket")
                                                  .ToListAsync();

            //await _context.Tickets.ToListAsync(); // magically works thanks to this line


            double price = 0.0;
            foreach (var o in userOrders)
            {
                foreach (var t in o.TicketsInOrder)
                {
                    price += (t.Quantity * t.Ticket.Price);
                }
            }
            ViewBag.totalPrice = price;

            return View(userOrders);
        }

        public async Task<IActionResult> Details(Guid? orderId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var model = await _context.TicketsInOrders.Include(t => t.Order)
                                                      .Include(t => t.Ticket)
                                                      .Where(t => t.Order.UserId.Equals(userId) && t.Order.Id == orderId)
                                                      .ToListAsync();

            double price = 0.0;
            foreach (var ticket in model[0].Order.TicketsInOrder)
            {
                price += (ticket.Quantity * ticket.Ticket.Price);
            }
            ViewBag.totalPrice = price;

            return View(model);
        }
    }
}
