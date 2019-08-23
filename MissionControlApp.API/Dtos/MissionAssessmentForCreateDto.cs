using System;

namespace MissionControlApp.API.Dtos
{
    public class MissionAssessmentForCreateDto
    {
        public int UserId { get; set; }
        public int MissionId { get; set; }
        public string DataLocation { get; set; }
        public string DataType { get; set; }
        public string DataDomainExpert { get; set; }
        public string ChallengeSolution { get; set; }
        public string DataQuality { get; set; }
        public ulong EstimatedCost { get; set; }        
        public string ChallengeType { get; set; }
        public string InfrastructureRequirement { get; set; }
        public string AccuracyRequirement { get; set; }
        public bool Active { get; set; }
        public DateTime? DateCreated { get; set; }

        public MissionAssessmentForCreateDto()
        {
            DateCreated = DateTime.Now;
            Active = false;
        }
    }
}