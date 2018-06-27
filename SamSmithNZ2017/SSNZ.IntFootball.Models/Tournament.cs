using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.IntFootball.Models
{
    public class Tournament
    {
        public Tournament() { }

        public int CompetitionCode { get; set; }
        public int TournamentCode { get; set; }
        public int TournamentYear { get; set; }
        public string TournamentName { get; set; }
        public int HostTeamCode { get; set; }
        public string HostTeamName { get; set; }
        public string HostFlagName { get; set; }
        public int CoHostTeamCode { get; set; }
        public string CoHostTeamName { get; set; }
        public string CoHostFlagName { get; set; }
        public int CoHostTeamCode2 { get; set; }
        public string CoHostTeamName2 { get; set; }
        public string CoHostFlagName2 { get; set; }
        public int GameCount { get; set; }
        public int GamesCompleteCount { get; set; }
        public DateTime? MinGameTime { get; set; }
        public DateTime? MaxGameTime { get; set; }
        public int FormatCode { get; set; }
        public int R1FormatRoundCode { get; set; }
        public Boolean R1IsGroupStage { get; set; }
        public int R1NumberOfTeamsInGroup { get; set; }
        public int R1NumberOfGroupsInRound { get; set; }
        public int R1NumberOfTeamsFromGroupThatAdvance { get; set; }
        public int R1TotalNumberOfTeamsThatAdvance { get; set; }
        public string R1FirstGroupCode { get; set; }
        public int R2FormatRoundCode { get; set; }
        public Boolean R2IsGroupStage { get; set; }
        public int R2NumberOfTeamsInGroup { get; set; }
        public int R2NumberOfGroupsInRound { get; set; }
        public int R2NumberOfTeamsFromGroupThatAdvance { get; set; }
        public int R2TotalNumberOfTeamsThatAdvance { get; set; }
        public string R2FirstGroupCode { get; set; }
        public int R3FormatRoundCode { get; set; }
        public Boolean R3IsGroupStage { get; set; }
        public int R3NumberOfTeamsInGroup { get; set; }
        public int R3NumberOfGroupsInRound { get; set; }
        public int R3NumberOfTeamsFromGroupThatAdvance { get; set; }
        public int R3TotalNumberOfTeamsThatAdvance { get; set; }
        public string R3FirstGroupCode { get; set; }
        public decimal ImportingTotalPercentComplete { get; set; }
        public decimal ImportingTeamPercent { get; set; }
        public decimal ImportingGamePercent { get; set; }
        public decimal ImportingPlayerPercent { get; set; }
        public decimal ImportingGoalsPercent { get; set; }
        public decimal ImportingPenaltyShootoutGoalsPercent { get; set; }
        public string Notes { get; set; }
        public string LogoImage { get; set; }
        public string QualificationImage { get; set; }
        public int TotalGoals { get; set; }
        public int TotalPenalties { get; set; }
        public int TotalShootoutGoals { get; set; }

    }
}
