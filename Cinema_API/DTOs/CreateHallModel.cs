using System.ComponentModel.DataAnnotations;

namespace Cinema_API.DTOs
{
    public class CreateHallModel
    {
        [Range(1, 300)]
        public int Seats { get; set; }
        public bool IsVip { get; set; }

    }
}
