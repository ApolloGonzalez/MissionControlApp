using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MissionControlApp.API.Helpers;
using MissionControlApp.API.Models;
using Microsoft.EntityFrameworkCore;
using MissionControlApp.API.Dtos;

namespace MissionControlApp.API.Data
{
    public class MissionControlRepository : IMissionControlRepository
    {
        private readonly DataContext _context;
        public MissionControlRepository(DataContext context)
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

        public async Task<Industry> GetIndustry(int industryId)
        {
            return await _context.Industries.FirstOrDefaultAsync(i => i.Id == industryId);
        }

        public async Task<IEnumerable<Industry>> GetIndustries()
        {
            return await _context.Industries
                .Where(i => i.Active == true).ToListAsync();
        }

        public async Task<IEnumerable<Platform>> GetPlatforms()
        {
            return await _context.Platforms
                .Where(i => i.Active == true).ToListAsync();
        }

        public async Task<BusinessFunction> GetBusinessFunction(int businessFunctionId)
        {
            return await _context.BusinessFunctions.FirstOrDefaultAsync(b => b.Id == businessFunctionId);
        }

        public async Task<IEnumerable<BusinessFunction>> GetBusinessFunctions()
        {
            return await _context.BusinessFunctions
                .Where(b => b.Active == true).ToListAsync();
        }

        public async Task<IEnumerable<Accelerator>> GetAcceleratorsByBusinessFunctionAndIndustry(int businessFunctionId, int industryId)
        {
            return await _context.Accelerators
                .Where(b => b.BusinessFunctionId == businessFunctionId || b.IndustryId == industryId && b.Active == true)
                .ToListAsync();   
        }

        public async Task<IEnumerable<MissionTeam>> GetMissionTeam(int missionId)
        {
            var missionTeams =  await _context.MissionTeams
                .Include(u => u.User)
                .ThenInclude(p => p.Photos)
                .Where(mt => mt.MissionId == missionId)
                .ToListAsync();

            return missionTeams;
        }

        
        public async Task<IEnumerable<User>> GetMissionEmployees()
        {
            var users =  await _context.Users
                .Include(p => p.Photos)
                .Where(u => u.Employee == true)
                .ToListAsync();

            return users;
        }

        public async Task<Mission> GetMission(int userId, int missionId)
        {
            return await _context.Missions
                .Include(u => u.User)
                .Include(b => b.BusinessFunction)
                .Include(i => i.Industry)
                .Include(ma => ma.MissionAccelerators)
                .ThenInclude(a => a.Accelerator)
                .Include(mp => mp.MissionPlatforms)
                .ThenInclude(p => p.Platform)
                .Include(mt => mt.MissionTeam)
                .ThenInclude(mu => mu.User)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(i => i.Id == missionId && i.UserId == userId);
        }

        public async Task<PagedList<Mission>> GetMissions(MissionParams missionParams)
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
                .Where(mu => mu.UserId == missionParams.UserId)
                .AsQueryable();

            return await PagedList<Mission>.CreateAsync(missions, missionParams.PageNumber, missionParams.PageSize);
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

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u =>
                u.LikerId == userId && u.LikeeId == recipientId);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id, bool isCurrentUser)
        {
            var query = _context.Users.Include(p => p.Photos).AsQueryable();

            if (isCurrentUser)
                query = query.IgnoreQueryFilters();

            var user = await query.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(p => p.Photos)
                .OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Gender == userParams.Gender);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikers.Contains(u.Id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikees.Contains(u.Id));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await _context.Users
                .Include(x => x.Likers)
                .Include(x => x.Likees)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (likers)
            {
                return user.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);
            }
            else
            {
                return user.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .AsQueryable();

            switch (messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId 
                        && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId 
                        && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId 
                        && u.RecipientDeleted == false && u.IsRead == false);
                    break;
            }

            messages = messages.OrderByDescending(d => d.MessageSent);

            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => m.RecipientId == userId && m.RecipientDeleted == false 
                    && m.SenderId == recipientId 
                    || m.RecipientId == recipientId && m.SenderId == userId 
                    && m.SenderDeleted == false)
                .OrderByDescending(m => m.MessageSent)
                .ToListAsync();

            return messages;
        }
    }
}