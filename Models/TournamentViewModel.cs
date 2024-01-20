﻿using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.AspNetCore.Identity;

namespace api_mvc.Models
{
    public class TournamentViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsFinished { get; set; }
        public bool IsStarted { get; set; }
        public int PlayersNumber { get; set; }
        public int RoundsNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public string? OwnerUserId { get; set; }
        public IdentityUser? OwnerUser { get; set; }
    }
}
