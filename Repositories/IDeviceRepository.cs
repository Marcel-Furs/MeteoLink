using MeteoLink.Data.Models;
using System.Linq.Expressions;

namespace MeteoLink.Repositories
{
    public interface IDeviceRepository
    {
        Task<DeviceModel> Get(int id);
        Task<List<DeviceModel>> GetAll();
        Task<DeviceModel> Get(Expression<Func<DeviceModel, bool>> exp);
        Task<List<DeviceModel>> GetAll(Expression<Func<DeviceModel, bool>> exp);
    }
}
