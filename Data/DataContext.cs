using MeteoLink.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MeteoLink.Data
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<SensorModel> Sensors { get; set; }
        public DbSet<MeasurementModel> Measurements { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacja User -> Devices
            modelBuilder.Entity<DeviceModel>()
                .HasOne(d => d.User)
                .WithMany(u => u.Devices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Realacja User -> Notifications
            modelBuilder.Entity<NotificationModel>()
                .HasOne(d => d.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja Device -> Sensors
            modelBuilder.Entity<SensorModel>()
                .HasOne(s => s.Device)
                .WithMany(d => d.Sensors)
                .HasForeignKey(s => s.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja Sensor -> Measurements
            modelBuilder.Entity<MeasurementModel>()
                .HasOne(m => m.Sensor)
                .WithMany(s => s.Measurements)
                .HasForeignKey(m => m.SensorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
