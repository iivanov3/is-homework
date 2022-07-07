using System;
using System.Collections.Generic;
using TicketsApp.Models.Identity;

namespace TicketsApp.Models.Domain
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }

        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
    }
}
