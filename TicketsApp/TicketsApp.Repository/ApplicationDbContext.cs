﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TicketsApp.Domain.DomainModels;
using TicketsApp.Domain.Identity;

namespace TicketsApp.Repository
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

            /*builder.Entity<BaseEntity>()
                   .HasKey(t => t.Id);*/

            builder.Entity<TicketInShoppingCart>()
                .HasOne(t => t.Ticket)
                .WithMany(sc => sc.TicketsInShoppingCart)
                .HasForeignKey(t => t.ShoppingCartId);

            builder.Entity<TicketInShoppingCart>()
                .HasOne(sc => sc.ShoppingCart)
                .WithMany(t => t.TicketsInShoppingCart)
                .HasForeignKey(sc => sc.TicketId);

            builder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Owner)
                .WithOne(u => u.UserCart)
                .HasForeignKey<ShoppingCart>(sc => sc.OwnerId);

            builder.Entity<Order>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Order>()
                .Property(o => o.OrderedOn)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<TicketInOrder>()
                .HasOne(t => t.Ticket)
                .WithMany(o => o.TicketsInOrder)
                .HasForeignKey(t => t.OrderId);

            builder.Entity<TicketInOrder>()
                .HasOne(o => o.Order)
                .WithMany(t => t.TicketsInOrder)
                .HasForeignKey(o => o.TicketId);
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<TicketInShoppingCart> TicketsInShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TicketInOrder> TicketsInOrders { get; set; }
    }
}
