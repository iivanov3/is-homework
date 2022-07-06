using System.Collections.Generic;
using TicketsApp.Models.Domain;

namespace TicketsApp.Web.Models.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
        public double TotalPrice { get; set; }
    }
}
