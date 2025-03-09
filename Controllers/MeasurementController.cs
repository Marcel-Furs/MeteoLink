using MeteoLink.Attributes;
using MeteoLink.Data.Models;
using MeteoLink.Dto;
using MeteoLink.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeteoLink.Controllers
{
    [MeteoLinkV1Route]
    [ApiController]
    [Authorize]
    public class MeasurementController : ControllerBase
    {
        private readonly MeasurementRepository _repository;

        public MeasurementController(MeasurementRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("add-measurement")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateMeasurement([FromBody] MeasurementDto measurementDto)
        {
            if (measurementDto.SensorId == null || measurementDto.Value == null || measurementDto.Timestamp == null)
            {
                return BadRequest("SensorId, Value, and TimeStamp are required.");
            }

            // Check if sensor exists
            var sensor = await _repository.GetSensor(measurementDto.SensorId);
            if (sensor == null)
            {
                return NotFound($"Sensor with Id {measurementDto.SensorId} not found.");
            }

            var measurement = new MeasurementModel
            {
                SensorId = measurementDto.SensorId,
                Value = measurementDto.Value,
                TimeStamp = DateTime.Now
            };

            // Add measurement
            await _repository.Create(measurement);
            return Ok(measurement);
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMeasurements()
        {
            var measurements = await _repository.GetAll();
            var measurementsDto = new List<MeasurementDto>();

            if (measurements != null)
            {
                foreach (var v in measurements)
                {
                    var measurementDto = new MeasurementDto
                    {
                        Id = v.Id,
                        Value = v.Value,
                        Timestamp = v.TimeStamp,
                        SensorId = (int)v.SensorId,
                        SensorDto = new SensorDto 
                        {
                            Id = v.Sensor.Id,
                            Name = v.Sensor.Name,
                            Type = v.Sensor.Type,
                            Unit = v.Sensor.Unit
                        }
                    };
                    measurementsDto.Add(measurementDto);
                }
            }
            else
            {
                return BadRequest("There is no data in measurements");
            }

            return Ok(measurementsDto);
        }

        [HttpGet("getDailyAverageMeasurements")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDailyAverageMeasurements()
        {
            var measurements = await _repository.GetAll();

            if (measurements == null || !measurements.Any())
            {
                return BadRequest("There is no data in measurements.");
            }

            var dailyAverages = measurements
                .Where(m => m.TimeStamp.HasValue) 
                .GroupBy(m => new { Date = m.TimeStamp.Value.Date, m.SensorId })
                .Select(group => new
                {
                    Date = group.Key.Date, 
                    SensorId = group.Key.SensorId, 
                    AverageValue = group.Average(m => m.Value) 
                })
                .ToList();

            var result = new List<DailyAverageDto>();

            foreach (var average in dailyAverages)
            {
                var sensor = await _repository.GetSensor(average.SensorId);
                result.Add(new DailyAverageDto
                {
                    Date = average.Date,
                    SensorId = average.SensorId,
                    SensorName = sensor?.Name ?? "Unknown",
                    AverageValue = average.AverageValue
                });
            }

            return Ok(result);
        }


        //[HttpGet("sensor/{sensorId}")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetMeasurementsBySensorId(int sensorId)
        //{
        //    var measurements = await _repository.GetAll(m => m.SensorId == sensorId);
        //    if (!measurements.Any())
        //    {
        //        return NotFound($"No measurements found for SensorId {sensorId}.");
        //    }

        //    return Ok(measurements);
        //}

        //[HttpPost("register-sensor")]
        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterSensor([FromBody] SensorModel sensor)
        //{
        //    if (sensor.Id == 0 || string.IsNullOrEmpty(sensor.Name) || string.IsNullOrEmpty(sensor.Type))
        //    {
        //        return BadRequest("Sensor ID, Name, and Type are required.");
        //    }

        //    var existingSensor = await _repository.Get(m => m.Sensor.Id == sensor.Id);
        //    if (existingSensor != null)
        //    {
        //        return Conflict($"Sensor with Id {sensor.Id} already exists.");
        //    }

        //    await _repository.CreateSensor(sensor);
        //    return CreatedAtAction(nameof(GetMeasurementsBySensorId), new { sensorId = sensor.Id }, sensor);
        //}
    }
}