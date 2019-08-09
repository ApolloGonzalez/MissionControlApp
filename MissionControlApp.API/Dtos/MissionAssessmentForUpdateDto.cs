using System;

namespace MissionControlApp.API.Dtos
{
    public class MissionAssessmentForUpdateDto
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public int UserId { get; set; }
        public string DataLocation { get; set; }
        public string DataType { get; set; }
        public string DataDomainExpert { get; set; }
        public string ChallengeSolution { get; set; }
        public string DataQuality { get; set; }
        public string ChallengeType { get; set; }
        public string InfrastructureRequirement { get; set; }
        public string AccuracyRequirement { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public MissionAssessmentForUpdateDto()
        {
            UpdateDateTime = DateTime.Now;
            
        }
    }
}