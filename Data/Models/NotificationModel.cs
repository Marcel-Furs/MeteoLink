using System.ComponentModel.DataAnnotations;

namespace MeteoLink.Data.Models
{
    public class NotificationModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public string? Message { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        [Required]
        public bool? IsRead { get; set; }

        public UserModel? User { get; set; }
    }
}
