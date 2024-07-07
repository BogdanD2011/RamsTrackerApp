namespace RamsTrackerAPI.Models.DTO.AuthDTO
{
    public class LoginResponseDTO
    {

        public string Username { get; set; }
        public string[] Roles { get; set; }
        public string JwtToken { get; set; }
    }
}
