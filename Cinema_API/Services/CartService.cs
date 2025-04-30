using DataAccess.Data;
using DataAccess.Entity;

namespace Cinema_API.Services
{
    public class CartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public void UserCreated(int userId)
        {
            var cart = new Cart
            {
                UserId = userId,
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void TicketBooked(int ticketId, int userId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            ticket.StatusId = 2;
            _context.Users.Find(userId).Cart.Tickets.Add(ticket);
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
            _context.Users.Find(userId).Cart.Tickets.Remove(ticket);
            _context.SaveChanges();
        }

        public void CartBought(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var sale = new Sale
            {
                UserId = userId,
                SaleDate = DateTime.Now,
            };

            foreach (var ticket in user.Cart.Tickets)
            {
                ticket.StatusId = 1;
                _context.Tickets.Update(ticket);
                sale.Tickets.Add(ticket);
            }

            _context.Sales.Add(sale);
            Clear(user.CartId);
            _context.SaveChanges();
        }

        public void AbortUserSession(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            foreach (var ticket in user.Cart.Tickets)
            {
                ticket.StatusId = null;
                _context.Tickets.Update(ticket);
            }

            Clear(user.CartId);
            _context.SaveChanges();
        }

        private void Clear(int cartId)
        {
            _context.Carts.Find(cartId).Tickets.Clear();
            _context.SaveChanges();
        }
    }
}
