using MeteoLink.Data.Models;
using MeteoLink.Dto;

namespace MeteoLink.Repositories
{
    public interface IUserRepository
    {
        UserModel Get(int id);
        List<UserModel> GetAll();

        Task<UserModel> FindByNameAsync(string name);
        Task CreateAsync(UserModel user);

        bool Create(UserModel user);
        bool Update(UserModel user);
        bool Delete(int id);
    }
}
