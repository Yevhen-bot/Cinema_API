using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class CreateSaleModel
    {
        public int UserId { get; set; }
        public int TicketsCount { get; set; }
        public int TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
