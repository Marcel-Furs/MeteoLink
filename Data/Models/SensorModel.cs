using System.ComponentModel.DataAnnotations;

namespace MeteoLink.Data.Models
{
    public class SensorModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int? DeviceId { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public string? Unit { get; set; }

        public DeviceModel? Device { get; set; }

        public ICollection<MeasurementModel> Measurements { get; set; } = new List<MeasurementModel>();
    }
}
