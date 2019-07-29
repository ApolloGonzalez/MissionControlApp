using System.Threading.Tasks;
using MissionControlApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MissionControlApp.API.Dtos;
using Microsoft.AspNetCore.Identity;
using MissionControlApp.API.Models;
using Microsoft.Extensions.Options;
using MissionControlApp.API.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace MissionControlApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMissionControlRepository _missionControlRepo;
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public AdminController(
            IMissionControlRepository missionControlRepo,
            DataContext context,
            UserManager<User> userManager,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _userManager = userManager;
            _cloudinaryConfig = cloudinaryConfig;
            _context = context;
            _missionControlRepo = missionControlRepo;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from user in _context.Users
                                  orderby user.UserName
                                  select new
                                  {
                                      Id = user.Id,
                                      UserName = user.UserName,
                                      JobTitle = user.JobTitle,
                                      Employee = user.Employee,
                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles
                                               on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                                  }).ToListAsync();
            return Ok(userList);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("missionTeam/{missionId}")]
        public async Task<IActionResult> GetMissionTeam(int missionId)
        {
            var missionTeam = await _missionControlRepo.GetMissionTeam(missionId);

            var missionTeamToReturn = (from teamMember in missionTeam
                select new MissionTeamToReturnDto 
                {
                    Id = teamMember.Id,
                    UserId = teamMember.User.Id,
                    MissionId = teamMember.MissionId,
                    Username = teamMember.User.UserName,
                    Employee = teamMember.User.Employee,
                    JobTitle = teamMember.User.JobTitle,
                    Gender = teamMember.User.Gender,
                    Age = teamMember.User.DateOfBirth.CalculateAge(),
                    KnownAs = teamMember.User.KnownAs,
                    Created = teamMember.User.Created,
                    LastActive = teamMember.User.LastActive,
                    City = teamMember.User.City,
                    Country = teamMember.User.Country,
                    PhotoUrl = teamMember.User.Photos.FirstOrDefault(p => p.IsMain).Url
                }).ToList();
        
            return Ok(missionTeamToReturn);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("missionEmployees")]
        public async Task<IActionResult> GetMissionEmployees(int missionId)
        {
            var missionEmployees = await _missionControlRepo.GetMissionEmployees();

            var missionEmployeesToReturn = (from employee in missionEmployees
                select new UserForDetailedDto 
                {
                    Id = employee.Id,
                    Username = employee.UserName,
                    Employee = employee.Employee,
                    JobTitle = employee.JobTitle,
                    Gender = employee.Gender,
                    Age = employee.DateOfBirth.CalculateAge(),
                    KnownAs = employee.KnownAs,
                    Created = employee.Created,
                    LastActive = employee.LastActive,
                    City = employee.City,
                    Country = employee.Country,
                    PhotoUrl = employee.Photos.FirstOrDefault(p => p.IsMain).Url
                }).ToList();
        
            return Ok(missionEmployeesToReturn);
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { };
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photosForModeration")]
        public async Task<IActionResult> GetPhotosForModeration()
        {
            var photos = await _context.Photos
                .Include(u => u.User)
                .IgnoreQueryFilters()
                .Where(p => p.IsApproved == false)
                .Select(u => new
                {
                    Id = u.Id,
                    UserName = u.User.UserName,
                    Url = u.Url,
                    IsApproved = u.IsApproved
                }).ToListAsync();

            return Ok(photos);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("approvePhoto/{photoId}")]
        public async Task<IActionResult> ApprovePhoto(int photoId)
        {
            var photo = await _context.Photos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == photoId);

            photo.IsApproved = true;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("rejectPhoto/{photoId}")]
        public async Task<IActionResult> RejectPhoto(int photoId)
        {
            var photo = await _context.Photos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == photoId);

            if (photo.IsMain)
                return BadRequest("You cannot reject the main photo");

            if (photo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _context.Photos.Remove(photo);
                }
            }

            if (photo.PublicId == null)
            {
                _context.Photos.Remove(photo);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}