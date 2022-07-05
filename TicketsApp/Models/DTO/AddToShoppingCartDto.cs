using System;
using TicketsApp.Models.Domain;

namespace TicketsApp.Web.Models.DTO
{
    public class AddToShoppingCartDto
    {
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int Quantity { get; set; }
    }
}
