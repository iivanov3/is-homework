using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Domain.DomainModels;

namespace TicketsApp.Domain.DTO
{
    public class OrderDto
    {
        public Order Order { get; set; }
        public double TotalPrice { get; set; }
    }
}
