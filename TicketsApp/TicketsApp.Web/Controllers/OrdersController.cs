using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TicketsApp.Services.Interface;

namespace TicketsApp.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = _orderService.GetUserOrders(userId);

            return View(model);
        }

        public IActionResult Details(Guid orderId)
        {
            var model = _orderService.GetOrderDetails(orderId);

            return View(model);
        }
    }
}
