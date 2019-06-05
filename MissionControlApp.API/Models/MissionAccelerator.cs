using System;
using System.ComponentModel.DataAnnotations;

namespace MissionControlApp.API.Models
{
    public class MissionAccelerator
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public Mission Mission { get; set; }
        public int AcceleratorId { get; set; }
        public Accelerator Accelerator { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool Active { get; set; }      

        public MissionAccelerator()
        {
            DateCreated = DateTime.Now;
            Active = true;
        }  
    }
}