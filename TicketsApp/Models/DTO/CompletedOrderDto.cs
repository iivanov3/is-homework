using System;
using System.Collections.Generic;
using TicketsApp.Web.Models.Domain;

namespace TicketsApp.Web.Models.DTO
{
    public class CompletedOrderDto
    {
        public List<CompletedOrder> CompletedOrders { get; set; }
    }
}
