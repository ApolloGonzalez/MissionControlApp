using System;
using System.Collections.Generic;

namespace MissionControlApp.API.Models
{
    public class Industry
    {
        public int Id { get; set; }
        public string IndustryName { get; set; }
        public string IndustryAlias { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
    }
}