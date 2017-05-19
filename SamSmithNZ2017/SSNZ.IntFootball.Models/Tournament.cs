using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.IntFootball.Models
{
    public class Tournament
    {
        public Tournament() { }

        private string logoImage;
        private string qualificationImage;


        public short CompetitionCode { get; set; }
        public short TournamentCode { get; set; }
        public short TournamentYear { get; set; }
        public string TournamentName { get; set; }
        public short HostTeamCode { get; set; }
        public string HostTeamName { get; set; }
        public string HostFlagName { get; set; }
        public short CoHostTeamCode { get; set; }
        public string CoHostTeamName { get; set; }
        public string CoHostFlagName { get; set; }
        public short GameCount { get; set; }
        public DateTime MinGameTime { get; set; }
        public DateTime MaxGameTime { get; set; }
        public short FormatCode { get; set; }
        public short R1_FormatRoundCode { get; set; }
        public Boolean R1_IsGroupStage { get; set; }
        public short R1_NumberOfTeamsInGroup { get; set; }
        public short R1_NumberOfGroupsInRound { get; set; }
        public short R1_NumberOfTeamsFromGroupThatAdvance { get; set; }
        public short R1_TotalNumberOfTeamsThatAdvanceFromRound { get; set; }
        public string R1_FirstGroupCode { get; set; }
        public short R2_FormatRoundCode { get; set; }
        public Boolean R2_IsGroupStage { get; set; }
        public short R2_NumberOfTeamsInGroup { get; set; }
        public short R2_NumberOfGroupsInRound { get; set; }
        public short R2_NumberOfTeamsFromGroupThatAdvance { get; set; }
        public short R2_TotalNumberOfTeamsThatAdvanceFromRound { get; set; }
        public string R2_FirstGroupCode { get; set; }
        public short R3_FormatRoundCode { get; set; }
        public Boolean R3_IsGroupStage { get; set; }
        public short R3_NumberOfTeamsInGroup { get; set; }
        public short R3_NumberOfGroupsInRound { get; set; }
        public short R3_NumberOfTeamsFromGroupThatAdvance { get; set; }
        public short R3_TotalNumberOfTeamsThatAdvanceFromRound { get; set; }
        public string R3_FirstGroupCode { get; set; }
        public decimal ImportingTotalPercentComplete { get; set; }
        public decimal ImportingTeamPercent { get; set; }
        public decimal ImportingGamePercent { get; set; }
        public decimal ImportingPlayerPercent { get; set; }
        public decimal ImportingGoalsPercent { get; set; }
        public decimal ImportingPenaltyShootoutGoalsPercent { get; set; }
        public string Notes { get; set; }
        public string LogoImage
        {
            get
            {
                return logoImage;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    logoImage = "images/no_image.gif";
                }
                else
                {
                    logoImage = value;
                }
            }
        }
        public string QualificationImage
        {
            get
            {
                return qualificationImage;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    qualificationImage = "images/no_image.gif";
                }
                else
                {
                    qualificationImage = value;
                }
            }
        }
    }
}
