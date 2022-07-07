﻿using System;
using TicketsApp.Models.Domain;

namespace TicketsApp.Web.Models.Domain
{
    public class TicketInOrder
    {
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
