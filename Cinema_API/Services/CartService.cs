using DataAccess.Data;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cinema_API.Services
{
    public class CartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public int UserCreated(int userId)
        {
            var cart = new Cart
            {
                UserId = userId,
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart.Id;
        }

        public void TicketBooked(int ticketId, int userId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            ticket.StatusId = 2;
            ticket.CartId = _context.Carts.FirstOrDefault(c => c.UserId == userId).Id;
            _context.SaveChanges();
        }

        public void TicketUnbooked(int ticketId, int userId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }
            ticket.StatusId = null;
            ticket.CartId = null;
            _context.SaveChanges();
        }

        public void CartBought(int userId)
        {
            var user = _context.Users.Include(u => u.Cart).ThenInclude(c => c.Tickets).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var sale = new Sale
            {
                UserId = userId,
                SaleDate = DateTime.Now,
            };
            _context.Sales.Add(sale);
            _context.SaveChanges();

            foreach (var ticket in user.Cart.Tickets)
            {
                ticket.StatusId = 1;
                ticket.SaleId = sale.Id;
                _context.Tickets.Update(ticket);
            }

            _context.SaveChanges();
            Clear(user.Cart.Id);
        }

        public void AbortUserSession(int userId)
        {
            var user = _context.Users.Include(u => u.Cart).ThenInclude(c => c.Tickets).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            foreach (var ticket in user.Cart.Tickets)
            {
                ticket.StatusId = null;
                ticket.CartId = null;
                _context.Tickets.Update(ticket);
            }

            _context.SaveChanges();
            Clear(user.CartId);
        }

        private void Clear(int cartId)
        {
            _context.Carts.Find(cartId)!.Tickets.Clear();
            _context.SaveChanges();
        }
    }
}
