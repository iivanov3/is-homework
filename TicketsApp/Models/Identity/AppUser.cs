using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TicketsApp.Models.Domain;

namespace TicketsApp.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public ShoppingCart UserCart { get; set; }
    }
}
