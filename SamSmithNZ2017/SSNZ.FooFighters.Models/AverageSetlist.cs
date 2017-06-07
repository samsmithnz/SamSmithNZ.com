using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.FooFighters.Models
{
    public class AverageSetlist
    {
        public int YearCode { get; set; }
        public int SongCode { get; set; }
        public string SongName { get; set; }
        public int SongCount { get; set; }
        public int SongRank { get; set; }
        public decimal AvgShowSongOrder { get; set; }

    }
}
