using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class Device
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public List<Measurement> Measurements { get; set; }
    }
}
