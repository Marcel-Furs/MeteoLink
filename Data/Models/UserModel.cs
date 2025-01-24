using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace MeteoLink.Data.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        public ICollection<DeviceModel> Devices { get; set; } = new List<DeviceModel>();

        public ICollection<NotificationModel> Notifications { get; set; } = new List<NotificationModel>();
    }
}
