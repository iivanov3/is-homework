using System;
using System.Collections.Generic;
using TicketsApp.Domain.Identity;

namespace TicketsApp.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }

        public ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
