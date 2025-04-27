using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class CreateDiscountModel
    {
        public int FilmId { get; set; }
        public int DiscountPercent { get; set; }
    }
}
