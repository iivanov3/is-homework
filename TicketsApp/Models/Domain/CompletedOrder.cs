using System;

namespace TicketsApp.Web.Models.Domain
{
    public class CompletedOrder
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderedOn { get; set; }
        public DateTime PlayedOn { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
        public string Genre { get; set; }
    }
}
