using System;

namespace MissionControlApp.API.Dtos
{
    public class MissionTeamToReturnDto
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public bool Employee { get; set; }
        public string JobTitle { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
    }
}