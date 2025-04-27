using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class CreateFilmModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; } //seconds
        public DateTime ReleaseDate { get; set; }
        public int? Restriction { get; set; }
        public string? Description { get; set; }
    }
}
