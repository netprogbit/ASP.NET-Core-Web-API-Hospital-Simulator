namespace Server.DTOs
{
    public class AuthDto
    {
        public int PatientId { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
