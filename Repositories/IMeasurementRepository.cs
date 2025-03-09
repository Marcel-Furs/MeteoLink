using MeteoLink.Data.Models;
using System.Linq.Expressions;

namespace MeteoLink.Repositories
{
    public interface IMeasurementRepository
    {
        Task<MeasurementModel> Get(int id);
        Task<List<MeasurementModel>> GetAll();
        Task<MeasurementModel> Get(Expression<Func<MeasurementModel, bool>> exp);
        Task<List<MeasurementModel>> GetAll(Expression<Func<MeasurementModel, bool>> exp);

        Task CreateSensor(SensorModel sensor);

        Task<SensorModel?> GetSensor(int? sensorId);
    }
}
