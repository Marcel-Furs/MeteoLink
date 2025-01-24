using System.ComponentModel.DataAnnotations;

namespace MeteoLink.Data.Models
{
    public class MeasurementModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? SensorId { get; set; }

        [Required]
        public float? Value { get; set; }

        [Required]
        public DateTime? TimeStamp { get; set; }

        public SensorModel? Sensor { get; set; }
    }
}
