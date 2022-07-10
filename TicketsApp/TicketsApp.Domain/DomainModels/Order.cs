using System;
using System.Collections.Generic;
using TicketsApp.Domain.Identity;

namespace TicketsApp.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime OrderedOn { get; set; }

        public ICollection<TicketInOrder> TicketsInOrder { get; set; }
    }
}
