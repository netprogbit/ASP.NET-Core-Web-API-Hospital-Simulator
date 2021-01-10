namespace DataLayer.Entities
{
    // Only used for the query result 
    public class Summary
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DeviceId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string DeviceName { get; set; }
        public int CurrentHR { get; set; }
        public int CurrentRR { get; set; }
        public int AvgHR { get; set; }
        public int AvgRR { get; set; }
    }
}
