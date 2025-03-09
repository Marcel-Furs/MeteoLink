using MeteoLink.Attributes;
using MeteoLink.Data.Models;
using MeteoLink.Dto;
using MeteoLink.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeteoLink.Controllers
{
    [MeteoLinkV1Route]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceRepository _repository;
        private readonly MeasurementRepository _measurementRepo;

        public DeviceController(DeviceRepository repository, MeasurementRepository measurementRepo)
        {
            _repository = repository;
            _measurementRepo = measurementRepo;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterDeviceWithSensors([FromBody] DeviceDto deviceDto)
        {
            if (deviceDto == null || deviceDto.Id == 0 || string.IsNullOrEmpty(deviceDto.Name))
            {
                return BadRequest("Device ID and Name are required.");
            }

            var device = await _repository.Get(d => d.Id == deviceDto.Id);
            if (device == null)
            {
                device = new DeviceModel
                {
                    Id = deviceDto.Id,
                    Name = deviceDto.Name,
                    UserId = 1, 
                    Location = deviceDto.Location,
                    CreatedAt = DateTime.UtcNow
                };

                await _repository.Create(device);
            }

            foreach (var sensorDto in deviceDto.Sensors)
            {
                var existingSensor = await _measurementRepo.Get(m => m.Sensor.Id == sensorDto.Id);
                if (existingSensor == null)
                {
                    var sensor = new SensorModel
                    {
                        Id = sensorDto.Id,
                        Name = sensorDto.Name,
                        DeviceId = device.Id,
                        Type = sensorDto.Type,
                        Unit = sensorDto.Unit
                    };

                    await _measurementRepo.CreateSensor(sensor);
                }
            }

            return Ok("Device and sensors registered successfully.");
        }

        [HttpGet("ByUserId/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDeviceById(int id)
        {
            var device = await _repository.GetDeviceByUserId(id);
            if (device == null)
            {
                return NotFound($"Device with Id {id} not found.");
            }

            var deviceDto = new DeviceDto
            {
                Id = device.Id,
                Name = device.Name,
                Location = device.Location,
                CreatedAt = device.CreatedAt
            };

            var sensors = await _repository.GetSensorsByDeviceId(device.Id);

            deviceDto.Sensors = sensors.Select(sensor => new SensorDto
            {
                Id = sensor.Id,
                Name = sensor.Name,
                Type = sensor.Type,
                Unit = sensor.Unit,
                DeviceId = sensor.DeviceId
            }).ToList();

            return Ok(deviceDto);
        }
    }
}