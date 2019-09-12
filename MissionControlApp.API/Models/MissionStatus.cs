using System;

namespace MissionControlApp.API.Models
{
    public class MissionStatus
    {
        public int Id { get; set; }
        public string MissionStatusName { get; set; }
        public string MissionStatusCode { get; set; }
        public string MissionStatusAlias { get; set; }
        public int Sequence { get; set; }
        public string Description { get; set; }
        public string MissionStatusType { get; set; }
        //public Mission Mission { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
    }
}