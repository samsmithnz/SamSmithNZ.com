using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class IndexViewModel
    {
        public List<Tournament> Tournaments { get; set; }
        public List<TournamentImportStatus> TournamentsImportStatus { get; set; }

    }
}
