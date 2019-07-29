using System;
using System.Collections.Generic;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Dtos
{
    public class MissionToReturnDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string KnownAs { get; set; }
        public bool Employee { get; set; }
        public string JobTitle { get; set; }
        public int MissionId { get; set; }
        public string MissionName { get; set; }
        public int IndustryId { get; set; }
        public string IndustryAlias { get; set; }
        public int BusinessFunctionId { get; set; }
        public string BusinessFunctionAlias { get; set; }
        public string Challenge { get; set; }
        public string DesiredOutcome { get; set; }
        public string BusinessImpact { get; set; }
        public int TimeFrame { get; set; }
        public ICollection<MissionAcceleratorToReturnDto> Accelerators { get; set; }
        public ICollection<MissionPlatformToReturnDto> Platforms { get; set; }
        public ICollection<MissionTeamToReturnDto> MissionTeam { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
        public bool Public { get; set; }

        public MissionToReturnDto()
        {
            DateCreated = DateTime.Now;
            Active = false;
        }
    }
}