using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MissionControlApp.API.Models
{
    public class Accelerator
    {
        public int Id { get; set; }
        public int BusinessFunctionId { get; set; }
        public BusinessFunction BusinessFunction { get; set; }
        public int IndustryId { get; set; }
        public Industry Industry { get; set; }
        public string AcceleratorName { get; set; }
        public string ModelType { get; set; }
        //public ICollection<MissionAccelerator> MissionAccelerators { get; set; }
        //public int? MissionAcceleratorId { get; set; }
        //public MissionAccelerator MissionAccelerator { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
    }
}