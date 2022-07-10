using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Domain.DTO;

namespace TicketsApp.Services.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto GetShoppingCartInfo(string userId);
        bool RemoveTicketFromShoppingCart(string userId, Guid id);
        bool MakeOrder(string userId);
    }
}
