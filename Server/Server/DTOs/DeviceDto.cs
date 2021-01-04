namespace Server.DTOs
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
    }
}
