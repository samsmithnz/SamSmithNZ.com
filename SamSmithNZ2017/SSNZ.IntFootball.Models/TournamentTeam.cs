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
        public string CoachName { get; set; }
        public string CoachNationalityFlagName { get; set; }
        public int ELORating { get; set; }
        public string ELORatingDifference
        {
            get
            {
                int result = ELORating - FifaRanking;
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

    }
}
