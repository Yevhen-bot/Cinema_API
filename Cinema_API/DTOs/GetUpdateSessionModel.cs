using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class GetUpdateSessionModel
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public DateTime StartTime { get; set; }
        public int Price { get; set; }
        public int StatusId { get; set; }
    }
}
