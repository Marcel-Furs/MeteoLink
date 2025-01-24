using MeteoLink.Data;
using MeteoLink.Data.Models;
using MeteoLink.Dto;
using Microsoft.EntityFrameworkCore;

namespace MeteoLink.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }


        public async Task<UserModel> FindByNameAsync(string name)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task CreateAsync(UserModel user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public bool Create(UserModel user)
        {
            context.Add(user);
            context.SaveChanges();
            return true;
        }

        public bool Update(UserModel user)
        {
            var entity = Get(user.Id);
            if (entity == null)
            {
                return false;
            }
            context.Update(user);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id) 
        {
            var entity = Get(id);
            if(entity == null) 
            {
                return false;
            }
            context.Users.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public UserModel Get(int id) 
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<UserModel> GetAll()
        {
            return context.Users.ToList();
        }
    }
}