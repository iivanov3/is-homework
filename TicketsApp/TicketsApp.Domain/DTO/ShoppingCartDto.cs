using System.Collections.Generic;
using TicketsApp.Domain.DomainModels;

namespace TicketsApp.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
        public double TotalPrice { get; set; }
    }
}
