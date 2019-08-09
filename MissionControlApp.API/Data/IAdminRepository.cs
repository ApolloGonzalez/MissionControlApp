using System.Threading.Tasks;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Data
{
    public interface IAdminRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<MissionAssessment> GetMissionAssessment(int missionAssessmentId);
    }
}