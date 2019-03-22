using System;
using System.Collections.Generic;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Dtos
{
    public class UserForDetailDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailDto> Photos { get; set; }
    }
}