using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using TicketsApp.Services.Interface;

namespace TicketsApp.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.GetShoppingCartInfo(userId);

            return View(result);
        }

        public IActionResult RemoveTicketFromShoppingCart(Guid ticketId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _shoppingCartService.RemoveTicketFromShoppingCart(userId, ticketId);

            if (!result)
            {
                return RedirectToAction("Index", "Tickets");
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult OrderShoppingCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _shoppingCartService.MakeOrder(userId);

            if (!result)
            {
                return RedirectToAction("Index", "Tickets");
            }

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
