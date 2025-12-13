using Eventures.Filters;
using Eventures.Models;
using Eventures.Services;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eventures.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventService eventService;
        public EventsController(EventService eventService) 
        {
            this.eventService = eventService;
        }
        [Authorize]
        public async Task<IActionResult> All()
        {
            List<Event> events = await eventService.GetAllEvents();
            return View(events);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create() 
        {
            return View(new CreateEventViewModel());
        }
        [ServiceFilter(typeof(LogActionCreateFilter))]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> onCreate([Bind("Name,Place,Start,End,TotalTickets,PricePerTicket")] CreateEventViewModel newEvent) 
        {
            if (ModelState.IsValid) 
            {
                await eventService.CreateEvent(newEvent.Name, newEvent.Place, newEvent.Start, newEvent.End, newEvent.TotalTickets, newEvent.PricePerTicket);
                HttpContext.Items["Event"] = newEvent;
                return Redirect("/Events/All");
            }
            return View("Create",newEvent);
        }
    }
}
