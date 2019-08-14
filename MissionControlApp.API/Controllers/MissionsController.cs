using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissionControlApp.API.Data;
using MissionControlApp.API.Dtos;
using MissionControlApp.API.Helpers;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly IMissionControlRepository _repo;
        private readonly IMapper _mapper;

        public MissionsController(IMissionControlRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost("CreateMission")]
        public async Task<IActionResult> CreateMission(int userId, MissionForCreateDto missionForCreateDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
             var mission = new Mission {
                UserId = userId,
                MissionName = missionForCreateDto.MissionName,
                IndustryId = missionForCreateDto.IndustryId,
                BusinessFunctionId = missionForCreateDto.BusinessFunctionId,
                DesiredOutcome = missionForCreateDto.DesiredOutcome,
                Challenge = missionForCreateDto.Challenge,
                BusinessImpact = missionForCreateDto.BusinessImpact,
                TimeFrame = missionForCreateDto.TimeFrame,
                Public = missionForCreateDto.Public
            };

            _repo.Add(mission);

            if (await _repo.SaveAll())
            {
                foreach(MissionAccelerator ma in missionForCreateDto.MissionAccelerators)
                {
                    var missionAccelerator = new MissionAccelerator();
                    missionAccelerator.AcceleratorId = ma.AcceleratorId;
                    missionAccelerator.MissionId = mission.Id;
                    _repo.Add(missionAccelerator);
                    
                    await _repo.SaveAll();
                }

                foreach(MissionPlatform mp in missionForCreateDto.MissionPlatforms)
                {
                    var missionPlatform = new MissionPlatform();
                    missionPlatform.PlatformId = mp.PlatformId;
                    missionPlatform.MissionId = mission.Id;
                    _repo.Add(missionPlatform);
                    
                    await _repo.SaveAll();
                }
                return CreatedAtRoute("GetMission", new {userId = userId, missionId = mission.Id}, mission);
            }
            throw new Exception("Creating the mission failed on save");
        }

        [HttpGet("{missionId}", Name = "GetMission")]
        public async Task<IActionResult> GetMission(int missionId)
        {            
            var userId =  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var mission = await _repo.GetMission(userId, missionId);

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

        public async Task<IActionResult> GetMissions([FromQuery]MissionParams missionParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            missionParams.UserId = currentUserId;
            
            var missions = await _repo.GetMissions(missionParams);

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
                   /*  MissionTeam = 
                        (from missionTeam in mission.MissionTeams.Select(a => a.User).ToList()
                        select new MissionTeamToReturnDto
                        {
                            UserId = missionTeam.Id,
                            Username = missionTeam.UserName,
                            KnownAs = missionTeam.KnownAs,
                            Employee = missionTeam.Employee,
                            JobTitle = missionTeam.JobTitle,
                            Gender = missionTeam.Gender,
                            City = missionTeam.City,
                            Country = missionTeam.Country,
                            PhotoUrl = missionTeam.Photos.FirstOrDefault(p => p.IsMain).Url
                        }).ToList(), */
                    DateCreated = mission.DateCreated,
                    Active = mission.Active,
                    Public = mission.Public
                }).OrderByDescending(cr => cr.DateCreated);

            Response.AddPagination(missions.CurrentPage, missions.PageSize,
                missions.TotalCount, missions.TotalPages);

            return Ok(missionsToReturn);
        }

        [Authorize(Policy = "ManageMissionQueueRole")]
        [HttpGet("GetMissionsInQueue")]
        public async Task<IActionResult> GetMissionsInQueue([FromQuery]MissionParams missionParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            missionParams.UserId = currentUserId;
            
            var missions = await _repo.GetMissionsInQueue(missionParams);

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
                        (from missionTeam in mission.MissionTeam.ToList()
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

        [HttpGet("{missionId}/missionteam", Name = "GetMissionTeam")]
        public async Task<IActionResult> GetMissionTeam(int missionId)
        {
            var missionTeam = await _repo.GetMissionTeam(missionId);

            var missionTeamToReturn = (from teamMember in missionTeam.Where(a => a.Active == true)
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

        [HttpGet("{missionId}/assessment", Name = "GetAssessment")]
        public async Task<IActionResult> GetMissionAssessment(int missionId)
        {            
            var userId =  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var missionAssessment = await _repo.GetMissionAssessment(userId, missionId);

            var missionAssessmentToReturn = _mapper.Map<MissionAssessmentToReturnDto>(missionAssessment);
            
            return Ok(missionAssessmentToReturn);
        }

        [HttpGet("industries", Name = "GetIndustries")]
        public async Task<IActionResult> GetIndustries()
        {
            var industries = await _repo.GetIndustries();

            return Ok(industries);
        }

        [HttpGet("industry/{id}", Name = "GetIndustry")]
        public async Task<IActionResult> GetIndustry(int id)
        {
            var industry = await _repo.GetIndustry(id);

            return Ok(industry);
        }

        [HttpGet("businessfunctions", Name = "GetBusinessFunctions")]
        public async Task<IActionResult> GetBusinessFunctions()
        {
            var businessFunctions = await _repo.GetBusinessFunctions();

            return Ok(businessFunctions);
        }

        [HttpGet("businessfunction/{id}", Name = "GetBusinessFunction")]
        public async Task<IActionResult> GetBusinessFunction(int id)
        {
            var businessFunction = await _repo.GetBusinessFunction(id);

            return Ok(businessFunction);
        }

        [HttpGet("platforms", Name = "GetPlatforms")]
        public async Task<IActionResult> GetPlatforms()
        {
            var platforms = await _repo.GetPlatforms();

            var platfomsToReturn = _mapper.Map<IEnumerable<PlatformToReturnDto>>(platforms);

            return Ok(platfomsToReturn);
        }

        [HttpGet("missioncreateformlists", Name = "GetMissionCreateFormLists")]
        public async Task<IActionResult> GetMissionCreateFormLists()
        {
            var businessFunctions = await _repo.GetBusinessFunctions();
            var industries = await _repo.GetIndustries();
            var platforms = await _repo.GetPlatforms();
            
            var businessFunctionsToReturn = _mapper
                .Map<IEnumerable<BusinessFunctionToReturnDto>>(businessFunctions);
            var industriesToReturn = _mapper
                .Map<IEnumerable<IndustryToReturnDto>>(industries);
            var platfomsToReturn = _mapper
                .Map<IEnumerable<PlatformToReturnDto>>(platforms);

            var missionCreateFormListsToReturn = new MissionCreateFormListsToReturnDto {
                BusinessFunctions = businessFunctionsToReturn.ToList(),
                Industries = industriesToReturn.ToList(),
                Platforms = platfomsToReturn.ToList()
            };

            return Ok(missionCreateFormListsToReturn);
        }

        [HttpGet("businessfunction/{businessFunctionId}/industry/{industryId}", Name = "GetMissionCreateFormAccelerators")]
        public async Task<IActionResult> GetMissionCreateFormAccelerators(int businessFunctionId, int industryId)
        {
            var accelerators = await _repo
                .GetAcceleratorsByBusinessFunctionAndIndustry(businessFunctionId, industryId);
             
            var acceleratorsToReturn = _mapper
                .Map<IEnumerable<AcceleratorToReturnDto>>(accelerators);

            return Ok(acceleratorsToReturn);
        }

    }
}