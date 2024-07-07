using System.ComponentModel.DataAnnotations;

namespace RamsTrackerAPI.Models.DTO
{
    public class HsPersonContactDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
