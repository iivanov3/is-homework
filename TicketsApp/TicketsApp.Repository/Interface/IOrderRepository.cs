using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Domain.DomainModels;

namespace TicketsApp.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> GetAllOrders();
        public List<Order> GetUserOrders(string userId);
        public Order GetOrderDetails(Guid orderId);
    }
}
