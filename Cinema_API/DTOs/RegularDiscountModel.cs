using DataAccess.Entity;

namespace Cinema_API.Models
{
    public class RegularDiscountModel
    {
        public int FilmId { get; set; }
        public int DiscountPercent { get; set; }
    }
}
