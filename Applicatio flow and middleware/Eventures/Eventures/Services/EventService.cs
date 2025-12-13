using Eventures.Data;
using Eventures.Models;

namespace Eventures.Services
{
    public class EventService
    {
        private readonly EventureContext _context;
        public EventService(EventureContext context)
        {
            _context = context;
        }
        public async Task<List<Event>> GetAllEvents() 
        {
            List<Event> events = _context.Events.ToList();
            return events;
        }
        public async Task<Event> CreateEvent(string name,string place,DateTime start,DateTime end,int totalTickets,double pricePerTicket) 
        {
            Event newEvent = new Event()
            {
                Name = name,
                Place = place,
                Start = start,
                End = end,
                PricePerTicket = pricePerTicket,
                TotalTickets = totalTickets,
            };
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }
    }
}
