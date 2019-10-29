using System;

namespace MissionControlApp.API.Models
{
    public class MissionAppSettings
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public virtual Mission Mission { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }

        public MissionAppSettings()
        {
            DateCreated = DateTime.Now;
            Active = true;
        }
    }
}