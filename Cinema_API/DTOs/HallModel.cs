using System.ComponentModel.DataAnnotations;

namespace Cinema_API.Models
{
    public class HallModel
    {
        [Range(1, 300)]
        public int Seats { get; set; }
        public bool IsVip { get; set; }

    }
}
