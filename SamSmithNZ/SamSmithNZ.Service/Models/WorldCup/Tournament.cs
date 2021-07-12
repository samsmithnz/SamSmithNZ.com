using System;

namespace SamSmithNZ.Service.Models.WorldCup
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
        public DateTime? MinGameTime { get; set; }
        public DateTime? MaxGameTime { get; set; }
        public int FormatCode { get; set; }
        public int R1FormatRoundCode { get; set; }
        public bool R1IsGroupStage { get; set; }
        public int R1NumberOfTeamsInGroup { get; set; }
        public int R1NumberOfGroupsInRound { get; set; }
        public int R1NumberOfTeamsFromGroupThatAdvance { get; set; }
        public int R1TotalNumberOfTeamsThatAdvance { get; set; }
        public string R1FirstGroupCode { get; set; }
        public int R2FormatRoundCode { get; set; }
        public bool R2IsGroupStage { get; set; }
        public int R2NumberOfTeamsInGroup { get; set; }
        public int R2NumberOfGroupsInRound { get; set; }
        public int R2NumberOfTeamsFromGroupThatAdvance { get; set; }
        public int R2TotalNumberOfTeamsThatAdvance { get; set; }
        public string R2FirstGroupCode { get; set; }
        public int R3FormatRoundCode { get; set; }
        public bool R3IsGroupStage { get; set; }
        public int R3NumberOfTeamsInGroup { get; set; }
        public int R3NumberOfGroupsInRound { get; set; }
        public int R3NumberOfTeamsFromGroupThatAdvance { get; set; }
        public int R3TotalNumberOfTeamsThatAdvance { get; set; }
        public string R3FirstGroupCode { get; set; }
        public string Notes { get; set; }
        public string LogoImage { get; set; }
        public string QualificationImage { get; set; }

    }
}
