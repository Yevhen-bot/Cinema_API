using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class GetUpdateSaleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime SaleDate { get; set; }
        public ICollection<GetUpdateTicketModel> Tickets { get; set; }
    }
}
