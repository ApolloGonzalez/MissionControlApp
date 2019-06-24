using System;

namespace MissionControlApp.API.Dtos
{
    public class MissionPlatformToReturnDto
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public string PlatformName { get; set; }
        public string PlatformAlias { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }

        public MissionPlatformToReturnDto()
        {
            DateCreated = DateTime.Now;
            Active = true;
        }  
    }
}