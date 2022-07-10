using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketsApp.Domain.DomainModels;
using TicketsApp.Repository.Interface;

namespace TicketsApp.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return entities.Include(u => u.User)
                           .Include(t => t.TicketsInOrder)
                           .Include("TicketsInOrder.Ticket")
                           .ToListAsync().Result;
        }

        public List<Order> GetUserOrders(string userId)
        {
            return entities.Include(u => u.User)
                           .Include(t => t.TicketsInOrder)
                           .Include("TicketsInOrder.Ticket")
                           .Where(u => u.UserId.Equals(userId))
                           .ToListAsync().Result;
        }

        public Order GetOrderDetails(Guid orderId)
        {
            return entities.Where(o => o.Id == orderId)
                           .Include(o => o.TicketsInOrder)
                           .Include("TicketsInOrder.Ticket")
                           .FirstOrDefaultAsync().Result;
        }
    }
}
