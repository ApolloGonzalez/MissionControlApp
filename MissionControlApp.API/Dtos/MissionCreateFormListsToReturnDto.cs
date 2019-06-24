using System.Collections.Generic;

namespace MissionControlApp.API.Dtos
{
    public class MissionCreateFormListsToReturnDto
    {
        public ICollection<PlatformToReturnDto> Platforms { get; set; }
        public ICollection<BusinessFunctionToReturnDto> BusinessFunctions { get; set; }
        public ICollection<IndustryToReturnDto> Industries { get; set; }
    }
}