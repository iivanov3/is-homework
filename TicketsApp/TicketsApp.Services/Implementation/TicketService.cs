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
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;

        public TicketService(IRepository<Ticket> ticketRepository, IUserRepository userRepository, 
                             IRepository<TicketInShoppingCart> ticketInShoppingCartRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = _userRepository.Get(userID);
            var userShoppingCart = user.UserCart;

            if (item.TicketId != null && userShoppingCart != null)
            {
                var ticket = GetDetailsForTicket(item.TicketId);

                if (ticket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        ShoppingCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                        Quantity = item.Quantity
                    };

                    _ticketInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
            }
            return false;
        }

        public void CreateNewTicket(Ticket t)
        {
            _ticketRepository.Insert(t);
        }

        public void DeleteTicket(Guid id)
        {
            var ticket = GetDetailsForTicket(id);
            _ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            return _ticketRepository.GetAll();
        }

        public List<Ticket> GetFilteredTickets(DateTime date)
        {
            return _ticketRepository.GetAll().Where(t => t.Date.Date == date.Date).ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return _ticketRepository.Get(id);
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var ticket = GetDetailsForTicket(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                Ticket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdateExistingTicket(Ticket t)
        {
            _ticketRepository.Update(t);
        }
    }
}
