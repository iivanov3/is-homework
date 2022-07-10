using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Domain.DomainModels;
using TicketsApp.Domain.DTO;

namespace TicketsApp.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        List<Ticket> GetFilteredTickets(DateTime date);
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket t);
        void UpdateExistingTicket(Ticket t);
        AddToShoppingCartDto GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}
