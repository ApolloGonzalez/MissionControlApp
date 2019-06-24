using System;

namespace MissionControlApp.API.Models
{
    public class MissionPlatform
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public Mission Mission { get; set; }
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }      

        public MissionPlatform()
        {
            DateCreated = DateTime.Now;
            Active = true;
        }  
    }
}