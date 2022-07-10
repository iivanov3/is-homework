using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketsApp.Domain.DomainModels;
using TicketsApp.Domain.DTO;
using TicketsApp.Repository.Interface;
using TicketsApp.Services.Interface;

namespace TicketsApp.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;
    
        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository,
                                   IRepository<TicketInOrder> ticketInOrderRepository, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _userRepository = userRepository;
        }

        public ShoppingCartDto GetShoppingCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);
            var userShoppingCart = loggedInUser.UserCart;
            var userTickets = userShoppingCart.TicketsInShoppingCart.ToList();

            double totalPrice = 0.0;
            foreach (var ticket in userTickets)
            {
                totalPrice += (ticket.Quantity * ticket.Ticket.Price);
            }

            ShoppingCartDto result = new ShoppingCartDto
            {
                TicketsInShoppingCart = userTickets,
                TotalPrice = totalPrice
            };
            return result;
        }

        public bool MakeOrder(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var loggedInUser = _userRepository.Get(userId);
            var userShoppingCart = loggedInUser.UserCart;

            if (userShoppingCart.TicketsInShoppingCart.Count == 0)
            {
                return false;
            }

            Order userOrder = new Order
            {
                Id = Guid.NewGuid(),
                User = loggedInUser,
                UserId = userId
            };

            _orderRepository.Insert(userOrder);

            List<TicketInOrder> ticketsInOrder = userShoppingCart.TicketsInShoppingCart
                                                                 .Select(t => new TicketInOrder
                                                                 {
                                                                     Id = Guid.NewGuid(),
                                                                     OrderId = userOrder.Id,
                                                                     Order = userOrder,
                                                                     TicketId = t.Ticket.Id,
                                                                     Ticket = t.Ticket,
                                                                     Quantity = t.Quantity
                                                                 }).ToList();
            
            foreach (var ticket in ticketsInOrder)
            {
                _ticketInOrderRepository.Insert(ticket);
            }

            loggedInUser.UserCart.TicketsInShoppingCart.Clear();
            _userRepository.Update(loggedInUser);

            return true;
        }

        public bool RemoveTicketFromShoppingCart(string userId, Guid id)
        {
            if (string.IsNullOrEmpty(userId) || id == null)
            {
                return false;
            }
            
            var loggedInUser = _userRepository.Get(userId);
            var userShoppingCart = loggedInUser.UserCart;
            var ticketToRemove = userShoppingCart.TicketsInShoppingCart.Where(t => t.TicketId == id).FirstOrDefault();

            userShoppingCart.TicketsInShoppingCart.Remove(ticketToRemove);
            _shoppingCartRepository.Update(userShoppingCart);
            
            return true;
        }
    }
}
