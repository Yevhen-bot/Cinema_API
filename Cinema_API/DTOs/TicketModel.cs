using System.ComponentModel.DataAnnotations;
using DataAccess.Entity;

namespace Cinema_API.Models
{
    public class TicketModel
    {
        public int SessionId { get; set; }
        public int SeatNumber { get; set; }
        [Range(0, 350)]
        public int? Price { get; set; }
        public int? StatusId { get; set; }
    }
}
