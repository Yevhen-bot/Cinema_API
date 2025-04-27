using System.ComponentModel.DataAnnotations;
using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class GetUpdateTicketModel
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SeatNumber { get; set; }
        [Range(0, 350)]
        public int? Price { get; set; }
        public int? StatusId { get; set; }
    }
}
