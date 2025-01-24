using System.ComponentModel.DataAnnotations;

namespace MeteoLink.Data.Models
{
    public class DeviceModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        public UserModel? User { get; set; }

        public ICollection<SensorModel> Sensors { get; set; } = new List<SensorModel>();
    }
}
