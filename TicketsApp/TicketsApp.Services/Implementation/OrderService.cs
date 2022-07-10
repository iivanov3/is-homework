using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Domain.DomainModels;
using TicketsApp.Domain.DTO;
using TicketsApp.Repository.Interface;
using TicketsApp.Services.Interface;

namespace TicketsApp.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public List<Order> GetUserOrders(string userId)
        {
            return _orderRepository.GetUserOrders(userId);
        }

        public OrderDto GetOrderDetails(Guid orderId)
        {
            var result = _orderRepository.GetOrderDetails(orderId);

            double price = 0.0;
            foreach (var t in result.TicketsInOrder)
            {
                price += (t.Quantity * t.Ticket.Price);
            }

            OrderDto order = new OrderDto
            {
                Order = result,
                TotalPrice = price
            };

            return order;
        }
    }
}
