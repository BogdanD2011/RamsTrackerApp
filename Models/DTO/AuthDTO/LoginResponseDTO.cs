namespace RamsTrackerAPI.Models.DTO.AuthDTO
{
    public class LoginResponseDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string[] Roles { get; set; }
        public string Contractor { get; set; }
        public string JwtToken { get; set; }
    }
}
