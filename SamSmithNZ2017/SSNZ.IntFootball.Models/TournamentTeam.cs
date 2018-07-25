using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.IntFootball.Models
{
    public class TournamentTeam
    {
        public TournamentTeam() { }

        public string Placing { get; set; }
        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public string FlagName { get; set; }
        public int RegionCode { get; set; }
        public string RegionName { get; set; }
        public int FifaRanking { get; set; }
        public int StartingEloRating { get; set; }
        public string CoachName { get; set; }
        public string CoachNationalityFlagName { get; set; }
        public int ELORating { get; set; }
        public string ELORatingDifference
        {
            get
            {
                int result = ELORating - StartingEloRating;
                if (result > 0)
                {
                    return "+" + result.ToString();
                }
                else
                {
                    return result.ToString();
                }

            }
        }
        public bool IsActive { get; set; }
        public decimal ChanceToWin { get; set; }
        public int GF { get; set; }
        public int GA { get; set; }
        public int GD { get; set; }
    }
}
