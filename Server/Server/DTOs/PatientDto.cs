using System;

namespace Server.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
