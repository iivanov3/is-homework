﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TicketsApp.Domain.DomainModels;

namespace TicketsApp.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }

        public ShoppingCart UserCart { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
