namespace Cinema_API.DTOs
{
    public class GetUpdateSaleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TicketsCount { get; set; }
        public int TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
