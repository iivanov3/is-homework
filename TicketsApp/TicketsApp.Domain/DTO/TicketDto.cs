using System;
using System.Collections.Generic;
using TicketsApp.Domain.DomainModels;

namespace TicketsApp.Domain.DTO
{
    public class TicketDto
    {
        public List<Ticket> Tickets { get; set; }
        public DateTime FilterDate { get; set; }
    }
}
