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
using System.Security.Claims;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace MissionControlApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMissionControlRepository _missionControlRepo;
        private readonly IAdminRepository _adminRepo;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public AdminController(
            IMissionControlRepository missionControlRepo,
            IAdminRepository adminRepo,
            IMapper mapper,
            DataContext context,
            UserManager<User> userManager,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _userManager = userManager;
            _cloudinaryConfig = cloudinaryConfig;
            _context = context;
            _missionControlRepo = missionControlRepo;
            _adminRepo = adminRepo;
            _mapper = mapper;

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

        [Authorize(Policy = "ManageMissionQueueRole")]
        [HttpGet("GetMissionsInQueue")]
        public async Task<IActionResult> GetMissionsInQueue([FromQuery]MissionParams missionParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            missionParams.UserId = currentUserId;
            
            var missions = await _adminRepo.GetMissionsInQueue(missionParams);

            var missionsToReturn = (from mission in missions
                select new MissionToReturnDto 
                {
                    UserId = mission.UserId,
                    UserName = mission.User.UserName,
                    KnownAs = mission.User.KnownAs,
                    Employee = mission.User.Employee,
                    JobTitle = mission.User.JobTitle,
                    MissionId = mission.Id,
                    MissionName = mission.MissionName,
                    IndustryId = mission.IndustryId,
                    IndustryAlias = mission.Industry.IndustryAlias,
                    BusinessFunctionId = mission.BusinessFunctionId,
                    BusinessFunctionAlias = mission.BusinessFunction.BusinessFunctionAlias,
                    Challenge = mission.Challenge,
                    DesiredOutcome = mission.DesiredOutcome,
                    BusinessImpact = mission.BusinessImpact,
                    EstimatedRoi = mission.EstimatedRoi,
                    ActualRoi = mission.ActualRoi,
                    ActualCost = mission.ActualCost,
                    TimeFrame = mission.TimeFrame,
                    Accelerators = 
                        (from accelerator in mission.MissionAccelerators.Select(a => a.Accelerator).ToList() 
                        select new MissionAcceleratorToReturnDto
                        { 
                            Id = accelerator.Id,
                            MissionId = mission.Id,
                            AcceleratorName = accelerator.AcceleratorName,
                            ModelType = accelerator.ModelType,
                            DateCreated = accelerator.DateCreated,
                            Description = accelerator.Description,
                            Active = accelerator.Active
                        }).ToList(),
                    Platforms = 
                        (from platform in mission.MissionPlatforms.Select(a => a.Platform).ToList() 
                        select new MissionPlatformToReturnDto
                        { 
                            Id = platform.Id,
                            MissionId = platform.Id,
                            PlatformName = platform.PlatformName,
                            PlatformAlias = platform.PlatformAlias,
                            Description = platform.Description,
                            Type = platform.Type,
                            DateCreated = platform.DateCreated,
                            Active = platform.Active
                        }).ToList(),
                    MissionTeam = 
                        (from missionTeam in mission.MissionTeam.ToList().Where(a => a.Active)
                        select new MissionTeamToReturnDto
                        {
                            Id = missionTeam.Id,
                            UserId = missionTeam.UserId,
                            MissionId = missionTeam.MissionId,
                            Username = missionTeam.User.UserName,
                            KnownAs = missionTeam.User.KnownAs,
                            Employee = missionTeam.User.Employee,
                            JobTitle = missionTeam.User.JobTitle,
                            Gender = missionTeam.User.Gender,
                            City = missionTeam.User.City,
                            Country = missionTeam.User.Country,
                            Created = missionTeam.DateCreated,
                            PhotoUrl = missionTeam.User.Photos.FirstOrDefault(p => p.IsMain).Url
                        }).ToList(),                    
                    DateCreated = mission.DateCreated,
                    Active = mission.Active,
                    Public = mission.Public
                }).OrderBy(cr => cr.TimeFrame);

            Response.AddPagination(missions.CurrentPage, missions.PageSize,
                missions.TotalCount, missions.TotalPages);

            return Ok(missionsToReturn);
        }

        [Authorize(Policy = "ManageMissionQueueRole")]
        [HttpGet("mission/{missionId}")]
        public async Task<IActionResult> GetMission(int missionId)
        {            
            var userId =  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var mission = await _adminRepo.GetMission(missionId);

            var missionToReturn = new MissionToReturnDto 
            {
                UserId = mission.UserId,
                UserName = mission.User.UserName,
                KnownAs = mission.User.KnownAs,
                Employee = mission.User.Employee,
                JobTitle = mission.User.JobTitle,
                MissionId = mission.Id,
                MissionName = mission.MissionName,
                IndustryId = mission.IndustryId,
                IndustryAlias = mission.Industry.IndustryAlias,
                BusinessFunctionId = mission.BusinessFunctionId,
                BusinessFunctionAlias = mission.BusinessFunction.BusinessFunctionAlias,
                Challenge = mission.Challenge,
                DesiredOutcome = mission.DesiredOutcome,
                BusinessImpact = mission.BusinessImpact,
                EstimatedRoi = mission.EstimatedRoi,
                ActualRoi = mission.ActualRoi,
                ActualCost = mission.ActualCost,
                MissionAssessment = _mapper.Map<MissionAssessmentToReturnDto>(mission.MissionAssessment),
                TimeFrame = mission.TimeFrame,
                Accelerators = 
                    (from accelerator in mission.MissionAccelerators.Select(a => a.Accelerator).ToList() 
                    select new MissionAcceleratorToReturnDto
                    { 
                        Id = accelerator.Id,
                        MissionId = mission.Id,
                        AcceleratorName = accelerator.AcceleratorName,
                        ModelType = accelerator.ModelType,
                        DateCreated = accelerator.DateCreated,
                        Description = accelerator.Description,
                        Active = accelerator.Active
                    }).ToList(),
                    Platforms = 
                        (from platform in mission.MissionPlatforms.Select(a => a.Platform).ToList() 
                        select new MissionPlatformToReturnDto
                        { 
                            Id = platform.Id,
                            MissionId = platform.Id,
                            PlatformName = platform.PlatformName,
                            PlatformAlias = platform.PlatformAlias,
                            Description = platform.Description,
                            Type = platform.Type,
                            DateCreated = platform.DateCreated,
                            Active = platform.Active
                        }).ToList(),
                    MissionTeam = 
                        (from missionTeam in mission.MissionTeam.Where(a => a.Active == true).ToList()
                        select new MissionTeamToReturnDto
                        {
                            Id = missionTeam.Id,
                            UserId = missionTeam.UserId,
                            MissionId = missionTeam.MissionId,
                            Username = missionTeam.User.UserName,
                            KnownAs = missionTeam.User.KnownAs,
                            Employee = missionTeam.User.Employee,
                            JobTitle = missionTeam.User.JobTitle,
                            Gender = missionTeam.User.Gender,
                            City = missionTeam.User.City,
                            Country = missionTeam.User.Country,
                            Created = missionTeam.DateCreated,
                            PhotoUrl = missionTeam.User.Photos.FirstOrDefault(p => p.IsMain).Url
                        }).ToList(),
                DateCreated = mission.DateCreated,
                Active = mission.Active,
                Public = mission.Public
            };
            return Ok(missionToReturn);
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

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editMissionTeamMembers/{missionId}")]
        public async Task<IActionResult> EditMissionTeamMembers(int missionId, 
            MissionTeamMemberEditDto missionTeamMemberEditDto)
        {
            var missionTeam = await _missionControlRepo.GetMissionTeam(missionId);
            var activeMissionTeamMembers = missionTeam.Where(u => u.Active == true).Select(i => i.UserId);
            var selectedMissionTeamMembers = missionTeamMemberEditDto.MissionTeamMemberUserIds;

            // this result is returning team members that are no longer team members so foreach we need to deactivate
            var activeMissionTeamMembersResult = activeMissionTeamMembers.Except(selectedMissionTeamMembers);

            foreach(var teamMemberUserId in activeMissionTeamMembersResult.Distinct()) 
            {
                var missionTeamMemberToDeactivate = await _missionControlRepo.GetMissionTeamMember(missionId, teamMemberUserId);
                missionTeamMemberToDeactivate.Active = false;

                if(await _missionControlRepo.SaveAll() == false)
                    return BadRequest("Failed to deactivate Mission Team Member");
            }

            selectedMissionTeamMembers = selectedMissionTeamMembers ?? new int[] { };
            // this result is returning new team member so foreach we need to add new
            var selectedMissionTeamMemberResult = selectedMissionTeamMembers.Except(activeMissionTeamMembers);

            foreach(var teamMemberUserId in selectedMissionTeamMemberResult.Distinct())
            {
                var teamMember = await _missionControlRepo.GetMissionTeamMember(missionId, teamMemberUserId);
                if(teamMember != null)
                {
                    teamMember.Active = true;
                }
                else
                {
                    var missionTeamMemberToAdd = new MissionTeam() {
                        UserId = teamMemberUserId,
                        MissionId = missionId,
                        Active = true
                    };
                    _missionControlRepo.Add(missionTeamMemberToAdd);
                }

                if(await _missionControlRepo.SaveAll() == false)
                        return BadRequest("Failed to add Mission Team Member");
            }

            var missionTeamFromRepo = await _missionControlRepo.GetMissionTeam(missionId);
            var missionTeamToReturn  = _mapper.Map<IEnumerable<MissionTeamToReturnDto>>(missionTeamFromRepo);
            return Ok(missionTeamToReturn);
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

        [HttpPost("CreateMissionAssessment")]
        public async Task<IActionResult> CreateMissionAssessment(MissionAssessmentForCreateDto missionAssessmentForCreateDto)
        {
            var missionAssessment = _mapper.Map<MissionAssessment>(missionAssessmentForCreateDto);

            _adminRepo.Add(missionAssessment);

            if (await _adminRepo.SaveAll())
            {
                var missionAssessmentToReturn = _mapper.Map<MissionAssessmentToReturnDto>(missionAssessment);

                return CreatedAtRoute("GetMissionAssessment", 
                    new {missionAssessmentId = missionAssessment.Id}, missionAssessmentToReturn);
            }
            throw new Exception("Creating the mission assessment failed on save");
        }

        [HttpPut("editmissionassessment")]
        public async Task<IActionResult> UpdateMissionAssessment(MissionAssessmentForUpdateDto missionAssessmentForUpdateDto)
        {
            /* id is the userid of the active user if needed
                if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized(); */
            var missionAssessmentId = missionAssessmentForUpdateDto.Id;
            var missionAssessmentFromRepo = await _adminRepo.GetMissionAssessment(missionAssessmentId);

            _mapper.Map(missionAssessmentForUpdateDto, missionAssessmentFromRepo);

            if (await _adminRepo.SaveAll())
                return NoContent();

            throw new Exception($"Mission Assessment {missionAssessmentId} failed on save.");
        }

        [HttpGet("missionassessment/{missionAssessmentId}", Name = "GetMissionAssessment")]
        public async Task<IActionResult> GetMissionAssessment(int missionAssessmentId)
        {            
            var userId =  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var missionAssessment = await _adminRepo.GetMissionAssessment(missionAssessmentId);

            var missionAssessmentToReturn = _mapper.Map<MissionAssessmentToReturnDto>(missionAssessment);
            
            return Ok(missionAssessmentToReturn);
        }
    }
}