using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsApp.Domain.DomainModels;
using TicketsApp.Domain.DTO;
using TicketsApp.Repository;
using TicketsApp.Services.Interface;

namespace TicketsApp.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            var allTickets = _ticketService.GetAllTickets();
            TicketDto tickets = new TicketDto { Tickets = allTickets };

            return View(tickets);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(TicketDto model)
        {
            List<Ticket> tickets = _ticketService.GetFilteredTickets(model.FilterDate);

            TicketDto result = new TicketDto { Tickets = tickets, FilterDate = model.FilterDate };
            return View(result);
        }
        
        public IActionResult AddTicketToShoppingCart(Guid? id)
        {
            var model = _ticketService.GetShoppingCartInfo(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTicketToShoppingCart([Bind("TicketId", "Quantity")] AddToShoppingCartDto item)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _ticketService.AddToShoppingCart(item, userId);

            return RedirectToAction("Index", "Tickets");
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTicket(id);
            
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Date,Duration,Genre,Price,Rating")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _ticketService.CreateNewTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Title,Date,Duration,Genre,Price,Rating")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ticketService.UpdateExistingTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTicket(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return _ticketService.GetDetailsForTicket(id) != null;
        }
    }
}
