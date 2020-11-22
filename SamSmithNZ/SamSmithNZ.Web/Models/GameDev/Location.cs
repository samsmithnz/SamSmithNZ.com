using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.GameDev
{
    public class Location
    {
        public int Col { get; set; }
        public int Row { get; set; }
        public string Entry { get; set; }
        public string Exit { get; set; }

        public Location(int col, int row, string entry, string exit)
        {
            this.Col = col;
            this.Row = row;
            this.Entry = entry;
            this.Exit = exit;
        }
    }
}