using System;
using System.Collections.Generic;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Dtos
{
    public class MissionForCreateDto
    {
        public int UserId { get; set; }
        public string MissionName { get; set; }
        public int IndustryId { get; set; }
        public int BusinessFunctionId { get; set; }
        public string Challenge { get; set; }
        public string DesiredOutcome { get; set; }
        public string BusinessImpact { get; set; }
        public int TimeFrame { get; set; }
        public ICollection<MissionAccelerator> MissionAccelerators { get; set; }
        public ICollection<MissionPlatform> MissionPlatforms { get; set; }
        public bool Public { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }

        public MissionForCreateDto()
        {
            DateCreated = DateTime.Now;
            Active = false;
        }
    }
}