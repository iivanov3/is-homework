using System;
using System.Collections.Generic;
using TicketsApp.Models.Domain;

namespace TicketsApp.Web.Models.DTO
{
    public class TicketDto
    {
        public List<Ticket> Tickets { get; set; }
        public DateTime FilterDate { get; set; }
    }
}
