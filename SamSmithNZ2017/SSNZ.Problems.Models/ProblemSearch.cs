using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.Problems.Models
{
    public class ProblemSearch
    {
        public ProblemSearch() { }

        public string SearchText { get; set; }
        public int ProblemNumber { get; set; }
        public String Description { get; set; }
        public String Notes { get; set; }
    }
}
