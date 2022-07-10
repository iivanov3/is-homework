using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketsApp.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
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
        
        public ICollection<TicketInShoppingCart> TicketsInShoppingCart { get; set; }
        public ICollection<TicketInOrder> TicketsInOrder { get; set; }
    }
}
