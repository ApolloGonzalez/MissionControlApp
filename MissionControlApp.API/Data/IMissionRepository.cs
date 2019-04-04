using System.Collections.Generic;
using System.Threading.Tasks;
using MissionControlApp.API.Helpers;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Data
{
    public interface IMissionRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
    }
}