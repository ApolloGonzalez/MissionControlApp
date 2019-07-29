using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MissionControlApp.API.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string MissionName { get; set; }
        public int IndustryId { get; set; }
        public Industry Industry { get; set; }
        public int BusinessFunctionId { get; set; }
        public BusinessFunction BusinessFunction { get; set; }
        public string Challenge { get; set; }
        public string DesiredOutcome { get; set; }
        public string BusinessImpact { get; set; }
        public int TimeFrame { get; set; }
        public ICollection<MissionAccelerator> MissionAccelerators { get; set; }
        public ICollection<MissionPlatform> MissionPlatforms { get; set; }
        public ICollection<MissionTeam> MissionTeam { get; set; }
        public bool Public { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
        public Mission()
        {
            DateCreated = DateTime.Now;
            Active = false;
        }
    }
}