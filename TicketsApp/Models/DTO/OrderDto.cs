using System;
using System.Collections.Generic;
using TicketsApp.Models.Domain;

namespace TicketsApp.Web.Models.DTO
{
    public class OrderDto
    {
        public List<Order> Orders { get; set; }
    }
}
