using System;
using TicketsApp.Domain.DomainModels;

namespace TicketsApp.Domain.DTO
{
    public class AddToShoppingCartDto
    {
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int Quantity { get; set; }
    }
}
