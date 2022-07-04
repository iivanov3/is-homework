using System;
using System.Collections.Generic;
using TicketsApp.Models.Identity;

namespace TicketsApp.Models.Domain
{
    public class Order
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
