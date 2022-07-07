using System;
using System.Collections.Generic;
using TicketsApp.Models.Identity;
using TicketsApp.Web.Models.Domain;

namespace TicketsApp.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime OrderedOn { get; set; }

        public ICollection<TicketInOrder> TicketsInOrder { get; set; }
    }
}
