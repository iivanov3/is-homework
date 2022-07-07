using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketsApp.Web.Models.Domain;

namespace TicketsApp.Models.Domain
{
    public class Ticket
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Rating { get; set; }
        
        public virtual ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
        public virtual ICollection<TicketInOrder> Orders { get; set; }
    }
}
