using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class GetStatusTicketModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
