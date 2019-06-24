using System;

namespace MissionControlApp.API.Dtos
{
    public class PlatformToReturnDto
    {
        public int Id { get; set; }
        public string PlatformName { get; set; }
        public string PlatformAlias { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}