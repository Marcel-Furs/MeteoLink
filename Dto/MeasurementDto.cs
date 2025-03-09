using MeteoLink.Data.Models;

namespace MeteoLink.Dto
{
    public class MeasurementDto
    {
        public int Id { get; set; }
        public float? Value { get; set; }
        public DateTime? Timestamp { get; set; }

        public int SensorId { get; set; }
        public SensorDto? SensorDto { get; set; }
    }
}
