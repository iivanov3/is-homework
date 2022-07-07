using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        
        public ShoppingCartController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = await _context.Users.Where(u => u.Id.Equals(userId))
                                                   .Include(sc => sc.UserCart)
                                                   .Include(sc => sc.UserCart.TicketsInShoppingCart)
                                                   .Include("UserCart.TicketsInShoppingCart.Ticket")
                                                   .FirstOrDefaultAsync();

            var userShoppingCart = loggedInUser.UserCart;

            var ticketPrices = userShoppingCart.TicketsInShoppingCart.Select(t => new
            {
                TicketPrice = t.Ticket.Price,
                Quantity = t.Quantity
            }).ToList();

            double totalPrice = 0;
            foreach (var ticket in ticketPrices)
            {
                totalPrice += ticket.TicketPrice * ticket.Quantity;
            }

            ShoppingCartDto ShoppingCartDtoItem = new ShoppingCartDto
            {
                TicketsInShoppingCart = userShoppingCart.TicketsInShoppingCart.ToList(),
                TotalPrice = totalPrice
            };

            return View(ShoppingCartDtoItem);
        }

        public async Task<IActionResult> RemoveTicketFromShoppingCart(Guid ticketId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = await _context.Users.Where(u => u.Id.Equals(userId))
                                                   .Include(sc => sc.UserCart)
                                                   .Include(sc => sc.UserCart.TicketsInShoppingCart)
                                                   .Include("UserCart.TicketsInShoppingCart.Ticket")
                                                   .FirstOrDefaultAsync();

            var userShoppingCart = loggedInUser.UserCart;
            var ticketToRemove = userShoppingCart.TicketsInShoppingCart.Where(t => t.TicketId.Equals(ticketId))
                                                                       .FirstOrDefault();
            userShoppingCart.TicketsInShoppingCart.Remove(ticketToRemove);

            _context.Update(userShoppingCart);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "ShoppingCart");
        }

        public async Task<IActionResult> OrderShoppingCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedInUser = await _context.Users.Where(u => u.Id.Equals(userId))
                                                   .Include(sc => sc.UserCart)
                                                   .Include(sc => sc.UserCart.TicketsInShoppingCart)
                                                   .Include("UserCart.TicketsInShoppingCart.Ticket")
                                                   .FirstOrDefaultAsync();
            var userShoppingCart = loggedInUser.UserCart;

            if (userShoppingCart.TicketsInShoppingCart.Count == 0)
            {
                return RedirectToAction("Index", "Tickets");
            }

            Order orderItem = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                User = loggedInUser
            };

            _context.Add(orderItem);

            List<TicketInOrder> ticketsInOrder = new List<TicketInOrder>();
            ticketsInOrder = userShoppingCart.TicketsInShoppingCart
                                             .Select(t => new TicketInOrder
                                             {
                                                 OrderId = orderItem.Id,
                                                 TicketId = t.Ticket.Id,
                                                 Ticket = t.Ticket,
                                                 Order = orderItem,
                                                 Quantity = t.Quantity
                                             }).ToList();

            foreach (var ticket in ticketsInOrder)
            {
                _context.Add(ticket);
            }

            loggedInUser.UserCart.TicketsInShoppingCart.Clear();
            _context.Update(loggedInUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
