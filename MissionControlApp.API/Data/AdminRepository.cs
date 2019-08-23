using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MissionControlApp.API.Dtos;
using MissionControlApp.API.Helpers;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        public AdminRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<MissionAssessment> GetMissionAssessment(int missionAssessmentId)
        {
            return await _context.MissionAssessments
                .Include(u => u.User)
                .Include(m => m.Mission)
                .FirstOrDefaultAsync(i => i.Id == missionAssessmentId);
        }

         public async Task<PagedList<Mission>> GetMissionsInQueue(MissionParams missionParams)
        {
            var missions = _context.Missions
                .Include(u => u.User)
                .Include(b => b.BusinessFunction)
                .Include(i => i.Industry)
                .Include(ma => ma.MissionAccelerators)
                .ThenInclude(a => a.Accelerator)
                .Include(mp => mp.MissionPlatforms)
                .ThenInclude(p => p.Platform)
                .Include(mt => mt.MissionTeam)
                .ThenInclude(u => u.User)
                .ThenInclude(p => p.Photos)
                .AsQueryable();

            return await PagedList<Mission>.CreateAsync(missions, missionParams.PageNumber, missionParams.PageSize);
        }

        public async Task<Mission> GetMission(int missionId)
        {
            return await _context.Missions
                .Include(u => u.User)
                .Include(b => b.BusinessFunction)
                .Include(i => i.Industry)
                .Include(a => a.MissionAssessment)
                .Include(ma => ma.MissionAccelerators)
                .ThenInclude(a => a.Accelerator)
                .Include(mp => mp.MissionPlatforms)
                .ThenInclude(p => p.Platform)
                .Include(mt => mt.MissionTeam)
                .ThenInclude(mu => mu.User)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(i => i.Id == missionId);
        }
    }
}