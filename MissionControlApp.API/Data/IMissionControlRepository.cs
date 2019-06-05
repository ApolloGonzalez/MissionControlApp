using System.Collections.Generic;
using System.Threading.Tasks;
using MissionControlApp.API.Helpers;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Data
{
    public interface IMissionControlRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id, bool isCurrentUser);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Like> GetLike(int userId, int recipientId);
        Task<Message> GetMessage(int id);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
        Task<Industry> GetIndustry(int industryId);
        Task<IEnumerable<Industry>> GetIndustries();
        Task<BusinessFunction> GetBusinessFunction(int businessFunctionId);
        Task<IEnumerable<BusinessFunction>> GetBusinessFunctions();
        Task<Mission> GetMission(int userId, int missionId);
        Task<PagedList<Mission>> GetMissions(MissionParams missionParams);
    }
}