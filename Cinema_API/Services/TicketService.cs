using AutoMapper;
using Cinema_API.DTOs;
using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cinema_API.Services
{
    public  class TicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        public void HallUpdated(int hallId, int newseats)
        {
            var ses = _context.Sessions.Where(s => s.HallId == hallId && s.StatusId == 3).Include(s => s.Tickets).ToList();
            ses.ForEach(s =>
            {
                var ticketstoremove = s.Tickets.Where(t => t.SeatNumber > newseats).ToList();
                if(ticketstoremove != null)
                    _context.RemoveRange(ticketstoremove);

                var ticks = s.Tickets.ToList();
                for(int i = ticks.Max(t => t.SeatNumber); i < newseats; i++)
                {
                    _context.Tickets.Add(new Ticket
                    {
                        SessionId = s.Id,
                        SeatNumber = i + 1,
                    });
                }
            });

            _context.SaveChanges();
        }

        public void SessionCreated(Session session)
        {
            if(session.StatusId == 3)
            {
                var hall = _context.Halls.FirstOrDefault(h => h.Id == session.HallId);
                for (int i = 1; i <= hall.Seats; i++)
                {
                    _context.Tickets.Add(new Ticket
                    {
                        SessionId = session.Id,
                        SeatNumber = i,
                    });
                }

            }

            _context.SaveChanges();
        }

        public void ReturnTicket(int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket == null)
            {
                throw new Exception("404");
            }

            if(ticket.Session.StatusId != 3)
            {
                throw new Exception("400");
            }

            ticket.StatusId = 3;
            _context.SaveChanges();
        }
    }
}
