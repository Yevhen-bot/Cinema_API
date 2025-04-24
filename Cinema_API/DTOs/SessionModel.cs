using DataAccess.Entity;

namespace Cinema_API.Models
{
    public class SessionModel
    {
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public DateTime StartTime { get; set; }
        public int Price { get; set; }
        public int StatusId { get; set; }
    }
}
