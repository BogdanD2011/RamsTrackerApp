using System.ComponentModel.DataAnnotations;

namespace RamsTrackerAPI.Models.DTO.AuthDTO
{
    public class RegisterRequestDTO
    {
        [Required]
        public string? FirstName { get; set; }
        
        [Required]
        public string? LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        
        public string[]? Roles { get; set; }

        
        public string? Contractor { get; set; }
        

    }
}
