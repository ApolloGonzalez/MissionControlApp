using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MissionControlApp.API.Models
{
    public class Accelerator
    {
        public int Id { get; set; }
        public int BusinessFunctionId { get; set; }
        public virtual BusinessFunction BusinessFunction { get; set; }
        public int IndustryId { get; set; }
        public virtual Industry Industry { get; set; }
        public string AcceleratorName { get; set; }
        public string ModelType { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
    }
}