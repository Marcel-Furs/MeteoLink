namespace MeteoLink.Dto
{
    public class SensorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }

        public int? DeviceId { get; set; }
    }
}
