namespace Cinema_API.DTOs
{
    public class GetDiscountModel
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int DiscountPercent { get; set; }
    }
}
