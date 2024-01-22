﻿using Microsoft.AspNetCore.Identity;

namespace api_mvc.Models
{
    public class PlayerViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public long? TournamentId { get; set; }
        public TournamentViewModel? Tournament { get; set; }
    }
}
