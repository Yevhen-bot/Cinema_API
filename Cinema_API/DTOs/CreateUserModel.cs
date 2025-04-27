using System.ComponentModel.DataAnnotations;
using DataAccess.Entity;

namespace Cinema_API.DTOs
{
    public class CreateUserModel
    {
        [MinLength(3)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        [Range(0, 100)]
        public int Bonuses { get; set; }
    }
}
