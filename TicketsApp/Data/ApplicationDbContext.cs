using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Models.Domain;
using TicketsApp.Models.Identity;
using TicketsApp.Web.Models.DTO;

namespace TicketsApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            
            builder.Entity<ShoppingCart>()
                .Property(sc => sc.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketInShoppingCart>()
                .HasKey(t => new { t.TicketId, t.ShoppingCartId });

            builder.Entity<TicketInShoppingCart>()
                .HasOne(t => t.Ticket)
                .WithMany(sc => sc.TicketsInShoppingCart)
                .HasForeignKey(t => t.TicketId);

            builder.Entity<TicketInShoppingCart>()
                .HasOne(sc => sc.ShoppingCart)
                .WithMany(t => t.TicketsInShoppingCart)
                .HasForeignKey(sc => sc.ShoppingCartId);

            builder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Owner)
                .WithOne(u => u.UserCart)
                .HasForeignKey<ShoppingCart>(sc => sc.OwnerId);

            builder.Entity<Order>()
                .HasKey(o => new { o.ShoppingCartId, o.AppUserId });

            builder.Entity<AppUser>()
                .HasMany(o => o.Orders)
                .WithOne(u => u.AppUser);
        }

        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    }
}
