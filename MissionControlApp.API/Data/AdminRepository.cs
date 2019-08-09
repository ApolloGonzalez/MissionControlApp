using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MissionControlApp.API.Dtos;
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
    }
}