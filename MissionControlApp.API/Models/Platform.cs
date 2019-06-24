using System;

namespace MissionControlApp.API.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string PlatformName { get; set; }
        public string PlatformAlias { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
    }
}