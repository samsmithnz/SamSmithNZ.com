using System;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class Player
    {
        public Player() { }

        public int PlayerCode { get; set; }
        public string PlayerName { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public int TournamentCode { get; set; }
        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsCaptain { get; set; }
        public string ClubName { get; set; }

    }
}
