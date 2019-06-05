using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
                TimeFrame = missionForCreateDto.TimeFrame
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
                MissionId = mission.Id,
                MissionName = mission.MissionName,
                IndustryId = mission.IndustryId,
                IndustryAlias = mission.Industry.IndustryAlias,
                BusinessFunctionId = mission.BusinessFunctionId,
                BusinessFunctionAlias = mission.BusinessFunction.BusinessFunctionAlias,
                Challenge = mission.Challenge,
                DesiredOutcome = mission.DesiredOutcome,
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
                        Active = accelerator.Active
                    }).ToList(),
                DateCreated = mission.DateCreated,
                Active = mission.Active
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
                    MissionId = mission.Id,
                    MissionName = mission.MissionName,
                    IndustryId = mission.IndustryId,
                    IndustryAlias = mission.Industry.IndustryAlias,
                    BusinessFunctionId = mission.BusinessFunctionId,
                    BusinessFunctionAlias = mission.BusinessFunction.BusinessFunctionAlias,
                    Challenge = mission.Challenge,
                    DesiredOutcome = mission.DesiredOutcome,
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
                            Active = accelerator.Active
                        }).ToList(),
                    DateCreated = mission.DateCreated,
                    Active = mission.Active
                });

            Response.AddPagination(missions.CurrentPage, missions.PageSize,
                missions.TotalCount, missions.TotalPages);

            return Ok(missionsToReturn);
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
    }
}