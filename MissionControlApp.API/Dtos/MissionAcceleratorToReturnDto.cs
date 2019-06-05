using System;

namespace MissionControlApp.API.Dtos
{
    public class MissionAcceleratorToReturnDto
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public string AcceleratorName { get; set; }
        public string ModelType { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }

        public MissionAcceleratorToReturnDto()
        {
            DateCreated = DateTime.Now;
            Active = true;
        }  
    }
}