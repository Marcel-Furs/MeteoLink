using MeteoLink.Data.Models;
using MeteoLink.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeteoLink.Repositories
{
    public class DeviceRepository : BaseCrudRepository<DeviceModel, int>, IDeviceRepository
    {
        public DeviceRepository(DataContext context) : base(context) { }

        public override async Task<DeviceModel> Get(int id)
        {
            return await context.Devices
                .Include(d => d.Sensors)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public override async Task<List<DeviceModel>> GetAll()
        {
            return await context.Devices
                .Include(d => d.Sensors)
                .ToListAsync();
        }

        public override async Task<DeviceModel> Get(Expression<Func<DeviceModel, bool>> exp)
        {
            return await context.Devices
                .Include(d => d.Sensors)
                .FirstOrDefaultAsync(exp);
        }

        public override async Task<List<DeviceModel>> GetAll(Expression<Func<DeviceModel, bool>> exp)
        {
            return await context.Devices
                .Include(d => d.Sensors)
                .Where(exp)
                .ToListAsync();
        }
        public async Task<DeviceModel> GetDeviceByUserId(int userId)
        {
            return await context.Devices.FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<List<SensorModel>> GetSensorsByDeviceId(int deviceId)
        {
            return await context.Sensors.Where(s => s.DeviceId == deviceId).ToListAsync();
        }


    }
}