using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MissionControlApp.API.Models
{
    public class MissionMetrics
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public virtual int Mission { get; set; }
        public int Models { get; set; }
        public int Ideas { get; set; } 
        public int ModelsDeployed { get; set; }
        public int Experiments { get; set; }
        public int ExperimentRuns { get; set; }
        
        [Column(TypeName="Money")]
        public decimal Cost { get; set; }
        public string MissionStatus { get; set; }
        public int NotebookVMs { get; set; }
        public bool Active { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public MissionMetrics() 
        {
            DateCreated = DateTime.Now;
            Active = true;
        }

    }
}