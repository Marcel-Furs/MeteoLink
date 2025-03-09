namespace MeteoLink.Dto
{
    public class DailyAverageDto
    {
        public DateTime? Date { get; set; }
        public int? SensorId { get; set; }
        public float? AverageValue { get; set; }
        public string SensorName { get; set; }
    }
}
