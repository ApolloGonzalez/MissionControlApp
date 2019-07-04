using System;

namespace MissionControlApp.API.Models
{
    public class MissionTeam
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public Mission Mission { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool Active { get; set; }
        public DateTime? DateCreated { get; set; }

        public MissionTeam()
        {
            DateCreated = DateTime.Now;
            Active = true;
        }  
    }
}