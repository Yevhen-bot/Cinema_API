using System.ComponentModel.DataAnnotations;

namespace Cinema_API.DTOs
{
    public class ChangeEmailModel
    {
        [EmailAddress]
        public string NewEmail { get; set; }
        [EmailAddress]
        public string OldEmail { get; set; }
    }
}
