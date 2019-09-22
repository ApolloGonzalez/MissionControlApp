using System;
using System.Collections.Generic;

namespace MissionControlApp.API.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string MissionName { get; set; }
        public int IndustryId { get; set; }
        public virtual Industry Industry { get; set; }
        public int BusinessFunctionId { get; set; }
        public virtual BusinessFunction BusinessFunction { get; set; }
        public string Challenge { get; set; }
        public string DesiredOutcome { get; set; }
        public string BusinessImpact { get; set; }
        public ulong EstimatedRoi { get; set; }
        public ulong ActualRoi { get; set; }
        public ulong ActualCost { get; set; }
        public int TimeFrame { get; set; }
        public virtual ICollection<MissionAccelerator> MissionAccelerators { get; set; }
        public virtual ICollection<MissionPlatform> MissionPlatforms { get; set; }
        public virtual ICollection<MissionTeam> MissionTeam { get; set; }
        public virtual MissionAssessment MissionAssessment { get; set; }
        public bool Public { get; set; }
        public int MissionStatusId { get; set; }
        public virtual MissionStatus MissionStatus { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
        public Mission()
        {
            DateCreated = DateTime.Now;
            Active = false;
        }
    }
}