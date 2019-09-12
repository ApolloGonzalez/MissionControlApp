using System;

namespace MissionControlApp.API.Models
{
    public class BusinessFunction
    {
        public int Id { get; set; }
        public string BusinessFunctionName { get; set; }
        public string BusinessFunctionAlias { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }
    }
}