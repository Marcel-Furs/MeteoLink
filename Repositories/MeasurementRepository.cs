using MeteoLink.Data.Models;
using MeteoLink.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeteoLink.Repositories
{
    public class MeasurementRepository : BaseCrudRepository<MeasurementModel, int>, IMeasurementRepository
    {
        public MeasurementRepository(DataContext context) : base(context) { }

        public override async Task<MeasurementModel> Get(int id)
        {
            return await context.Measurements
                .Include(m => m.Sensor)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<List<MeasurementModel>> GetAll()
        {
            return await context.Measurements
                .Include(m => m.Sensor)
                .ToListAsync();
        }

        public override async Task<MeasurementModel> Get(Expression<Func<MeasurementModel, bool>> exp)
        {
            return await context.Measurements
                .Include(m => m.Sensor)
                .FirstOrDefaultAsync(exp);
        }

        public override async Task<List<MeasurementModel>> GetAll(Expression<Func<MeasurementModel, bool>> exp)
        {
            return await context.Measurements
                .Include(m => m.Sensor)
                .Where(exp)
                .ToListAsync();
        }

        public async Task CreateSensor(SensorModel sensor)
        {
            if (sensor == null)
                throw new ArgumentNullException(nameof(sensor));

            context.Sensors.Add(sensor);
            await context.SaveChangesAsync();
        }

        // Metoda do pobierania czujnika
        public async Task<SensorModel?> GetSensor(int? sensorId)
        {
            return await context.Sensors
                .FirstOrDefaultAsync(s => s.Id == sensorId);
        }
    }
}