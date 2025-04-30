using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Ticket
    {
        public int Id { get; set; }
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public int SeatNumber { get; set; }
        public int? Price { get; set; }
        public StatusTicket? Status { get; set; }
        public int? StatusId { get; set; } 
        public int? SaleId { get; set; } 
        public Sale? Sale { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
    }
}
