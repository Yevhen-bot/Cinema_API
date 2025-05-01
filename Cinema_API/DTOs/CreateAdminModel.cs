using System.ComponentModel.DataAnnotations;

namespace Cinema_API.DTOs
{
    public class CreateAdminModel
    {
        [MinLength(3)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
