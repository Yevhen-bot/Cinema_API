namespace Cinema_API.DTOs
{
    public class UpdateFilmModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; } //seconds
        public DateTime ReleaseDate { get; set; }
        public int? Restriction { get; set; }
        public string? Description { get; set; }
    }
}
