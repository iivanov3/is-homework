using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Domain.DomainModels;
using TicketsApp.Domain.DTO;

namespace TicketsApp.Services.Interface
{
    public interface IOrderService
    {
        public List<Order> GetAllOrders();
        public List<Order> GetUserOrders(string userId);
        public OrderDto GetOrderDetails(Guid orderId);
    }
}
