using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class Measurement
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int HR { get; set; }
        public int RR { get; set; }
    }
}
