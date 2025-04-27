using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class UpdateRegularDiscountModel
    {
        public int Id { get; set; }
        public int DiscountPercent { get; set; }
    }
}
