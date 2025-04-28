using AutoMapper;
using Cinema_API.DTOs;
using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cinema_API.Services
{
    public  class SessionService
    {
        private readonly AppDbContext _context;

        public SessionService(AppDbContext context)
        {
            _context = context;
        }

        public void FilmRemoved(int filmId)
        {
            var ses = _context.Sessions
                .Include(s => s.Film)
                .Include(s => s.Tickets)
                .Include(s => s.Hall)
                .Include(s => s.Status)
                .Where(s => s.FilmId == filmId && s.StatusId == 3)
                .ToList();

            var tickets = _context.Tickets
                .Include(t => t.Session)
                .Where(t => ses.Any(s => s.Id == t.SessionId))
                .ToList();

            ses.ForEach(s =>
            {
                s.StatusId = 2;
            });

            tickets.ForEach(t =>
            {
                t.StatusId = null;
            });

            _context.SaveChanges();
        }

        public void HallRemoved(int hallId)
        {
            var ses = _context.Sessions
                .Include(s => s.Film)
                .Include(s => s.Tickets)
                .Include(s => s.Hall)
                .Include(s => s.Status)
                .Where(s => s.HallId == hallId && s.StatusId == 3)
                .ToList();

            var tickets = _context.Tickets
                .Include(t => t.Session)
                .Where(t => ses.Any(s => s.Id == t.SessionId))
                .ToList();

            ses.ForEach(s =>
            {
                s.StatusId = 2;
            });

            tickets.ForEach(t =>
            {
                t.StatusId = null;
            });

            _context.SaveChanges();
        }

        public void HallUpdated(int hallId, int difference)
        {
            var ses = _context.Sessions.Where(s => s.HallId == hallId && s.StatusId == 3).Include(s => s.Tickets).ToList();
            if(difference > 0)
            {
                ses.ForEach(s =>
                {
                    s.Tickets.Add(new Ticket
                    {
                        SessionId = s.Id,
                        SeatNumber = s.Tickets.Count + 1,
                    });
                });
            } else
            {
                ses.ForEach(s =>
                {
                    var tickets = s.Tickets.Where(t => t.SeatNumber > s.Tickets.Count + difference).ToList();
                    tickets.ForEach(t =>
                    {
                        s.Tickets.Remove(t);
                    });
                });
            }

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
    }
}
